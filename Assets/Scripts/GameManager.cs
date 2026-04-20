using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ApiClient api;
    [SerializeField] private List<PlayerController> players;
    [SerializeField] private UIGameManager uiManager;
    public string gameId = "carrera1";

    [Header("Configuracion")]
    private int localPlayerId;
    private int otherPlayerId;
    [SerializeField] private Camera cameraPlayer0;
    [SerializeField] private Camera cameraPlayer1;
    [SerializeField] private float pollingInterval = 0.1f;

    private bool gameFinished = false;
    private bool isReady = false;

    public void Start()
    {
        localPlayerId = PlayerPrefs.GetInt("localPlayerId", 0);
        otherPlayerId = PlayerPrefs.GetInt("otherPlayerId", 1);

        api.OnDataReceived += OnDataReceived;
        uiManager.Initialize();
        StartCoroutine(CheckServerConnection());
    }

    private IEnumerator CheckServerConnection()
    {
        ServerData resetData = new ServerData { posX = 0f, posY = 0f, posZ = 0f };
        yield return StartCoroutine(api.PostPlayerData(gameId, localPlayerId.ToString(), resetData));
        yield return new WaitForSeconds(1f);

        string testUrl = "http://localhost:5005/swagger-ui";
        using (UnityEngine.Networking.UnityWebRequest webRequest =
               UnityEngine.Networking.UnityWebRequest.Get(testUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityEngine.Networking.UnityWebRequest.Result.Success ||
                webRequest.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError)
            {
                uiManager.ShowServerConnected();
                yield return new WaitForSeconds(2f);
                uiManager.HideServerConnected();
                StartGame();
            }
            else
            {
                uiManager.ShowConnectionError();
            }
        }
    }

    public void RetryConnection()
    {
        uiManager.HideConnectionError();
        StartCoroutine(CheckServerConnection());
    }

    public void StartGame()
    {
        gameFinished = false;
        isReady = false;

        players[localPlayerId].ResetPlayer();

        if (localPlayerId == 0)
        {
            cameraPlayer0.gameObject.SetActive(true);
            cameraPlayer1.gameObject.SetActive(false);
        }
        else
        {
            cameraPlayer0.gameObject.SetActive(false);
            cameraPlayer1.gameObject.SetActive(true);
        }

        players[localPlayerId].SetupLocalPlayer(this);
        players[localPlayerId].StartGame();
        uiManager.ShowBanner(localPlayerId);
        StartCoroutine(PollingCoroutine());
        StartCoroutine(SetReady());
    }

    private IEnumerator SetReady()
    {
        yield return new WaitForSeconds(3f);
        isReady = true;
    }

    private IEnumerator PollingCoroutine()
    {
        while (!gameFinished)
        {
            StartCoroutine(api.GetPlayerData(gameId, otherPlayerId.ToString()));
            yield return new WaitForSeconds(pollingInterval);
        }
    }

    public void OnDataReceived(int playerId, ServerData data)
    {
        if (playerId >= 0 && playerId < players.Count)
        {
            if (data.posX == 999f && !gameFinished && isReady)
            {
                gameFinished = true;
                players[localPlayerId].StopGame();
                uiManager.ShowLoser();
                return;
            }
            if (data.posX != 999f)
            {
                Vector3 position = new Vector3(data.posX, data.posY, data.posZ);
                players[playerId].MovePlayer(position);
            }
        }
    }

    public void SendPlayerPosition(int playerId)
    {
        Vector3 position = players[playerId].GetPosition();
        ServerData data = new ServerData
        {
            posX = position.x,
            posY = position.y,
            posZ = position.z
        };
        StartCoroutine(api.PostPlayerData(gameId, playerId.ToString(), data));
    }

    public void PlayerReachedGoal(int playerId)
    {
        if (gameFinished) return;
        gameFinished = true;
        players[localPlayerId].StopGame();
        uiManager.ShowWinner();
        ServerData winData = new ServerData { posX = 999f, posY = 0f, posZ = 0f };
        StartCoroutine(api.PostPlayerData(gameId, localPlayerId.ToString(), winData));
    }
}
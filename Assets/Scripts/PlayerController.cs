using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerId;
    [SerializeField] private KeyCode moveKey = KeyCode.Space;
    [SerializeField] private float stepSize = 0.5f;
    
    private bool isLocalPlayer = false;
    private bool gameStarted = false;
    private bool gameFinished = false;
    
    public float metaX = 20f;
    
    private GameManager gameManager;

    public void SetupLocalPlayer(GameManager gm)
    {
        isLocalPlayer = true;
        gameManager = gm;
    }

    public void StartGame()
    {
        gameStarted = true;
        gameFinished = false;
    }

    public void StopGame()
    {
        gameFinished = true;
    }

    public void ResetPlayer()
    {
        isLocalPlayer = false;
        gameStarted = false;
        gameFinished = false;
        gameManager = null;
    }

    void Update()
    {
        if (!isLocalPlayer || !gameStarted || gameFinished) return;

        if (Input.GetKeyDown(moveKey))
        {
            transform.position += new Vector3(stepSize, 0, 0);
            gameManager.SendPlayerPosition(playerId);

            if (transform.position.x >= metaX)
            {
                gameManager.PlayerReachedGoal(playerId);
            }
        }
    }

    public void MovePlayer(Vector3 position)
    {
        transform.position = position;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public int GetPlayerId()
    {
        return playerId;
    }
}
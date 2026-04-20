using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelWinner;
    [SerializeField] private GameObject panelLoser;
    [SerializeField] private GameObject panelConnectionError;
    [SerializeField] private GameObject panelServerConnected;
    [SerializeField] private GameObject panelSettings;

    [Header("Banners")]
    [SerializeField] private GameObject bannerPlayer0;
    [SerializeField] private GameObject bannerPlayer1;

    public void Initialize()
    {
        panelWinner.SetActive(false);
        panelLoser.SetActive(false);
        panelConnectionError.SetActive(false);
        panelServerConnected.SetActive(false);
        panelSettings.SetActive(false);
    }

    public void ShowWinner() => panelWinner.SetActive(true);
    public void ShowLoser() => panelLoser.SetActive(true);
    public void ShowConnectionError() => panelConnectionError.SetActive(true);
    public void HideConnectionError() => panelConnectionError.SetActive(false);
    public void ShowServerConnected() => panelServerConnected.SetActive(true);
    public void HideServerConnected() => panelServerConnected.SetActive(false);

    public void ShowBanner(int localPlayerId)
    {
        bannerPlayer0.SetActive(localPlayerId == 0);
        bannerPlayer1.SetActive(localPlayerId == 1);
    }

    public void OnSettingsButton()
    {
        if (panelSettings.activeSelf)
        {
            panelSettings.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            panelSettings.SetActive(true);
        }
    }

    public void OnPauseButton() => Time.timeScale = 0f;
    public void OnPlayButton() => Time.timeScale = 1f;

    public void OnStopButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScene");
    }

    public void OnReplayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SelectPlayerScene");
    }

    public void OnExitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HomeScene");
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPlayerManager : MonoBehaviour
{
    public void SelectPlayer1()
    {
        PlayerPrefs.SetInt("localPlayerId", 0);
        PlayerPrefs.SetInt("otherPlayerId", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }

    public void SelectPlayer2()
    {
        PlayerPrefs.SetInt("localPlayerId", 1);
        PlayerPrefs.SetInt("otherPlayerId", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameScene");
    }
    public void OnBackButton()
    {
        SceneManager.LoadScene("HomeScene");
    }
}
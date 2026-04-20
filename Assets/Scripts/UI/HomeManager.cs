using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    [SerializeField] private GameObject panelHome;
    [SerializeField] private GameObject panelInstructions1;
    [SerializeField] private GameObject panelInstructions2;

    void Start()
    {
        panelHome.SetActive(true);
        panelInstructions1.SetActive(false);
        panelInstructions2.SetActive(false);
    }

    public void OnPlayButton()
    {
        panelHome.SetActive(false);
        panelInstructions1.SetActive(true);
    }

    public void OnNextButton()
    {
        panelInstructions1.SetActive(false);
        panelInstructions2.SetActive(true);
    }

    public void OnContinueButton()
    {
        SceneManager.LoadScene("SelectPlayerScene");
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
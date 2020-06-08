using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private Text highscoreText;

    [SerializeField]
    private GameObject CanvasQuiteGame;
    [SerializeField]
    private GameObject CanvasInfo;
    [SerializeField]
    private GameObject CanvasInstructions;
    [SerializeField]
    private GameObject CanvasStore;

#pragma warning restore 0649

    void Start()
    {
        highscoreText.text = "Highscore: "+((int)PlayerPrefs.GetFloat("Highscore")).ToString();
        CanvasQuiteGame.SetActive(false);
        CanvasInfo.SetActive(false);
        CanvasInstructions.SetActive(false);
        CanvasStore.SetActive(false);
    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        if (CanvasQuiteGame != null)
        {
        CanvasQuiteGame.SetActive(true);
        }
    }
    public void Info()
    {
        if (CanvasInfo != null)
        {
            CanvasInfo.SetActive(true);
        }
    }
    public void QuitGamePanel()
    {
        Application.Quit();
    }

    public void GoBackToMainMenu()
    {
        CanvasQuiteGame.SetActive(false);

    }
    public void ExitInfo()
    {
        CanvasInfo.SetActive(false);
    }
    public void ExitInstructions()
    {
        CanvasInstructions.SetActive(false);
    }
    public void GoToNextPanel()
    {
        CanvasInfo.SetActive(false);
        CanvasInstructions.SetActive(true);
    }
    public void GoToPreviousPanel()
    {
        CanvasInfo.SetActive(true);
        CanvasInstructions.SetActive(false);
    }
    public void OpenStore()
    {
        CanvasStore.SetActive(true);

    }
    public void CloseStore()
    {
        CanvasStore.SetActive(false);

    }
}

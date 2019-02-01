using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICanvasResult : MonoBehaviour
{
    public enum EndGameType
    {
        EndGame_50Lose,
        EndGame_LoseDie,
        EndGame_Win
    }
    public EndGameType endgameType;

    public GameObject EndGame_50Lose;
    public GameObject EndGame_LoseDie;
    public GameObject EndGame_Win;

    public GameObject pnlMenu;
    public GameObject btnNext;
    public GameObject btnRetry;

    // Use this for initialization
    void Start()
    {
        if (GameSystem.instance)
        {
            if (GameSystem.instance.isWin)
            {
                endgameType = EndGameType.EndGame_Win;
                EndGame_Win.SetActive(true);
                ShowPnlMenu();
                btnNext.SetActive(true);
            }
            else if (GameSystem.instance.isLoose50)
            {
                endgameType = EndGameType.EndGame_50Lose;
                EndGame_50Lose.SetActive(true);
                Invoke("ShowPnlMenu", 2.1f);
                btnRetry.SetActive(true);
            }
            else if (GameSystem.instance.isLoose)
            {
                endgameType = EndGameType.EndGame_LoseDie;
                EndGame_LoseDie.SetActive(true);
                ShowPnlMenu();
                btnRetry.SetActive(true);
            }
        }
    }

    void ShowPnlMenu()
    {
        pnlMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBtnRetryClicked()
    {
        Log.Info("OnBtnRetryClicked()");
        SceneManager.LoadScene(GameSystem.instance.lastLevel);
    }

    public void OnBtnNextClicked()
    {
        Log.Info("OnBtnNextClicked()");
        GameSystem.instance.levelID += 1;
        SceneManager.LoadScene("level" + GameSystem.instance.levelID);
    }
}

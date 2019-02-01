using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UICanvasIngame : MonoBehaviour
{
    public TextMeshProUGUI txtPause;
    private bool isPauseGame;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        OnEventListener();
    }

    private void OnDisable()
    {
        StopEventListener();
    }

    void OnEventListener()
    {
        Player.Event_PlayerReachEnd += Event_PlayerReachEnd_Handler;
    }

    void StopEventListener()
    {
        Player.Event_PlayerReachEnd -= Event_PlayerReachEnd_Handler;
    }

    private void Event_PlayerReachEnd_Handler(GameObject sender)
    {
        Log.Info("Event_PlayerReachEnd_Handler()");
        //TODO: finish game UI
        // change scene
        SceneManager.LoadScene(GlobalVar.EnemyTag);
    }

    public void OnBtnPauseClicked()
    {
        Log.Info("OnBtnPauseClicked()");
        if (!isPauseGame)
        {
            Time.timeScale = 0f;
            isPauseGame = true;
            txtPause.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            isPauseGame = false;
            txtPause.gameObject.SetActive(false);
        }
    }

    public void BtnWin()
    {
        GameSystem.instance.isWin = true;
        SceneManager.LoadScene(GlobalVar.ResultScene);
    }

    public void BtnLoose50()
    {
        GameSystem.instance.isLoose50 = true;
        SceneManager.LoadScene(GlobalVar.ResultScene);
    }

    public void BtnLoose()
    {
        GameSystem.instance.isLoose = true;
        SceneManager.LoadScene(GlobalVar.ResultScene);
    }
}

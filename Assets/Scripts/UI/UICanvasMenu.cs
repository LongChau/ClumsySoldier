using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICanvasMenu : MonoBehaviour
{
    public Image imgPnlBG;
    public Animator pnlBGAnim;
    public Image imgBegin;

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
        UIImageStory.Event_AnimComplete += Event_AnimComplete_Handler;
        FakePlayerControl.Event_FinishMoving += Event_FinishMoving_Handler;
    }

    private void Event_FinishMoving_Handler(GameObject sender)
    {
        imgPnlBG.enabled = true;
        pnlBGAnim.enabled = true;
    }

    void StopEventListener()
    {
        UIImageStory.Event_AnimComplete -= Event_AnimComplete_Handler;
    }

    private void Event_AnimComplete_Handler(GameObject sender)
    {
        imgBegin.gameObject.SetActive(false);
    }

    public void BtnPlayClicked()
    {
        Log.Info("BtnPlayClicked()");
        SceneManager.LoadScene("level1");
    }

    public void BtnExitClicked()
    {
        Log.Info("BtnExitClicked()");
        Application.Quit();
    }
}

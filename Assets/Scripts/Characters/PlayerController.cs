using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public Player player;
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private SwordContactController swordContact;
    private int rotAngle;
    //// Use this for initialization
    //void Start () {

    //}

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            RotateRight();
        }
        else if (Input.GetMouseButtonDown(0) && 
            player.playerGesture.GetCurrentAction() == DrawAction.None)   // for sword
        {
            Log.Info("Input.GetMouseButtonDown(0)");
            //  check touch top or bot?
            Vector3 touchPos;
            touchPos = Input.mousePosition;
            float halfScreen = Screen.height / 2;
            if (touchPos.y >= halfScreen)     // touch top
            {
                Log.Info("touch top");
                // TODO: check if player use magic
                //----
                // change sword state to fire magic


                // if not use magic then fire normal
                sword.SetActive(true);
                swordContact.swordState = SwordContactController.SwordState.fire;
                player.playerState = Player.PlayerState.attack;
            }
            else    // touch bot
            {
                Log.Info("touch bot");
                sword.SetActive(true);
                swordContact.swordState = SwordContactController.SwordState.defend;
            }
        }
    }

    private void RotateLeft()
    {
        rotAngle -= 90;
        if (rotAngle < 0)
        {
            rotAngle += 360;
        }
        ApplyRotAngle();
    }

    private void RotateRight()
    {
        rotAngle += 90;
        if (rotAngle >= 360)
        {
            rotAngle -= 360;
        }
        ApplyRotAngle();
    }

    private void ApplyRotAngle()
    {
        transform.rotation = Quaternion.Euler(0, rotAngle, 0);
    }

    
}

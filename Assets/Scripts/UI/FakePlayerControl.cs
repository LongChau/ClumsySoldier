using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlayerControl : MonoBehaviour
{
    public delegate void FinishMoving(GameObject sender);
    public static event FinishMoving Event_FinishMoving;

    public bool canMove;
    public bool finishMoving;
    // Use this for initialization
    void Start()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !finishMoving)
        {
            transform.Translate(Vector3.left * 2.0f * Time.deltaTime);
            //Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 2.0f * Time.deltaTime);
            if (transform.position.x <= 0)
            {
                canMove = false;
                // finish moving
                finishMoving = true;
                if (finishMoving)
                {
                    finishMoving = false;
                    if (Event_FinishMoving != null)
                    {
                        Event_FinishMoving(gameObject);
                    }
                }
            }
        }
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
    }

    void StopEventListener()
    {
        UIImageStory.Event_AnimComplete -= Event_AnimComplete_Handler;
    }

    private void Event_AnimComplete_Handler(GameObject sender)
    {
        canMove = true;
    }
}

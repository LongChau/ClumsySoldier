using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIImageStory : MonoBehaviour
{
    public delegate void AnimComplete(GameObject sender);
    public static event AnimComplete Event_AnimComplete;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnimationComplete()
    {
        Log.Info("AnimationComplete()");
        if (Event_AnimComplete != null)
        {
            Event_AnimComplete(gameObject);
        }
    }
}

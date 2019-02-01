using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillController : MonoBehaviour
{
    public Transform left;
    public Transform right;

    public Element skillID;
    public bool isChoosen;
    public Image item;

    public ImgShieldController imgShieldController;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.zero, 20 * Time.deltaTime);
    }

    #region Event Listener
    private void OnEnable()
    {
        EventListener();
    }

    private void OnDisable()
    {
        StopEventListener();
    }

    void EventListener()
    {
        ElementSwitchPanel.Event_GetChoosenSkill += Event_GetChoosenSkill_Handler;
    }

    void StopEventListener()
    {
        ElementSwitchPanel.Event_GetChoosenSkill -= Event_GetChoosenSkill_Handler;
    }
    #endregion

    #region Event Handler
    private void Event_GetChoosenSkill_Handler(GameObject sender, int choosenSkillID)
    {
        if ((int)skillID == choosenSkillID)
        {
            Log.Info("Event_GetChoosenSkill_Handler() " + gameObject);
            isChoosen = true;
        }
        else
        {
            isChoosen = false;
        }

    }
    #endregion
}

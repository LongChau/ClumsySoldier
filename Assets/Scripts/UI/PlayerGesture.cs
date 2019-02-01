using UnityEngine;
using UnityEngine.EventSystems;

public enum DrawAction
{
    None = 0,
    DrawCircle,
    DrawDash,
    Invalid
}

/// <summary>
/// Control canvas, UI, and input
/// </summary>
public class PlayerGesture : MonoBehaviour
{
    public float DashDuration = 0.5f;

    public Camera mainCamera;
    public Player player;
    public float angleThreshold = 5;
    public float moveThreshold = 100;

    private Vector2 playerPos;
    private Vector2 originalPos;
    private Vector2 originalDir;
    private Vector2 lastDir;
    private float dragTime;

    private float startAngle;
    private float stopAngle;
    private DrawAction currentAction;

    private Vector3 lastPlayerPos;

    //// Use this for initialization
    //void Start()
    //{
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void OnDragBegin(BaseEventData eventData)
    {
        //currentAction = DrawAction.None;
        PointerEventData pointerData = eventData as PointerEventData;
        playerPos = mainCamera.WorldToScreenPoint(player.transform.position);
        originalPos = pointerData.position;
        originalDir = originalPos - playerPos;
        lastDir = originalDir;

        // Calculate start angle
        //startAngle = Vector2.SignedAngle(Vector2.right, originalDir);
        //stopAngle = startAngle;
        //Debug.Log("Start angle = " + startAngle);

        dragTime = 0;

        // save last position for dash back
        lastPlayerPos = player.transform.position;
    }

    public void OnDragEnd(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        Vector2 curDir = pointerData.position - playerPos;

        // Do nothing
        //currentAction = DrawAction.None;
        if (dragTime < DashDuration)
        {
            player.ApplyDash(curDir);
        }
        //player.transform.position = lastPlayerPos;
    }

    public void OnDragMoving(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        Vector2 curDir = pointerData.position - playerPos;

        // Calculate angle diff
        //var angleDiff = Vector2.SignedAngle(lastDir, curDir);
        //stopAngle += angleDiff;
        //player.SpendMana(Mathf.Abs(angleDiff) * 0.0015f);

        //var angleError = Mathf.Abs(stopAngle - startAngle);

        dragTime += Time.deltaTime;

        // Calculate movement diff
        //var radiusError = Mathf.Abs(curDir.magnitude - originalDir.magnitude);
        //if (radiusError > moveThreshold)
        //{
            //if (currentAction == DrawAction.None)
            //{
                //currentAction = DrawAction.DrawDash;
                //ApplyDash(curDir);
            //}
            //else if (currentAction == DrawAction.DrawCircle)
            //{
            //    currentAction = DrawAction.Invalid;
            //}
        //}
        //else if(angleError > angleThreshold)
        //{
        //    if (currentAction == DrawAction.None)
        //    {
        //        currentAction = DrawAction.DrawCircle;
        //    }
        //}

        //Debug.Log(currentAction);
        //if (currentAction == DrawAction.DrawCircle)
        //{
        //    lastDir = curDir;
        //    UpdateShieldAngle();
        //}
        //Debug.Log("Angle = " + stopAngle);
        //Debug.Log("radiusError = " + radiusError + "; angle error = " + angleError);
    }
    
    //private void UpdateShieldAngle()
    //{
    //    float start = startAngle;
    //    float stop = stopAngle;

    //    // Prevent reserved direction
    //    if (start > stop)
    //    {
    //        var temp = start;
    //        start = stop;
    //        stop = temp;
    //    }

    //    // Prevent negative value
    //    while (start <= -180)
    //    {
    //        start += 360;
    //        stop += 360;
    //    }

    //    player.StartShieldAngle = start;
    //    player.StopShieldAngle = stop;

    //    //Debug.Log("Start = " + start + "; stop = " + stop);
    //}

    public DrawAction GetCurrentAction()
    {
        return currentAction;
    }
}

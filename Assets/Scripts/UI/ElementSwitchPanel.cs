using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// control the scroll shield the right corner
/// </summary>
//public class PnlScrollRightController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
public class ElementSwitchPanel : MonoBehaviour
{
    private const float RotationStep = 360f / 8;
    private const float SnapThreshold = 5;

    public delegate void GetChoosenSkill(GameObject sender, int choosenSkillID);
    public static event GetChoosenSkill Event_GetChoosenSkill;

    public CanvasScaler canvas;
    public UISkillController[] childrens;
    public ImgShieldController imgShieldController;
    public Element choosenID;

    public Player player;

    public bool isLeftSide;

    private Vector2 originalPos;
    private Vector2 originalDir;
    private Vector2 lastDir;

    private RectTransform centerRect;
    private Vector2 centerPos;

    private float originalAngle;
    private float angleDiff;
    private int itemOffset;
    private float nextAngle;

    private void Start()
    {
        choosenID = Element.None;
        player.ShieldElement = Element.None;
        itemOffset = 0;
        originalAngle = 0;
        nextAngle = 0;
        centerRect = imgShieldController.GetComponent<RectTransform>();
        if (isLeftSide)
        {
            centerPos = new Vector2(
                centerRect.anchoredPosition.x,
                centerRect.anchoredPosition.y);
        }
        else
        {
            centerPos = new Vector2(
                canvas.referenceResolution.x - centerRect.anchoredPosition.x,
                centerRect.anchoredPosition.y);
        }
        //Debug.Log("Center pos = " + centerPos);

        itemOffset = 0;
        //for (int i = 0; i < player.elements.Length; i++)
        //{
        //    if (isLeftSide)
        //    {

        //    }
        //    else
        //    {
        //        items[i + 1].sprite = itemSprites[(int)player.elements[i]];
        //    }
        //}
    }

    // Update is called once per frame
    //void Update()
    //{
    //}

    public void OnBeginDrag(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;

        originalPos = pointerData.position;
        originalDir = originalPos - centerPos;
        lastDir = originalDir;
    }

    public void OnDrag(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        Vector2 curDir = pointerData.position - centerPos;

        angleDiff = Vector2.SignedAngle(originalDir, curDir);
        float currentAngle = originalAngle + angleDiff;
        //Debug.Log("cur Angle = " + curAngle);
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        // Snap to next angle
        AngleCutOff(ref currentAngle);
        UpdateCurrentStep(currentAngle);
        
        if (Mathf.Abs(currentAngle - nextAngle) < SnapThreshold)
        {
            transform.rotation = Quaternion.Euler(0, 0, nextAngle);
        }
    }

    public void UpdateCurrentStep(float curAngle)
    {
        itemOffset = Mathf.RoundToInt(curAngle / RotationStep);
        nextAngle = itemOffset * RotationStep;
        AngleCutOff(ref nextAngle);

        if (itemOffset >= 8)
        {
            itemOffset -= 8;
        }

        if (itemOffset < 0)
        {
            itemOffset += 8;
        }

        var selectedId = itemOffset;
        if (selectedId >= 0) // && selectedId < player.elements.Length)
        {
            choosenID = childrens[selectedId].skillID;
        }
        else
        {
            choosenID = Element.None;
        }
        player.ShieldElement = choosenID;
        
        Debug.Log("Offset = " + itemOffset + "; Selected item " + choosenID);
    }

    private void AngleCutOff(ref float angle)
    {
        while (angle < 0) angle += 360;
        while (angle >= 360) angle -= 360;
    }

    public void OnEndDrag(BaseEventData eventData)
    {
        PointerEventData pointerData = eventData as PointerEventData;
        originalAngle = originalAngle + angleDiff;
        AngleCutOff(ref originalAngle);

        // Snap to next angle
        UpdateCurrentStep(originalAngle);
        transform.rotation = Quaternion.Euler(0, 0, nextAngle);
        originalAngle = nextAngle;
    }
}

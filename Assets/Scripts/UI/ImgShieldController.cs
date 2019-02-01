using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImgShieldController : MonoBehaviour
{
    public GameObject tempCenterChild;

    public GameObject centerChild;
    public ElementSwitchPanel pnlSwitchElement;

    //// Use this for initialization
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (tempCenterChild != centerChild)
        {
            tempCenterChild = centerChild;
            //pnlScrollRight.rotateState = PnlScrollRightController.RotateState.idle;
        }
    }

}

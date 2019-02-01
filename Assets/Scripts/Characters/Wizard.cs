using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wizard : Enemy {

    public Transform magicStartingPoint;

    protected override void LookAt(Vector3 lookingPos)
    {
        LookAt90(lookingPos);  
    }
}

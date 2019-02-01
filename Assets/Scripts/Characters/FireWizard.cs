using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWizard : Wizard {

    public Bullet prefabFireBall;

    protected override void Attack(Actor victim)
    {
        //var fireBall = Instantiate(prefabFireBall, 
        //    magicStartingPoint.position, Quaternion.identity);
        //fireBall.destination = victim.transform.position;
        //fireBall.damage = damage;
    }
}

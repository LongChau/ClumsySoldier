using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightWizard : Wizard {

    public Beam beam;

    protected override void Start()
    {
        base.Start();
        beam.damage = damage;  
    }

    protected override void Attack(Actor victim)
    {
        //beam.transform.position = magicStartingPoint.position;
        //beam.ShootAt(victim);
    }
}

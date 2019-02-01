using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy {

    protected override void Attack(Actor victim)
    {
        victim.TakeDamage(damage);
        victim.TakeDamage(damage);
    }
}

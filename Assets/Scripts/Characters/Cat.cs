using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Enemy {

    protected override void Attack(Actor victim)
    {
        victim.TakeDamage(damage);
        victim.TakeDamage(damage);
        victim.TakeDamage(damage);
    }
}

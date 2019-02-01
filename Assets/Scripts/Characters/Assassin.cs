using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Enemy {

    protected override void Attack(Actor victim)
    {
        victim.TakeDamage(damage);
        victim.TakeDamage(damage);
        victim.TakeDamage(damage);
        victim.TakeDamage(damage);
        victim.TakeDamage(damage);
    }
}

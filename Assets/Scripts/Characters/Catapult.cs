using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : Enemy {

    public Bullet prefabStone;
    public Animator anim;
    public Transform firingPos;

    private Actor victim;

    protected override void Update()
    {
        UpdateAttack();
    }

    protected override void Attack(Actor victim)
    {
        this.victim = victim;
        anim.SetTrigger("Shoot");
    }

    public void OnShoot()
    {
        var stone = Instantiate(prefabStone, firingPos.position, transform.rotation);
        stone.destination = victim.transform.position;
        stone.damage = damage;
    }
}

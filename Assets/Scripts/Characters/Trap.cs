using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Enemy {

    public Element element;

    protected override void Update()
    {
        UpdateAttack();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Color color = Color.white;
        switch (element)
        {
            case Element.Ice:
                color = Color.cyan;
                break;
            case Element.Electric:
                color = Color.yellow;
                break;
            case Element.Fire:
                color = Color.red;
                break;
        }
        color.a = 0.5f;
        Gizmos.color = color;

        Gizmos.DrawCube(transform.position, transform.lossyScale);
    }

    protected override void Attack(Actor victim)
    {
        victim.TakeDamage(damage);
    }

    public override void TakeDamage(float damage)
    {
        // Invicible
    }
}

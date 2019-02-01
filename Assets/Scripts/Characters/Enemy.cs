using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Actor {

    public Player player;
    public float range;
    public int damagePercent;
    public float coolDownDuration;

    private float coolDown;
    protected float damage;

    // Use this for initialization
    protected override void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        damage = damagePercent / 100f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        LookAt(player.transform.position);

        UpdateAttack();
    }

    protected void UpdateAttack()
    {
        if (coolDown <= 0)
        {
            float distance = Vector2.Distance(
                player.transform.position, transform.position);

            if (distance < range)
            {
                Attack(player);
                coolDown = coolDownDuration;
            }
        }
        else
        {
            coolDown -= Time.deltaTime;
        }
    }

    protected virtual void Reset()
    {
        var playerGO = GameObject.FindGameObjectWithTag("Player");
        player = playerGO.GetComponent<Player>();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

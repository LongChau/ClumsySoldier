using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 1f;
    public float aoeRadius = 1f;

    public Vector3 destination;
    public float damage;

    public Transform explosion;

    //// Use this for initialization
    //void Start () {

    //}

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, destination);
        if (distance > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position, destination, speed * Time.deltaTime);
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        Collider2D playerCol = Physics2D.OverlapCircle(transform.position, aoeRadius,
            LayerMask.GetMask("Player"));
        if (playerCol != null)
        {
            var player = playerCol.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(damage);
        }
        Destroy(gameObject);

        Transform exp = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(exp.gameObject, 0.5f);
    }
}

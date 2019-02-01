using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Control sword 
/// </summary>
public class SwordContactController : MonoBehaviour
{
    public Enemy enemyContact;
    public enum SwordState
    {
        fire,
        fireMagic,
        defend
    }
    public SwordState swordState;

    [Header("GD config here: ")]
    public float damage;

    bool canHit;    // hit once per enable
    
    // Use this for initialization
    void Start()
    {
        //Invoke("HideWithTime", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        
    }

    private void FixedUpdate()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag(GlobalVar.EnemyTag))
    //    {
    //        Log.Info("OnTriggerEnter2D() " + "Meet enemy");

    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag(GlobalVar.EnemyTag))
    //    {
    //        Log.Info("OnTriggerExit2D() " + "Meet enemy");
    //        Actor _actor = collision.GetComponent<Actor>();
    //        _actor.TakeDamage(damage);
    //    }
    //}

    private void OnEnable()
    {
        Invoke("HideWithTime", 0.75f);
        canHit = true;
        if (enemyContact)
        {
            MakeDame(enemyContact, damage);
        }
    }

    private void OnDisable()
    {
        canHit = false;
    }

    // check sword state
    void CheckState()
    {
        switch (swordState)
        {
            case SwordState.fire:
                break;
            case SwordState.fireMagic:
                break;
            case SwordState.defend:
                break;
            default:
                break;
        }
    }

    void HideWithTime()
    {
        Log.Info("HideWithTime()");
        gameObject.SetActive(false);
    }

    public void MakeDame(Enemy enemy, float dame)
    {
        enemy.TakeDamage(dame);
    }
}

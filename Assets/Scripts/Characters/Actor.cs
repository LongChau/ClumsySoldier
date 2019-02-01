using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LookingDirection
{
    Up = 0,
    Right,
    Down,
    Left
}

[SelectionBase]
public abstract class Actor : MonoBehaviour {

    public GameObject sprite;

    public int totalHP = 1000;

    protected int hp;
    protected bool isAlive = true;
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
    }

    protected virtual void Start()
    {
        hp = totalHP;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isAlive)
        {
            hp -= (int)(damage * totalHP);

            if (hp <= 0)
            {
                Die();

                if (gameObject.CompareTag(GlobalVar.PlayerTag))
                {
                    GameSystem.instance.isLoose = true;
                    // go to scene gameover
                    SceneManager.LoadScene(GlobalVar.ResultScene);
                    GameSystem.instance.lastLevel = SceneManager.GetActiveScene().name;
                }
            }
        }
    }

    protected virtual void Die()
    {
        Log.Info("Actor die: " + gameObject.name);
        isAlive = false;
        gameObject.SetActive(false);
    }

    protected LookingDirection CalcLookingDirection2(Vector3 dest)
    {
        Vector3 offset = dest - transform.position;
        if (offset.x > 0)
        {
            return LookingDirection.Right;
        }
        return LookingDirection.Left;
    }

    protected LookingDirection CalcLookingDirection(Vector3 dest)
    {
        Vector3 offset = dest - transform.position;
        if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y))
        {
            if (offset.x > 0)
            {
                return LookingDirection.Right;
            }
            return LookingDirection.Left;
        }
        else
        {
            if (offset.y > 0)
            {
                return LookingDirection.Up;
            }
            return LookingDirection.Down;
        }
    }

    protected virtual void LookAt(Vector3 lookingPos)
    {
        LookAtFree(lookingPos);
    }

    protected virtual void Attack(Actor victim)
    {
        // Do nothing
    }

    protected void LookAtFree(Vector3 lookingPos)
    {
        if (Vector3.Distance(lookingPos, transform.position) > 0.1f)
        {
            transform.LookAt(lookingPos);
            transform.rotation = transform.rotation * Quaternion.Euler(0, -90, 0);
        }
    }

    protected void LookAt90(Vector3 lookingPos)
    {
        if (sprite == null)
        {
            return;
        }

        LookingDirection look = CalcLookingDirection2(lookingPos);

        switch (look)
        {
            case LookingDirection.Left:
                sprite.transform.localScale = new Vector3(1, 1, 1);
                break;

            case LookingDirection.Right:
                sprite.transform.localScale = new Vector3(-1, 1, 1);
                break;

            case LookingDirection.Up:
                break;

            case LookingDirection.Down:
                break;
        }
    }
}

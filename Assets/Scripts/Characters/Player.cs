using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Player : Actor
{
    public delegate void PlayerReachEnd(GameObject sender);
    public static event PlayerReachEnd Event_PlayerReachEnd;

    public enum PlayerState
    {
        walk,
        attack
    }

    private const float DashDuration = 0.5f;

    public PlayerState playerState;

    [Header("Boolean animation values: ")]
    public bool isWalk;
    public bool isAttack;

    [Space]

    public Animator anim;

    public float rangeFire;
    public SwordContactController swordControl;
    private int enemyNum;
    private int enemyCount;
    public GameObject[] enemies;
    public ShieldController.ShieldState shieldState;
    public PlayerGesture playerGesture;

    public GameObject shield;
    public Renderer shieldRender;
    public Path path;
    public float movementSpeed = 0.8f;
    public float dashRange;

    private float startShieldAngle;
    private float stopShieldAngle;
    private int nextWP;
    private float dashTimer;

    private float originalSpeed;
    private Vector3 originalPos;
    public AudioSource sfxDash;
    public AudioSource sfxDie;

    public Texture[] shieldTex;
    public Element ShieldElement
    {
        get
        {
            return selectedShieldElement;
        }

        set
        {
            selectedShieldElement = value;
            Debug.Log("Selected shield: " + value);
            int idx = (int)value;
            if (idx >= 0)
            {
                shieldRender.gameObject.SetActive(true);
                shieldRender.material.mainTexture = shieldTex[idx];
            }
            else
            {
                shieldRender.gameObject.SetActive(false);
            }
        }
    }

    private Element selectedShieldElement;

    public int totalMana;
    public int mana;

    //public float StartShieldAngle
    //{
    //    get
    //    {
    //        return startShieldAngle;
    //    }

    //    set
    //    {
    //        startShieldAngle = value;
    //        float startRad = startShieldAngle * Mathf.Deg2Rad;
    //        shieldRender.material.SetFloat("_StartRadian", startRad);
    //    }
    //}

    //public float StopShieldAngle
    //{
    //    get
    //    {
    //        return stopShieldAngle;
    //    }

    //    set
    //    {
    //        stopShieldAngle = value;
    //        float stopRad = stopShieldAngle * Mathf.Deg2Rad;
    //        shieldRender.material.SetFloat("_StopRadian", stopRad);
    //    }
    //}

    public float HPScale
    {
        get
        {
            return (float)hp / totalHP;
        }
    }

    public float ManaScale
    {
        get
        {
            return (float)mana / totalHP;
        }
    }

    // Should start before camera detect original pos
    void Awake()
    {
        transform.position = path.nodes[0].position;
        nextWP = 1;
        //StartShieldAngle = 0;
        //StopShieldAngle = 0;
        shield.gameObject.SetActive(false);
        originalSpeed = movementSpeed;
        enemyCount = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDash();
        UpdateMoving();
        UpdateShieldState();
        CheckEnemy();

        CheckPlayerState();
        UpdateAnim();
    }

    protected override void Die()
    {
        sfxDie.Play();
        isAlive = false;
    }

    private void UpdateDash()
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                movementSpeed = originalSpeed;
                transform.position = originalPos;
            }
        }

        if (dashTimer <= 0)
        {
            movementSpeed = originalSpeed;
        }
    }

    void UpdateAnim()
    {
        anim.SetBool("isWalk", isWalk);
        anim.SetBool("isAttack", isAttack);
    }

    void CheckPlayerState()
    {
        switch (playerState)
        {
            case PlayerState.walk:
                isWalk = true;
                isAttack = false;
                break;
            case PlayerState.attack:
                isWalk = false;
                isAttack = true;
                break;
            default:
                break;
        }
    }

    private void CheckEnemy()
    {
        if (enemies == null || enemyNum >= enemyCount)
        {
            return;     // get out, no more enemy
        }

        float distance = Vector2.Distance(
                transform.position, enemies[enemyNum].transform.position);

        if (distance < rangeFire)
        {
            swordControl.enemyContact = enemies[enemyNum].GetComponent<Enemy>();
            if (!swordControl.enemyContact.IsAlive)
            {
                enemyNum++;
            }
        }

    }

    private void UpdateMoving()
    {
        var destination = path.nodes[nextWP].position;
        var direction = destination - transform.position;

        Debug.DrawRay(transform.position, direction.normalized * 1f);
    
        // Check forward hit
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f,
                LayerMask.GetMask("Enemy"));
        if (direction.magnitude > 0.1f && hit.collider != null)
        {
            Trap trap = hit.collider.GetComponent<Trap>();
            if (trap == null || trap.element != ShieldElement)
            {
                return;
            }
        }

        // Update next waypoint
        if (transform.position == destination)
        {
            if (nextWP < path.nodes.Length - 1)
            {
                nextWP++;
            }
            else
            {
                // check condition
                if (hp < totalHP/2)     // loose condition
                {
                    GameSystem.instance.isLoose50 = true;
                }
                else    // win condition
                {
                    GameSystem.instance.isWin = true;
                }

                // finish
                if (Event_PlayerReachEnd != null)
                {
                    Event_PlayerReachEnd(gameObject);
                }

                //TODO: go to scene gameover or win
                SceneManager.LoadScene(GlobalVar.ResultScene);
                GameSystem.instance.lastLevel = SceneManager.GetActiveScene().name;
            }
        }

        // Move to next waypoint
        transform.position = Vector3.MoveTowards(
            transform.position, destination, movementSpeed * Time.deltaTime);

        LookAt(destination);
    }

    private void Reset()
    {
        var tr = transform.Find("Shield");
        shieldRender = tr.GetComponent<Renderer>();
        var go = GameObject.FindGameObjectWithTag("Path");
        path = go.GetComponent<Path>();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    if (destination != null)
    //    {
    //        Gizmos.DrawCube(destination.position, Vector3.one * .5f);
    //        Gizmos.DrawLine(transform.position, destination.position);
    //    }
    //}

    protected override void LookAt(Vector3 lookingPos)
    {
        LookAt90(lookingPos);
    }

/// <summary>
    /// update shield state
    /// </summary>
    void UpdateShieldState()
    {
        //if (canvasController.isDrawCircle)
        //{
        //    shieldState = ShieldController.ShieldState.show;
        //    shield.SetActive(true);
            
        //}
        //else
        //{
        //    shieldState = ShieldController.ShieldState.hide;
        //    shield.SetActive(false);
        //}
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Log.Info("TakeDamage: " + damage);
        CheckShield();
    }

    /// <summary>
    /// Do this player active shield?
    /// </summary>
    void CheckShield()
    {
        if (shieldState == ShieldController.ShieldState.show)
        {
            shieldState = ShieldController.ShieldState.hide;
            shield.SetActive(false);
            //canvasController.isDrawCircle = false;
        }
    }

    public void SpendMana(float mana)
    {

    }

    public void ApplyDash(Vector3 curDir)
    {
        Debug.Log("Apply dash " + curDir);
        sfxDash.Play();
        originalPos = transform.position;
        //originalSpeed = movementSpeed;
        Vector3 playerPos = transform.position;
        Vector3 dashUp = new Vector3(0, 1, 0);
        Vector3 dashDown = new Vector3(0, -1, 0);
        Vector3 dashRight = new Vector3(1, 0, 0);
        Vector3 dashLeft = new Vector3(-1, 0, 0);

        //if (curDir.x > 0 && (-100 <= curDir.y && curDir.y <= 100))   // right
        //{
        //    playerPos = playerPos + dashRight;
        //    player.transform.position = playerPos;
        //}
        //else if (curDir.x < 0 && (-100 <= curDir.y && curDir.y <= 100))    // left
        //{
        //    playerPos = playerPos + dashLeft;
        //    player.transform.position = playerPos;
        //}
        //else if (curDir.y < 0 && (-100 <= curDir.x && curDir.x <= 100))    // down
        //{
        //    playerPos = playerPos + dashDown;
        //    player.transform.position = playerPos;
        //}
        //else if (curDir.y > 0 && (-100 <= curDir.x && curDir.x <= 100))    // up
        //{
        //    playerPos = playerPos + dashUp;
        //    player.transform.position = playerPos;
        //}

        if (Mathf.Abs(curDir.x) > Mathf.Abs(curDir.y))
        {
            if (curDir.x < 0)
            {
                // jump left
                playerPos += dashLeft * dashRange;
            }
            else
            {
                // jump right
                playerPos += dashRight * dashRange;
            }
        }
        else
        {
            if (curDir.y < 0)
            {
                // jump down
                playerPos += dashDown * dashRange;
            }
            else
            {
                // jump up
                playerPos += dashUp * dashRange;
            }
        }
        transform.position = playerPos;
        dashTimer = DashDuration;
        movementSpeed = 0;
    }
}

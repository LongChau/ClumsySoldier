using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    public float lifeTime = 0.5f;
    [System.NonSerialized]
    public float damage;
    public float smoothDamp = 0.1f;

    private Vector3 originalScale;
    private float counter;
    private float desiredScale;
    private float curScale;
    private float vel;

    private Actor target;
    private bool hitShield;

    private void Start()
    {
        gameObject.SetActive(false);
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            if (counter <= 0)
            {
                gameObject.SetActive(false);
                if (hitShield)
                {
                    //Player player = target as Player;
                    //.StartShieldAngle = 0;
                    //player.StopShieldAngle = 0;
                    //Debug.Log("Shield hit");
                }
                else
                {
                    target.TakeDamage(damage);
                }
            }
        }

        UpdateShooting();
    }

    public void CheckHitShield()
    {
        float incomingAngle = Vector2.SignedAngle(
             target.transform.right,
             -transform.right);

        Player player = target as Player;
        //if (incomingAngle < player.StartShieldAngle)
        //    incomingAngle += 360;

        //Debug.Log("Incomming angle = " + incomingAngle
        //    + "; start:" + player.StartShieldAngle +
        //    "; stop:" + player.StopShieldAngle);

        //if (incomingAngle > player.StartShieldAngle
        //    && incomingAngle < player.StopShieldAngle)
        //{
        //    hitShield = true;
        //}
        //else
        //{
        //    hitShield = false;
        //}
        hitShield = player.shield.gameObject.activeSelf;
    }

    public void ShootAt(Actor victim)
    {
        target = victim;

        // Scale beam to zero
        curScale = 0f;
        transform.localScale = new Vector3(originalScale.x * curScale,
            originalScale.y, originalScale.z);

        // Show the beam
        gameObject.SetActive(true);
        counter = lifeTime;
    }

    private void UpdateShooting()
    {
        // Look at target
        transform.LookAt(target.transform.position);
        transform.rotation *= Quaternion.Euler(0, -90, 0);

        CheckHitShield();

        // Scale the beam
        Vector3 offset = target.transform.position - transform.position;
        desiredScale = offset.magnitude;
        if (hitShield)
        {
            desiredScale -= 3f;
        }
        curScale = Mathf.SmoothDamp(curScale, desiredScale, ref vel, smoothDamp,
            float.MaxValue, Time.deltaTime);
        transform.localScale = new Vector3(originalScale.x * curScale,
            originalScale.y, originalScale.z);
    }
}

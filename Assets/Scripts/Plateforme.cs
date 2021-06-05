using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme : MonoBehaviour
{
    public float swapDuration;
    private float swapTimer;
    public bool playerIsOn;
    public bool isSwaped;
    private PlatformEffector2D platformEffector;

    private void Awake()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (isSwaped)
        {
            if (swapTimer <= 0)
            {
                isSwaped = false;
                if (platformEffector != null)
                {
                    platformEffector.rotationalOffset = 0;
                }
            }
            else
            {
                swapTimer -= Time.deltaTime;
            }
        }

        if (Input.GetKeyDown("s") && playerIsOn)
        {
            swapCollider();
        }
    }

    public void swapCollider()
    {
        swapTimer = swapDuration;
        isSwaped = true;
        if (platformEffector != null)
        {
            platformEffector.rotationalOffset = 180;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player"))
        {
            playerIsOn = true;
        }
        else
        {
            playerIsOn = false;
        }
    }
}

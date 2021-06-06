using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxVel;

    [SerializeField] private float groundDist = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector2 input;

    private SoundPlayer sP;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sP = GetComponent<SoundPlayer>();
    }

    private void Update()
    {
        input = Vector3.right * Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && OnGrounded())
        {
            Impulse(Vector3.up, jumpForce);
        }
        if (Input.GetKeyDown("r") && OnGrounded())
        {
            recallPotatoes();
        }
    }
    private void FixedUpdate()
    {
        transform.Translate(input * speed * Time.deltaTime);
        rb.velocity = Mathf.Clamp(rb.velocity.y, -maxVel, maxVel) * Vector3.up + Mathf.Clamp(rb.velocity.x, -maxVel, maxVel) * Vector3.right;
    }

    public void Impulse(Vector3 dir, float amount)
    {
        rb.AddForce(dir.normalized * amount, ForceMode2D.Impulse);
    }

    public void recallPotatoes()
    {
        sP.PlayRecall();
        Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
        for (int i = 0; i < allProjectiles.Length; i++)
        {
            if(allProjectiles[i].firstPickup && allProjectiles[i].GetMode() == ProjectileMode.Idle)
            {
                TuberGun.current.addPotatoesToAmmos(allProjectiles[i],true);
            }
        }

        GrowTrigger[] growTriggers = FindObjectsOfType<GrowTrigger>();
        for (int i = 0; i < growTriggers.Length; i++)
        {
            growTriggers[i].recallPotatoes();
        }
    }

    public bool OnGrounded()
    {
        return Physics2D.CircleCast(transform.position + GetComponent<CircleCollider2D>().radius * Vector3.down, groundDist, Vector2.zero, 0, groundLayer);
    }

    public Vector3 GetVel()
    {
        return input;
    }
}

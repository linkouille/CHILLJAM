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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = Vector3.right * Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && OnGrounded())
        {
            Impulse(Vector3.up, jumpForce);
        }
        if (Input.GetKeyDown("r"))
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
        Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
        for (int i = 0; i < allProjectiles.Length; i++)
        {
            if(allProjectiles[i].firstPickup && allProjectiles[i].GetMode() != ProjectileMode.Launched && allProjectiles[i].GetMode() != ProjectileMode.Follow)
            {
                TuberGun.current.addPotatoesToAmmos(allProjectiles[i]);
            }
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

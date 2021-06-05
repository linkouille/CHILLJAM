using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileMode mode;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float radius;
    [SerializeField] private float radiusMax;

    [SerializeField] private float groundDist = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        switch (mode)
        {
            case ProjectileMode.Idle:
                break;
            case ProjectileMode.Launched:
                Vector3 normVel = rb.velocity.normalized; 
                transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(normVel.y, normVel.x) * Mathf.Rad2Deg + 90f, Vector3.forward);

                break;
            case ProjectileMode.Follow:

                FollowTarget();

                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (mode)
        {
            case ProjectileMode.Idle:
                break;
            case ProjectileMode.Launched:
                if(collision.collider.tag == "Plateform")
                {
                    SetModeToIdle();
                }
                break;
            default:
                break;
        }
    }

    private void FollowTarget()
    {
        Vector3 dirToTarget = (target.position - transform.position).normalized;

        if (Vector3.Distance(transform.position,target.position) > radius)
        {// Not near Enough
            transform.Translate(dirToTarget * speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, target.position) > radiusMax)
        {// Too far
            TeleportToTarget();
        }
        rb.gravityScale = transform.position.y - (target.position.y - 0.1f) < 0 ? 0 : 0.25f;
    }

    private void TeleportToTarget()
    {
        transform.position = target.position + Vector3.up * Random.Range(-radius, radius) + Vector3.right * Random.Range(-radius, radius);
    }

    public ProjectileMode GetMode()
    {
        return mode;
    }

    public void ChangeMode(ProjectileMode nm)
    {
        mode = nm;
    }

    public void SetModeToIdle()
    {
        mode = ProjectileMode.Idle;
        transform.rotation = Quaternion.identity;
        rb.freezeRotation = true;
    }

    public void SetModeToLaunched()
    {
        mode = ProjectileMode.Launched;
        rb.freezeRotation = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        rb.gravityScale = 1;
    }

    public void SetModeToFollow(Transform target)
    {
        mode = ProjectileMode.Follow;
        rb.freezeRotation = true;
        gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
        SetTarget(target);
        rb.gravityScale = 0.25f;

    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    public bool OnGrounded()
    {
        return Physics2D.CircleCast(transform.position + GetComponent<Collider2D>().bounds.extents.y * Vector3.down, groundDist, Vector2.zero, 0, groundLayer);
    }
}

public enum ProjectileMode
{
    Idle,
    Launched,
    Follow
}

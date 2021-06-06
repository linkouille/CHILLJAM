using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileMode mode;
    [SerializeField] private Transform target;
    [SerializeField] public Transform follower;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float radius;
    [SerializeField] private float radiusMax;
    [SerializeField] private float groundDist = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    public bool firstPickup;

    private Rigidbody2D rb;
    private SoundProjectile sP;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sP = GetComponent<SoundProjectile>();
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
        firstPickup = true;
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
        transform.position = Vector3.Slerp(transform.position,target.position + Vector3.up * Random.Range(-radius, radius) + Vector3.right * Random.Range(-radius, radius),0.1f);
    }

    private IEnumerator RandomIdleSound()
    {
        while(mode == ProjectileMode.Follow)
        {
            yield return new WaitForSeconds(Random.Range(2, 10));
            sP.PlayIdle();
        }
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
        sP.PlaySound(ProjectileSound.Cri);
        mode = ProjectileMode.Launched;
        rb.freezeRotation = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        rb.gravityScale = 1;
    }

    public void SetModeToFollow(Transform target)
    {
        sP.PlaySound(ProjectileSound.Recup);
        mode = ProjectileMode.Follow;
        rb.freezeRotation = true;
        gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
        SetTarget(target);
        transform.rotation = Quaternion.identity;
        transform.position = target.position;
        rb.gravityScale = 0.25f;
        StartCoroutine(RandomIdleSound());
    }

    public void SetModeToPlanted()
    {
        sP.PlaySound(ProjectileSound.Planter);
        mode = ProjectileMode.Planted;
        rb.freezeRotation = true;
        rb.velocity = new Vector2(0,0);
        gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public bool OnGrounded()
    {
        return Physics2D.CircleCast(transform.position + GetComponent<Collider2D>().bounds.extents.y * Vector3.down, groundDist, Vector2.zero, 0, groundLayer);
    }

    public Vector3 GetVel()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) > radius)
            return (target.position - transform.position).normalized;
        return Vector3.zero;
    }
}

public enum ProjectileMode
{
    Idle,
    Launched,
    Follow, 
    Planted
}

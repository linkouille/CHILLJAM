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

    private Rigidbody2D rb;
    private float randFact;
    private Vector3 wanderingPos;

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

        if (Vector3.Distance(transform.position,target.position) > radius + randFact)
        {// Not near Enough
            transform.Translate(dirToTarget * speed * Time.deltaTime);

            rb.gravityScale = (target.GetComponent<PlayerMovement>() && target.GetComponent<PlayerMovement>().OnGrounded()) ? 1 : 0;

            if (Mathf.Abs(transform.position.x - target.position.x) < radius && Mathf.Abs(transform.position.y - target.position.y) > radius)
            {
                rb.gravityScale = 0;
            }
        }
        else
        {// Near enough
            if(Vector3.Distance(transform.position, wanderingPos) > 0.1f)
            {// Wandering in the radius
                Vector3 dirWandering = (wanderingPos - transform.position).normalized;
                transform.Translate(dirWandering * speed/2 * Time.deltaTime);
            }
            else { // Choose a new Wandering objective point            
                if(rb.gravityScale > 0)
                {
                    wanderingPos = transform.position + Vector3.right * Random.Range(-radius, radius);
                }
                else
                {
                    wanderingPos = transform.position + Vector3.right * Random.Range(-radius, radius) + Vector3.up * Random.Range(-radius, radius);
                }
            }
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
        this.target = target;
        this.randFact = Random.Range(-radius / 2, radius / 2);

    }

}

public enum ProjectileMode
{
    Idle,
    Launched,
    Follow
}

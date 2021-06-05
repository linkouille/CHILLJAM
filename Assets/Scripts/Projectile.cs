using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private ProjectileMode mode;
    [SerializeField] private Transform target;

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
    }

    public void SetModeToFollow(Transform target)
    {
        mode = ProjectileMode.Follow;
        rb.freezeRotation = false;
        gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");
        this.target = target;

    }

}

public enum ProjectileMode
{
    Idle,
    Launched,
    Follow, 
    Planted
}

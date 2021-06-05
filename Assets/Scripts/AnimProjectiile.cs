using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimProjectiile : MonoBehaviour
{
    private Animator anim;
    private Projectile p;

    private bool setLaunch = false;
    private bool setFollow = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        p = GetComponent<Projectile>();
    }

    private void Update()
    {
        anim.SetBool("Grounded", p.OnGrounded());
        anim.SetFloat("XVelocity", p.GetComponent<Rigidbody2D>().velocity.x);

        if(p.GetMode() == ProjectileMode.Follow && !setFollow)
        {
            anim.SetTrigger("Follow");
            setFollow = true;
            setLaunch = false;
        }else if (p.GetMode() == ProjectileMode.Launched && !setLaunch)
        {
            anim.SetTrigger("Launch");
            setFollow = false;
            setLaunch = true;
        }

    }
}

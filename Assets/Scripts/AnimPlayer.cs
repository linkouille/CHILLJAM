using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{

    [SerializeField] private Animator animG;
    [SerializeField] private SpriteRenderer gunRenderer;

    private Animator anim;
    private PlayerMovement pM;
    private TuberGun tG;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
        tG = GetComponent<TuberGun>();
    }

    private void Update()
    {
        Vector3 dir = tG.GetDir();

        gunRenderer.flipY = dir.x > 0; 

        anim.SetBool("Grounded", pM.OnGrounded());
        anim.SetFloat("XVelocity", pM.GetVel().x);
        anim.SetFloat("YVelocity", pM.GetComponent<Rigidbody2D>().velocity.y);
        if(Input.GetButtonDown("Jump") && pM.OnGrounded())
        {
            anim.SetTrigger("Jump");
        }
        if (Input.GetButton("Fire1") && tG.IsTuberGunLoaded())
        {
            // animG.SetTrigger("Shoot");
            animG.Play("Shoot");
        }
    }

}

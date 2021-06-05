using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{

    [SerializeField] private Animator animG;

    private Animator anim;
    private PlayerMovement pM;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pM = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        anim.SetBool("Grounded", pM.OnGrounded());
        anim.SetFloat("XVelocity", pM.GetVel().x);
        anim.SetFloat("YVelocity", pM.GetComponent<Rigidbody2D>().velocity.y);
        if(Input.GetButtonDown("Jump") && pM.OnGrounded())
        {
            anim.SetTrigger("Jump");
        }
        if (Input.GetButton("Fire1"))
        {
            animG.SetTrigger("Shoot");
        }
    }

}

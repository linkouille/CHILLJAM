using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input = Vector3.right * Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        transform.Translate(input * speed * Time.deltaTime);
    }

    public void Impulse(Vector3 dir, float amount)
    {
        rb.AddForce(dir.normalized * amount, ForceMode2D.Impulse);
    }

}

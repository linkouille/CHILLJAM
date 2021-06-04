using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuberGun : MonoBehaviour
{
    [SerializeField] private Transform cursorTransform;
    [SerializeField] private Transform projectile;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float impulseForce;

    [SerializeField] private Transform pivot;


    private PlayerMovement pM;
    private Vector3 dir;
    private Quaternion toDir;
    private Camera mainCam;

    private void Awake()
    {
        pM = GetComponent<PlayerMovement>();
        mainCam = Camera.main.GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        cursorTransform.position = mouseWorldPos;
        dir = transform.position - mouseWorldPos;
        toDir = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f, Vector3.forward);

        pivot.rotation = toDir;

        Debug.DrawLine(transform.position, mouseWorldPos, Color.red);

        if (Input.GetButtonDown("Jump"))
        {
            pM.Impulse(dir, impulseForce);
            Transform p = Instantiate(projectile,spawnPoint.position,toDir);

            p.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * impulseForce, ForceMode2D.Impulse);
            // Destroy(p.gameObject, 10);
        }
    }
}

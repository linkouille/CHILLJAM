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

    [SerializeField] private List<Transform> amo;

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
        dir = (transform.position - mouseWorldPos).normalized;
        toDir = Quaternion.AngleAxis(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90f, Vector3.forward);

        pivot.rotation = toDir;

        Debug.DrawLine(transform.position, mouseWorldPos, Color.red);

        if (Input.GetButtonDown("Fire1") && amo.Count > 0)
        {
            pM.Impulse(dir, impulseForce);
            Transform p = Instantiate(projectile,spawnPoint.position,toDir);
            p.gameObject.name = amo[0].gameObject.name;
            Destroy(amo[0].gameObject);
            amo.Remove(amo[0]);

            if(amo.Count > 0)
            {
                amo[0].GetComponent<Projectile>().SetTarget(transform);
                amo[0].position = spawnPoint.position;
            }

            p.GetComponent<Projectile>().SetModeToLaunched();
            p.GetComponent<Rigidbody2D>().AddForce(-dir * impulseForce, ForceMode2D.Impulse);
            // Destroy(p.gameObject, 10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Projectile")
        {
            Projectile p = collision.collider.GetComponent<Projectile>();
            if(p.GetMode() == ProjectileMode.Idle)
            {
                Transform target = (amo.Count == 0) ? transform : amo[amo.Count - 1];
                amo.Add(p.transform);
                p.SetModeToFollow(target);
            }
        }
    }
}

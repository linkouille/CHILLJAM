using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AreaSwitcher : MonoBehaviour
{
    public bool oneWayUse;
    private bool isUsed;
    public PolygonCollider2D col1;
    public PolygonCollider2D col2;
    private CinemachineConfiner confiner;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isUsed && collision.tag.Equals("Player"))
        {
            if (confiner.m_BoundingShape2D == col1)
            {
                confiner.m_BoundingShape2D = col2;
            }
            else if (confiner.m_BoundingShape2D == col2)
            {
                confiner.m_BoundingShape2D = col1;
            }

            if (oneWayUse)
            {
                isUsed = true;
            }
        }
    }
}

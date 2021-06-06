using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AreaSwitcher : MonoBehaviour
{
    public bool oneWayUse;
    private bool isUsed;
    public PolygonCollider2D col1;
    public int actualAreaCameraSize;
    public PolygonCollider2D col2;
    public int nextAreaCameraSize;
    private CinemachineConfiner confiner;
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isUsed && collision.tag.Equals("Player"))
        {
            if (confiner.m_BoundingShape2D == col1)
            {
                confiner.m_BoundingShape2D = col2;
                cinemachineVirtualCamera.m_Lens.OrthographicSize = actualAreaCameraSize;
            }
            else if (confiner.m_BoundingShape2D == col2)
            {
                confiner.m_BoundingShape2D = col1;
                cinemachineVirtualCamera.m_Lens.OrthographicSize = nextAreaCameraSize;
            }

            if (oneWayUse)
            {
                isUsed = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetFollow : MonoBehaviour
{
    [Range(0, 2)]
    public float rangeOffset = 0.8f;

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = this.transform.parent.position * rangeOffset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetFollow : MonoBehaviour
{
    [Range(0, 2)]
    public float rangeOffset = 0.8f;
    public float zPos = 0;

    public Vector3 startPos = new Vector3(200, 200, 0);

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = startPos + (this.transform.parent.position - startPos) * rangeOffset;
        pos.z = zPos;
        this.transform.position = pos; 
    }
}

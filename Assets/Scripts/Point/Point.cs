using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector3 GetPosition() => transform.position;
    public Vector3 GetSize() => transform.localScale;

    public void LookTarget(Vector3 positionObject)
    {        
        transform.rotation = Quaternion.LookRotation(positionObject);
    }
}

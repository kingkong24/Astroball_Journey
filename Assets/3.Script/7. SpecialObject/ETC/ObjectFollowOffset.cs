using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowOffset : MonoBehaviour
{
    [Header("Å¸°Ù")]
    [SerializeField] Transform transform_target;

    [Header("¼³Á¤")]
    public Vector3 Offset;

    private void Update()
    {
        transform.position = transform_target.position + Offset;
    }
}

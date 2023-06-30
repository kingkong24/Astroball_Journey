using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldForceCameraMovement : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] Transform transform_target;
    [SerializeField] Transform transform_mainCamera;
    [SerializeField] Transform transform_player;

    [Header("¼³Á¤")]
    [SerializeField] float distance = 3.0f;
    [SerializeField] float rotationSpeed = 30.0f;

    private void Awake()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            transform_mainCamera = mainCamera.transform;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform_player = player.transform;
        }
    }

    private void Update()
    {
        Vector3 OffsetDirection = (transform_mainCamera.position - transform_player.position).normalized;
        transform.position = transform_target.position + distance * OffsetDirection;
        transform.LookAt(transform_target);
    }
}
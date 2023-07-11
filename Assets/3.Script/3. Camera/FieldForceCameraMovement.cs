using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldForceCameraMovement : MonoBehaviour
{
    [Header("Transform")]
    [SerializeField] Transform transform_target;
    [SerializeField] Transform transform_mainCamera;
    [SerializeField] Transform transform_player;

    [Header("설정")]
    [SerializeField] float distance = 3.0f;
    [SerializeField] float rotationSpeed = 30.0f;

    [Header("확인용")]
    [SerializeField] PlayerCameraControl cameraControl;

    private void Awake()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if (mainCamera != null)
        {
            transform_mainCamera = mainCamera.transform;
            cameraControl = transform_mainCamera.GetComponent<PlayerCameraControl>();
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform_player = player.transform;
        }
    }

    private void Update()
    {
        Vector3 OffsetDirection = cameraControl.playerBackwardDirection;
        transform.position = transform_target.position + distance * OffsetDirection;
        transform.rotation = transform_mainCamera.rotation;
        // transform.LookAt(transform_target);
    }
}
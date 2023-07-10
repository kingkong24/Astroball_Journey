using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_6Manager : MonoBehaviour
{
    [Header("È®ÀÎ¿ë")]
    [SerializeField] GameObject gameObject_ball;
    [SerializeField] PlayerCameraControl playerCameraControl;

    private void Awake()
    {
        gameObject_ball = GameObject.FindGameObjectWithTag("Ball");
        playerCameraControl = FindObjectOfType<PlayerCameraControl>();
    }

    private void Start()
    {
        Rigidbody rigidbody_ball = gameObject_ball.GetComponent<Rigidbody>();
        rigidbody_ball.useGravity = false;
        playerCameraControl.isUseGravity = true;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_4Manager : MonoBehaviour
{
    [Header("È®ÀÎ¿ë")]
    [SerializeField] GameObject gameObject_ball;

    private void Awake()
    {
        gameObject_ball = GameObject.FindGameObjectWithTag("Ball");
    }

    private void Start()
    {
        Rigidbody rigidbody_ball = gameObject_ball.GetComponent<Rigidbody>();
        rigidbody_ball.useGravity = false;
    }
}

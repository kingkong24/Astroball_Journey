using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    [Header("장의 힘의 크기와 방향")]
    public float gracityForceAmount = 10f;

    [Header("확인용")]
    [SerializeField] Transform transform_ball;
    [SerializeField] Rigidbody rigidbody_ball;
    [SerializeField] bool isPlayerOn;

    private void Awake()
    {
        isPlayerOn = false;
        transform_ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
        rigidbody_ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isPlayerOn && rigidbody_ball != null)
        {
            Vector3 direction = transform.position - transform_ball.position;
            Vector3 force = direction.normalized * gracityForceAmount;
            rigidbody_ball.AddForce(force, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isPlayerOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isPlayerOn = false;
        }
    }
}

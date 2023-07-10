using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("상대 포탈")]
    [Tooltip("Cylinder를 넣을 것")]
    [SerializeField] Transform connectedPortal;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            Quaternion rotation = Quaternion.FromToRotation(transform.forward, connectedPortal.forward);

            Rigidbody ballRigidbody = col.GetComponent<Rigidbody>();

            Vector3 ballDirection = ballRigidbody.velocity.normalized;
            float ballSpeed = ballRigidbody.velocity.magnitude;

            ballDirection = Quaternion.Euler(0f, 180f, 0f) * ballDirection;
            ballDirection = rotation * ballDirection;

            ballRigidbody.isKinematic = true;
            ballRigidbody.isKinematic = false;

            col.transform.position = connectedPortal.position;

            ballRigidbody.velocity = ballSpeed * ballDirection;
        }
    }
}

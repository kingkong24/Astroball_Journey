using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyField : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float DampingFactor;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.rigidbody;
            ballRigidbody.velocity *= DampingFactor;
        }
    }
}

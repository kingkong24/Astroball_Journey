using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyField : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.rigidbody;
            ballRigidbody.velocity *= 0.9f;
        }
    }
}

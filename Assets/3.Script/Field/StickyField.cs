using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyField : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.rigidbody.velocity = Vector3.zero;
        }
    }
}

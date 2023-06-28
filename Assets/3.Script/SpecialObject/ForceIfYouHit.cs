using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceIfYouHit : MonoBehaviour
{
    [Header("힘 크기")]
    [SerializeField] float force;

    [Header("확인용")]
    [SerializeField] new Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            Vector3 forceDirection = col.transform.position - collider.bounds.center;
            forceDirection = forceDirection.normalized;
            col.attachedRigidbody.AddForce(forceDirection * force, ForceMode.Impulse);
        }
    }
}

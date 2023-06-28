using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceIfYouHit : MonoBehaviour
{
    [Header("�� ũ��")]
    [SerializeField] float force;

    [Header("Ȯ�ο�")]
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

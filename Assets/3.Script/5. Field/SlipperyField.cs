using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyField : MonoBehaviour
{
    [Header("설정")]
    [Range(0, 1.0f)]
    [SerializeField] float SlipperyDrag = 0;
    [Range(0, 1.0f)]
    [SerializeField] float SlipperyAngularDrag = 0;


    [Header("확인용")]
    [SerializeField] Rigidbody originalRigidbody;
    [SerializeField] float originalDrag;
    [SerializeField] float originalAngularDrag;

    private void Awake()
    {
        GameObject ballObject = GameObject.FindGameObjectWithTag("Ball");
        if (ballObject != null)
        {
            Rigidbody ballRigidbody = ballObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                originalDrag = ballRigidbody.drag;
                originalAngularDrag = ballRigidbody.angularDrag;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            originalRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            originalRigidbody.drag = SlipperyDrag;
            originalRigidbody.angularDrag = SlipperyAngularDrag;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            originalRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            originalRigidbody.drag = originalDrag;
            originalRigidbody.angularDrag = originalAngularDrag;
        }
    }
}

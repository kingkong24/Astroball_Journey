using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightScrewForceField : MonoBehaviour
{
    [Header("���� ���� ũ��")]
    [SerializeField] float forceAmount;
    [Range(0, 180)]
    [SerializeField] float Angle;

    [Header("Ȯ�ο�")]
    [SerializeField] FieldForceChecker fieldForceChecker;
    [SerializeField] Rigidbody rigidbody_ball;
    [SerializeField] List<GameObject> gameObjects_effect;
    public bool isPlayerOn;


    private void Awake()
    {
        fieldForceChecker = FindObjectOfType<FieldForceChecker>();
        rigidbody_ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        foreach (GameObject gameObject_effect in gameObjects_effect)
        {
            Transform transform_effect = gameObject_effect.transform;
            Rigidbody rigidbody_effect = gameObject_effect.GetComponent<Rigidbody>();
            Vector3 Projection = Vector3.ProjectOnPlane(transform_effect.position - transform.position, transform.up);
            float distance = Projection.magnitude;
            Vector3 direction = Quaternion.AngleAxis(Angle, transform.up) * Projection;
            rigidbody_effect.AddForce(direction * forceAmount / distance, ForceMode.Force);
        }

        if (isPlayerOn && rigidbody_ball != null)
        {
            Vector3 Projection = Vector3.ProjectOnPlane(rigidbody_ball.transform.position - transform.position, transform.up);
            float distance = Projection.magnitude;
            Vector3 direction = Quaternion.AngleAxis(Angle, transform.up) * Projection;
            rigidbody_ball.AddForce(direction * forceAmount / distance, ForceMode.Force);
            rigidbody_ball.AddTorque(transform.up * forceAmount / distance, ForceMode.Force); // ������ ȸ���� ����
            if (isPlayerOn)
            {
                fieldForceChecker.Recalculate();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isPlayerOn = true;
        }

        if (other.CompareTag("FieldForceEffect"))
        {
            gameObjects_effect.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isPlayerOn = false;
        }

        if (other.CompareTag("FieldForceEffect"))
        {
            gameObjects_effect.Remove(other.gameObject);
        }
    }
}

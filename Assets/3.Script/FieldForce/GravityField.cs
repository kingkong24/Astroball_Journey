using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    [Header("장의 힘의 크기와 방향")]
    public float gracityForceAmount = 10f;

    [Header("확인용")]
    [SerializeField] Transform Transform_player;
    [SerializeField] Rigidbody Rigidbody_player;
    [SerializeField] FieldForceChecker fieldForceChecker;
    [SerializeField] bool isPlayerOn;

    private void Awake()
    {
        isPlayerOn = false;
        Rigidbody_player = FindObjectOfType<Rigidbody>();
        fieldForceChecker = FindObjectOfType<FieldForceChecker>();
    }

    private void Update()
    {
        if (isPlayerOn && Rigidbody_player != null)
        {
            Vector3 direction = transform.position - Transform_player.position;
            float distance = direction.magnitude;
            Vector3 force = direction.normalized * gracityForceAmount / (distance * distance);
            Rigidbody_player.AddForce(force, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOn = true;
            fieldForceChecker.Recalculate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOn = false;
            fieldForceChecker.Recalculate();
        }
    }
}

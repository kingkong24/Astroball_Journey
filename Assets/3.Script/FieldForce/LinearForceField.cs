using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearForceField : MonoBehaviour
{
    [Header("장의 힘의 크기와 방향")]
    public float forceAmount = 10f;
    public Vector3 forceDirection = Vector3.forward;

    [Header("확인용")]
    [SerializeField] FieldForceChecker fieldForceChecker;
    [SerializeField] Rigidbody Rigidbody_player;
    public bool isPlayerOn;

    private void Awake()
    {
        isPlayerOn = false;
        fieldForceChecker = FindObjectOfType<FieldForceChecker>();
        Rigidbody_player = FindObjectOfType<Rigidbody>();

    }

    private void Update()
    {
        if (isPlayerOn && Rigidbody_player != null)
        {
            Vector3 force = forceDirection.normalized * forceAmount;
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

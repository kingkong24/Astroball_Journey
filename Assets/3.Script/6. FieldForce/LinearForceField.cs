using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearForceField : MonoBehaviour
{
    [Header("���� ���� ũ��� ����")]
    public float forceAmount = 10f;
    public Vector3 forceDirection = Vector3.forward;

    [Header("ȿ�� ����")]
    [SerializeField] GameObject[] gameObjects_effect;
    [SerializeField] float effectCooltime = 0.5f;

    [Header("Ȯ�ο�")]
    [SerializeField] FieldForceChecker fieldForceChecker;
    [SerializeField] Rigidbody Rigidbody_ball;
    [SerializeField] new Collider collider;
    [SerializeField] int EffectNum;
    [SerializeField] int effectCounter;
    [SerializeField] float activationTimer;
    public bool isPlayerOn;

    private void Awake()
    {
        EffectNum = gameObjects_effect.Length;
        activationTimer = 0;
        effectCounter = 0;
        isPlayerOn = false;
        collider = GetComponent<Collider>();
        fieldForceChecker = FindObjectOfType<FieldForceChecker>();
        Rigidbody_ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        EffectActive();

        if (isPlayerOn && Rigidbody_ball != null)
        {
            Vector3 force = forceDirection.normalized * forceAmount;
            Rigidbody_ball.AddForce(force, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isPlayerOn = true;
            fieldForceChecker.Recalculate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isPlayerOn = false;
            fieldForceChecker.Recalculate();
        }
    }

    /// <summary>
    /// gameObjects_effect�� ForceField�� ��Ȳ�� �°� �����մϴ�.
    /// </summary>
    private void EffectActive()
    {
        if(gameObjects_effect.Length == 0)
        {
            return;
        }

        activationTimer += Time.deltaTime;

        if (activationTimer > effectCooltime)
        {
            GameObject effectObject = gameObjects_effect[effectCounter];
            effectObject.transform.SetPositionAndRotation(GetRandomPosition(), Quaternion.LookRotation(forceDirection));

            effectObject.SetActive(true);

            Rigidbody effectRigidbody = effectObject.GetComponent<Rigidbody>();
            effectRigidbody.velocity = forceDirection * forceAmount;

            effectCounter++;
            if(effectCounter >= EffectNum)
            {
                effectCounter = 0;
            }
            activationTimer = 0;
        }
    }

    /// <summary>
    /// Collider���� Position�� ��ȯ�մϴ�.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPosition()
    {
        Vector3 minPoint = transform.position - collider.bounds.size * 0.5f;
        Vector3 maxPoint = transform.position + collider.bounds.size * 0.5f;

        float randomX = Random.Range(minPoint.x, maxPoint.x);
        float randomY = Random.Range(minPoint.y, maxPoint.y);
        float randomZ = Random.Range(minPoint.z, maxPoint.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
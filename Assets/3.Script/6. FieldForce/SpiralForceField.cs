using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralForceField : MonoBehaviour
{
    [Header("장의 힘의 크기")]
    public float forceAmountUP;
    public float forceAmountRight;

    [Header("Effect")]
    [SerializeField] GameObject[] gameObjects_effect;
    [SerializeField] float effectCooltime;
    [SerializeField] float rotationSpeed;

    [Header("object")]

    [Header("확인용")]
    [SerializeField] new Collider collider;
    [SerializeField] FieldForceChecker fieldForceChecker;
    [SerializeField] Rigidbody rigidbody_ball;
    [Space(5f)]
    [SerializeField] int effectCounter;
    [SerializeField] float activationTimer;
    [Space(5f)]
    [SerializeField] List<GameObject> gameObjects_fieldForceObject;
    [Space(5f)]
    public Vector3 directsionRight;
    [Space(5f)]
    public bool isPlayerOn;



    private void Awake()
    {
        collider = GetComponent<Collider>();
        fieldForceChecker = FindObjectOfType<FieldForceChecker>();
        rigidbody_ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody>();
        activationTimer = 0;

    }
    private void Update()
    {
        EffectActive();

        foreach (GameObject gameObject_effect in gameObjects_effect)
        {
            gameObject_effect.transform.position = gameObject_effect.transform.position + forceAmountUP * Time.deltaTime * transform.up;
            gameObject_effect.transform.RotateAround(transform.position, transform.up, rotationSpeed * Time.deltaTime);
        }

        if (isPlayerOn && rigidbody_ball != null)
        {
            Vector3 Projection = Vector3.ProjectOnPlane(rigidbody_ball.transform.position - transform.position, transform.up);
            float distance = Projection.magnitude;
            Vector3 direction = Quaternion.AngleAxis(135, transform.up) * Projection;
            directsionRight = direction * forceAmountRight / distance;
            rigidbody_ball.AddForce(directsionRight, ForceMode.Force);
            rigidbody_ball.AddForce(transform.up * forceAmountUP, ForceMode.Force);
            fieldForceChecker.Recalculate();
        }

        if(gameObjects_fieldForceObject.Count != 0)
        {
            foreach(GameObject fieldForceObject in gameObjects_fieldForceObject)
            {
                Rigidbody rigidbody = fieldForceObject.GetComponent<Rigidbody>();
                if(rigidbody != null)
                {
                    Vector3 Projection = Vector3.ProjectOnPlane(fieldForceObject.transform.position - transform.position, transform.up);
                    float distance = Projection.magnitude;
                    Vector3 direction = Quaternion.AngleAxis(135, transform.up) * Projection;
                    rigidbody.AddForce(direction * forceAmountRight / distance, ForceMode.Force);
                    rigidbody.AddForce(transform.up * forceAmountUP, ForceMode.Force);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isPlayerOn = true;
        }

        if (other.CompareTag("FieldForceObject"))
        {
            gameObjects_fieldForceObject.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            isPlayerOn = false;
            fieldForceChecker.Recalculate();
        }

        if (other.CompareTag("FieldForceObject"))
        {
            gameObjects_fieldForceObject.Remove(other.gameObject);
        }
    }

    /// <summary>
    /// gameObjects_effect를 ForceField의 상황에 맞게 생성합니다.
    /// </summary>
    private void EffectActive()
    {
        if (gameObjects_effect.Length == 0)
        {
            return;
        }

        activationTimer += Time.deltaTime;

        if (activationTimer > effectCooltime)
        {
            GameObject effectObject = gameObjects_effect[effectCounter];
            effectObject.transform.position = GetRandomPosition();
            effectObject.SetActive(true);
            effectCounter++;
            if (effectCounter >= gameObjects_effect.Length)
            {
                effectCounter = 0;
            }
            activationTimer = 0;
        }
    }

    /// <summary>
    /// Collider안의 Position을 반환합니다.
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

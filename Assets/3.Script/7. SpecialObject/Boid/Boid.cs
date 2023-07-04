using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    [Header("설정")]
    public Transform transform_target;
    [SerializeField] Vector3 minRange;
    [SerializeField] Vector3 maxRange;
    [SerializeField] float speed;
    [Space(5f)]
    public GameObject[] gameObjects_boidUnit;
    public Vector2 unitSpeedRange;
    public float unitDistanceRange;
    [Space(5f)]
    [Range(0, 10)]
    public float cohesionWeight = 1;
    [Range(0, 10)]
    public float alignmentWeight = 1;
    [Range(0, 10)]
    public float separationWeight = 1;
    [Range(0, 10)]
    public float targetWeight = 1;
    [Range(0, 10)]
    public float egoWeight = 1;
    [Range(0, 100)]
    public float boundsWeight = 1;

    [Header("확인용")]
    [SerializeField] Vector3 currentDirection;
    [SerializeField] float timer;


    void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 초기화합니다.
    /// </summary>
    void Initialize()
    {

        for (int i = 0; i < gameObjects_boidUnit.Length; i++)
        {
            Vector3 randomVec = Random.insideUnitSphere;
            randomVec *= unitDistanceRange;
            Quaternion randomRot = Quaternion.Euler(0, Random.Range(0, 360f), 0);
            gameObjects_boidUnit[i].transform.SetLocalPositionAndRotation(randomVec, randomRot);
        }
    }

    private void Update()
    {
        transform_target.position += speed * Time.deltaTime * currentDirection;
        timer += Time.deltaTime;

        if (!IsWithinRange(transform_target.localPosition) && timer > 0.1f)
        {
            currentDirection = GetRandomInsideDirection();
            timer = 0;
        }
    }

    bool IsWithinRange(Vector3 position)
    {
        return position.x >= minRange.x && position.x <= maxRange.x &&
               position.y >= minRange.y && position.y <= maxRange.y &&
               position.z >= minRange.z && position.z <= maxRange.z;
    }

    Vector3 GetRandomInsideDirection()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minRange.x, maxRange.x), Random.Range(minRange.y, maxRange.y), Random.Range(minRange.z, maxRange.z));
        return (randomPosition - transform_target.position).normalized;
    }
}

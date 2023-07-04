using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidUnit : MonoBehaviour
{
    [Header("Boid")]
    public Boid boid;

    [Header("설정")]
    [Range(0, 1)]
    [SerializeField] float lookTargetSpeed;

    [Header("확인용")]
    [SerializeField] Vector3 targetVecter;
    [SerializeField] Vector3 egoVector;
    [SerializeField] float speed;

    #region 초기화
    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 초기화합니다.
    /// </summary>
    void Initialize()
    {
        boid = GetComponentInParent<Boid>();
        speed = Random.Range(boid.unitSpeedRange.x, boid.unitSpeedRange.y);
        StartCoroutine(CalculateEgoVectorCoroutine());
    }
    #endregion

    void Update()
    {
        targetVecter = Vector3.zero;

        Vector3 cohesionVec = CalculateCohesionVector() * boid.cohesionWeight;
        Vector3 alignmentVec = CalculateAlignmentVector() * boid.alignmentWeight;
        Vector3 separationVec = CalculateSeparateVector() * boid.separationWeight;
        Vector3 boundsVec = CalculateBoundVector() * boid.boundsWeight;
        Vector3 targetVec = CalculateTargetVector() * boid.targetWeight;
        Vector3 egoVec = egoVector * boid.egoWeight;

        targetVecter = cohesionVec + alignmentVec + separationVec + boundsVec + targetVec + egoVec;

        targetVecter = Vector3.Lerp(transform.forward, targetVecter, lookTargetSpeed * Time.deltaTime);

        targetVecter = targetVecter.normalized;

        if (targetVecter == Vector3.zero)
        {
            targetVecter = egoVector;
        }

        transform.SetPositionAndRotation(transform.position + speed * Time.deltaTime * targetVecter, Quaternion.LookRotation(targetVecter));
    }

    #region Calculate Vectors
    IEnumerator CalculateEgoVectorCoroutine()
    {
        speed = Random.Range(boid.unitSpeedRange.x, boid.unitSpeedRange.y);
        egoVector = Random.insideUnitSphere;
        yield return new WaitForSeconds(Random.Range(1, 3f));
        StartCoroutine(CalculateEgoVectorCoroutine());
    }

    /// <summary>
    /// BoidUnit들의 중심 벡터 방향을 찾습니다.
    /// </summary>
    /// <returns> BoidUnit들의 중심 벡터 방향 </returns>
    private Vector3 CalculateCohesionVector()
    {
        Vector3 cohesionVec = Vector3.zero;

        for (int i = 0; i < boid.gameObjects_boidUnit.Length; i++)
        {
            cohesionVec += boid.gameObjects_boidUnit[i].transform.position;
        }

        cohesionVec /= boid.gameObjects_boidUnit.Length;
        cohesionVec -= transform.position;
        cohesionVec.Normalize();
        return cohesionVec;
    }

    /// <summary>
    /// BoidUnit들이 향하는 방향을 찾습니다.
    /// </summary>
    /// <returns> BoidUnit들이 향하는 방향 </returns>
    private Vector3 CalculateAlignmentVector()
    {
        Vector3 alignmentVec = transform.forward;

        for (int i = 0; i < boid.gameObjects_boidUnit.Length; i++)
        {
            alignmentVec += boid.gameObjects_boidUnit[i].transform.forward;
        }

        alignmentVec /= boid.gameObjects_boidUnit.Length;
        alignmentVec.Normalize();
        return alignmentVec;
    }

    /// <summary>
    /// BoidUnit들과 멀리 떨어지는 방향을 찾습니다.
    /// </summary>
    /// <returns> BoidUnit들과 멀리 떨어지는 방향 </returns>
    private Vector3 CalculateSeparateVector()
    {
        Vector3 separateVector = Vector3.zero;

            // 이웃들을 피하는 방향으로 이동
            for (int i = 0; i < boid.gameObjects_boidUnit.Length; i++)
            {
                separateVector += (transform.position - boid.gameObjects_boidUnit[i].transform.position);
            }


        separateVector /= boid.gameObjects_boidUnit.Length;
        separateVector.Normalize();
        return separateVector;
    }

    /// <summary>
    /// 타겟을 향하는 벡터를 찾습니다.
    /// </summary>
    /// <returns></returns>
    private Vector3 CalculateTargetVector()
    {
        Vector3 targetVector = boid.transform_target.position - transform.position;
        return targetVector;
    }


    /// <summary>
    /// 거리가 일정 이상이라면 중심으로의 벡터를 리턴합니다.
    /// </summary>
    /// <returns> </returns>
    private Vector3 CalculateBoundVector()
    {
        Vector3 offsetToCenter = boid.transform.position - transform.position;
        return offsetToCenter.magnitude >= boid.unitDistanceRange ? offsetToCenter.normalized : Vector3.zero;
    }
    #endregion
}

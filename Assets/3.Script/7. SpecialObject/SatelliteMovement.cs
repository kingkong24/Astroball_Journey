using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteMovement : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] float distance;
    [SerializeField] float k; // 케플러 상수를 정해주세요.

    [Header("확인용")]
    [SerializeField] Transform transform_planet;
    [SerializeField] Vector3 curlAxis;
    [SerializeField] float speed;

    private void Awake()
    {
        transform_planet = transform.parent;
        curlAxis = Random.insideUnitSphere.normalized;

        Vector3 randomPoint = Random.insideUnitSphere;
        randomPoint = Vector3.ProjectOnPlane(randomPoint, curlAxis).normalized;

        Vector3 initialPosition = transform_planet.position + (randomPoint * distance * transform_planet.localScale.magnitude * 0.65f);
        transform.position = initialPosition;


        float T = Mathf.Sqrt(k * Mathf.Pow(distance, 3));
        speed = 2 * Mathf.PI / T;
    }

    private void Update()
    {
        transform.RotateAround(transform_planet.position, curlAxis, speed * Time.deltaTime);
    }
}

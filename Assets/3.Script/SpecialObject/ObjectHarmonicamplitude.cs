using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHarmonicamplitude : MonoBehaviour
{
    [Header("설정")]
    [Range(0f, 90f)]
    [SerializeField] float amplitude = 90f;
    [SerializeField] float period = 2f;

    [Header("확인용")]
    [SerializeField] Quaternion initialRotation;
    [SerializeField] float elapsedTime;

    private void Start()
    {
        initialRotation = transform.rotation;
        transform.RotateAround(transform.position, transform.forward, amplitude);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float angle = CalculateAngle(elapsedTime);
        RotateObject(angle);
    }


    /// <summary>
    /// Harmonic Oscillation 각도를 계산합니다.
    /// </summary>
    /// <param name="time"> 걸린 시간 </param>
    /// <returns></returns>
    private float CalculateAngle(float time)
    {
        float omega = 2f * Mathf.PI / period;
        float angle = amplitude * Mathf.Cos(omega * time);
        return angle;
    }

    /// <summary>
    /// 물체를 주어진 각도만큼 회전
    /// </summary>
    /// <param name="angle"> 각도 </param>
    private void RotateObject(float angle)
    {
        transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, angle);
    }
}

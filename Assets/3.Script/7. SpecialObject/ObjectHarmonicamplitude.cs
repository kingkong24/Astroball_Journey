using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHarmonicamplitude : MonoBehaviour
{
    [Header("����")]
    [Range(0f, 90f)]
    [SerializeField] float amplitude = 90f;
    [SerializeField] float period = 2f;

    [Header("Ȯ�ο�")]
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
    /// Harmonic Oscillation ������ ����մϴ�.
    /// </summary>
    /// <param name="time"> �ɸ� �ð� </param>
    /// <returns></returns>
    private float CalculateAngle(float time)
    {
        float omega = 2f * Mathf.PI / period;
        float angle = amplitude * Mathf.Cos(omega * time);
        return angle;
    }

    /// <summary>
    /// ��ü�� �־��� ������ŭ ȸ��
    /// </summary>
    /// <param name="angle"> ���� </param>
    private void RotateObject(float angle)
    {
        transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, angle);
    }
}

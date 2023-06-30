using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatlinearMovement : MonoBehaviour
{
    [Header("����")]
    [SerializeField] float speed; // �̵� �ӵ�
    [SerializeField] float period; // �ֱ�
    [SerializeField] bool isRandomRotation;

    [Header("Ȯ�ο�")]
    [SerializeField] float timer;

    private Vector3 startingPosition; // �ʱ� ��ġ

    private void Start()
    {
        if(isRandomRotation)
        {
            float randomAngle = Random.Range(0f, 360f);
            transform.localRotation = Quaternion.Euler(0f, randomAngle, 0f);
        }
        timer = Random.Range(0f, period);
        startingPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float displacement = Mathf.Sin(timer * 2 * Mathf.PI / period) * speed;

        transform.position = startingPosition + new Vector3(displacement, 0, 0);
    }
}

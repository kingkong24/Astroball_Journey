using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatlinearMovement : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] float speed; // 이동 속도
    [SerializeField] float period; // 주기
    [SerializeField] bool isRandomRotation;

    [Header("확인용")]
    [SerializeField] float timer;

    private Vector3 startingPosition; // 초기 위치

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

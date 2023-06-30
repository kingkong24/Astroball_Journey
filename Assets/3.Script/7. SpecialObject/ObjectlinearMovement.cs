using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectlinearMovement : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] float force; // 이동 속도
    [SerializeField] float period; // 주기
    [SerializeField] bool isRandomRotation;

    [Header("확인용")]
    [SerializeField] float timer;
    [SerializeField] new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (isRandomRotation)
        {
            float randomAngle = Random.Range(0f, 360f);
            transform.localRotation = Quaternion.Euler(0f, randomAngle, 0f);
        }
        timer = Random.Range(0f, period);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float displacement = Mathf.Sin(timer * 2 * Mathf.PI / period) * force;
        Vector3 movement = new Vector3(displacement, 0, 0);
        rigidbody.AddForce(movement, ForceMode.VelocityChange);
    }
}

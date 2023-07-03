using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGoUpUnstable : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] float UpSpeed = 1f;
    [SerializeField] float UnstableSpeed = 1f;
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float cooltime;

    [Header("확인용")]
    [SerializeField] Vector3 moveDirection;
    [SerializeField] float timer;

    private void Awake()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooltime)
        {
            timer -= cooltime;
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(-180f, 180f), 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, randomRotation, rotationSpeed * Time.deltaTime);

        }

        Vector3 upMovement = UpSpeed * Time.deltaTime * transform.up;
        Vector3 forwardMovement = UnstableSpeed * Time.deltaTime * transform.forward;
        transform.position += upMovement + forwardMovement;
    }
}

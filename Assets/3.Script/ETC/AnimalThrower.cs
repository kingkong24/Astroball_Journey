using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalThrower : MonoBehaviour
{
    [Header("����")]
    [SerializeField] Rigidbody[] rigidbody_toThrow;
    [SerializeField] Transform throwDirection;
    [SerializeField] float throwForce;
    [SerializeField] float throwDelay;

    [Space(5.0f)]
    [Header("Ȯ�ο�")]
    [SerializeField] int count = 0;
    [SerializeField] float timer = 0;

    private void Start()
    {
        count = 0;
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= throwDelay)
        {
            ThrowObject(count);
            count++;
            timer -= throwDelay;
            if (count >= rigidbody_toThrow.Length)
                count = 0;
        }
    }

    /// <summary>
    /// count��° ������Ʈ�� �����ϴ�.
    /// </summary>
    /// <param name="count"> ��° </param>
    private void ThrowObject(int count)
    {
        if (count >= 0 && count < rigidbody_toThrow.Length)
        {
            rigidbody_toThrow[count].gameObject.SetActive(true);
            rigidbody_toThrow[count].velocity = Vector3.zero;
            rigidbody_toThrow[count].transform.position = transform.position;
            rigidbody_toThrow[count].AddForce((throwDirection.localPosition).normalized * throwForce, ForceMode.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBallRotation : MonoBehaviour
{
    [SerializeField] Vector3 Vector_rotationDir;
    [SerializeField] float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector_rotationDir, rotationSpeed * Time.deltaTime);
    }
}

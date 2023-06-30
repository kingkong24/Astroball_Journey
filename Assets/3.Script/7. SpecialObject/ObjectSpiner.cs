using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpiner : MonoBehaviour
{
    [Header("오브젝트들")]
    [SerializeField] GameObject[] gameObjects;

    [Header("설정")]
    [SerializeField] float rotateSpeed;
    [SerializeField] float distance;
    [Space(5f)]
    [SerializeField] float spinforce;

    private void Awake()
    {
        PlaceObjects();
        SpinObject();
    }


    // Update is called once per frame
    void Update()
    {
        if(gameObjects.Length == 0)
        {
            return;
        }

        foreach(GameObject obj in gameObjects)
        {
            obj.transform.RotateAround(transform.position, transform.forward, rotateSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// 오브젝트들 배치
    /// </summary>
    private void PlaceObjects()
    {
        if (gameObjects.Length == 0)
        {
            return;
        }

        float angle = 360f / gameObjects.Length;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            float radians = (angle * i) * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * distance;
            Vector3 targetPosition = transform.position + offset;
            gameObjects[i].transform.position = targetPosition;
        }
    }

    /// <summary>
    /// 오브젝트들 회전
    /// </summary>
    private void SpinObject()
    {
        foreach (GameObject obj in gameObjects)
        {
            Rigidbody obj_rig = obj.GetComponent<Rigidbody>();
            Vector3 rotationAxis = Random.onUnitSphere;
            obj_rig.AddTorque(rotationAxis * spinforce);
        }
    }
}

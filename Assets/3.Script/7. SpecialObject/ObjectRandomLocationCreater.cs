using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRandomLocationCreater : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] GameObject[] gameObjects_object;
    [SerializeField] float lengthX;
    [SerializeField] float lengthZ;
    [Space(5)]
    [SerializeField] float cooltime;

    [Header("확인용")]
    [SerializeField] int counter;
    [SerializeField] float timer;

    private void Awake()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= cooltime)
        {
            timer -= cooltime;
            gameObjects_object[counter].SetActive(true);
            gameObjects_object[counter].transform.position = RandomPosition();
            counter++;

            if(counter >= gameObjects_object.Length)
            {
                counter = 0;
            }
        }
    }

    Vector3 RandomPosition()
    {
        float randomX = Random.Range(-lengthX, lengthX);
        float randomZ = Random.Range(-lengthZ, lengthZ);
        return new Vector3(randomX, 0, randomZ);
    }


}

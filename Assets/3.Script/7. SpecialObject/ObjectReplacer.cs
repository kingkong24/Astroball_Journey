using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReplacer : MonoBehaviour
{
    [Header("랜덤으로 배치할 오브젝트들")]
    [SerializeField] GameObject[] gameObjects;

    [Header("설정")]
    [SerializeField] Vector3 minVector;
    [SerializeField] Vector3 maxVector;
    [SerializeField] float minDistance;
    [SerializeField] int MaxTryCount;

    [Header("확인용")]
    [SerializeField] Vector3[] points;

    private void Awake()
    {
        PlaceObjects();
    }

    private void PlaceObjects()
    {
        points = new Vector3[gameObjects.Length];

        for (int i = 0; i < gameObjects.Length; i++)
        {
            bool isValidPosition = false;
            int tryCounter = 0;

            while (!isValidPosition && tryCounter < MaxTryCount)
            {
                Vector3 randomPosition = GetRandomPosition();
                isValidPosition = CheckValidPosition(randomPosition, i);

                if (isValidPosition)
                {
                    points[i] = randomPosition;
                    gameObjects[i].transform.localPosition = randomPosition;
                }

                tryCounter++;
            }

            if (!isValidPosition)
            {
                gameObjects[i].SetActive(false);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(minVector.x, maxVector.x), Random.Range(minVector.y, maxVector.y), Random.Range(minVector.z, maxVector.z));
    }

    private bool CheckValidPosition(Vector3 position, int currentIndex)
    {
        for (int i = 0; i < currentIndex; i++)
        {
            if (Vector3.Distance(position, points[i]) < minDistance)
            {
                return false;
            }
        }

        return true;
    }

}

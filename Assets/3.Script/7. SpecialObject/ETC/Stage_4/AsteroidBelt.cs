using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBelt : MonoBehaviour
{
    [Header("¼³Á¤")]
    [SerializeField] GameObject[] gameObjects_StonePrefebs;
    [SerializeField] int stoneNumber;
    [SerializeField] Vector2 distanceRange;
    [SerializeField] Vector2 heightRange;
    [SerializeField] Vector2 scaleRange;
    [Space(5)]
    [SerializeField] float speed;

    private void Awake()
    {
        for (int i = 0; i < stoneNumber; i++)
        {
            GameObject stonePrefab = gameObjects_StonePrefebs[Random.Range(0, gameObjects_StonePrefebs.Length)];
            float distance = Random.Range(distanceRange.x, distanceRange.y);
            float height = Random.Range(heightRange.x, heightRange.y);
            float scale = Random.Range(scaleRange.x, scaleRange.y);
            float angle = Random.Range(0, 2 * Mathf.PI);
            Vector3 position = new Vector3(distance * Mathf.Sin(angle), height, distance * Mathf.Cos(angle));
            GameObject asteroid = Instantiate(stonePrefab, position, Quaternion.identity);
            asteroid.transform.localScale = new Vector3(scale, scale, scale);
            asteroid.transform.SetParent(transform);
        }
    }

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, speed * Time.deltaTime, 0f);
    }
}

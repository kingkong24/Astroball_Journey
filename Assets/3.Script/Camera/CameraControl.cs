using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region

#endregion

public class CameraControl : MonoBehaviour
{
    [Header("'Player' �±װ� ���� ������Ʈ�� ã���ϴ�.")]
    [SerializeField] Transform Transform_player;

    [Space(0.2f)]
    [Header("Target")]
    [SerializeField] Transform Transform_Firsttarget;
    public Transform Transform_target;

    [Space(0.2f)]
    [Header("Planet")]
    [SerializeField] Transform[] Transforms_planet;
    [SerializeField] Transform Transform_closestPlaent;

    [Space(0.2f)]
    [Header("����")]
    [SerializeField] float cameraDistanceAgainstTarget = 5.0f;
    [SerializeField] float cameraDistanceAgainstPlaent = 2.0f;
    [SerializeField] float cameraDistance;
    [SerializeField] float cameraSpeed = 10.0f;

    private void Awake()
    {
        cameraDistance = Mathf.Sqrt(Mathf.Pow(cameraDistanceAgainstTarget, 2) + Mathf.Pow(cameraDistanceAgainstPlaent, 2));
        Transform_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        SetTarget(Transform_Firsttarget);
        FindPlanets();
        FindClosestPlanet();
        Initialise();
    }

    private void LateUpdate()
    {

    }

    /// <summary>
    /// ī�޶��� ��ġ�� �ʱ�ȭ�մϴ�.
    /// </summary>
    public void Initialise()
    {
        Vector3 direction = (Transform_closestPlaent.position - Transform_player.position).normalized;
    }

    /// <summary>
    /// "Planet" �±׸� ���� ��� ������Ʈ�� Transform�� Transforms_planet�� �����մϴ�.
    /// </summary>
    public void FindPlanets()
    {
        GameObject[] planetObjects = GameObject.FindGameObjectsWithTag("Planet");
        Transforms_planet = new Transform[planetObjects.Length];
        for (int i = 0; i < planetObjects.Length; i++)
        {
            Transforms_planet[i] = planetObjects[i].transform;
        }
    }

    /// <summary>
    /// ���� ����� Planet�� Transform�� Transform_closestPlaent�� �����մϴ�.
    /// </summary>
    public void FindClosestPlanet()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestPlanet = null;

        foreach (Transform planet in Transforms_planet)
        {
            float distance = Vector3.Distance(Transform_player.position, planet.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlanet = planet;
            }
        }

        Transform_closestPlaent = closestPlanet;
    }

    /// <summary>
    /// Target�� Transform�� Transform_target���� ���մϴ�.
    /// </summary>
    /// <param name="Transform_target"> ������ Target�� Transform </param>
    public void SetTarget(Transform Transform_target)
    {
        this.Transform_target = Transform_target;
    }
}
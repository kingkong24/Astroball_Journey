using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("'Player' �±װ� ���� ������Ʈ�� ã���ϴ�.")]
    [SerializeField] GameObject GameObject_player;
    [SerializeField] PlayerMovement playerMovement;

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
    [SerializeField] float cameraLookUp = 1.0f;
    [SerializeField] float cameraSpeed = 10.0f;

    [Space(0.2f)]
    [Header("Ȯ�ο�")]
    [SerializeField] bool isFollow = false;
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        GameObject_player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = GameObject_player.GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        CameraInitialise();
    }


    // ��ǲ�ý������� �Ű��� ����.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraInitialise();
        }
    }

    /// <summary>
    /// ī�޶��� ��ġ�� �ʱ�ȭ�մϴ�.
    /// </summary>
    public void CameraInitialise()
    {
        isFollow = false;

        Vector3 directionUpNormal = (GameObject_player.transform.position - playerMovement.Transform_closestPlaent.position).normalized;

        Vector3 directionSide = (GameObject_player.transform.position - playerMovement.Transform_target.position);

        Vector3 projectedVector = (directionSide - Vector3.Project(directionSide, directionUpNormal)).normalized;

        Vector3 resultPosition = GameObject_player.transform.position + directionUpNormal * cameraDistanceAgainstPlaent + projectedVector * cameraDistanceAgainstTarget;

        transform.position = resultPosition;

        transform.LookAt(GameObject_player.transform.position + cameraLookUp * directionUpNormal);
    }

    /// <summary>
    /// player_shot�� �ϸ� Player�� ����ٴϴ� �ڷ�ƾ�� �����մϴ�.
    /// </summary>
    public void CameraFollow()
    {
        isFollow = true;
        offset = transform.position - GameObject_player.transform.position;
        StartCoroutine(Co_CameraFollow());
    }

    IEnumerator Co_CameraFollow()
    {
        while(isFollow)
        {
            transform.position = Vector3.Lerp(transform.position, GameObject_player.transform.position + offset, Time.deltaTime * cameraSpeed);
            yield return null;
        }
    }
}
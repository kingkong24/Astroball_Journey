using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    [Header("'Player' 태그가 붙은 오브젝트를 찾습니다.")]
    [SerializeField] GameObject GameObject_player;

    [Space(0.2f)]
    [Header("Target")]
    [SerializeField] Transform Transform_Firsttarget;
    public Transform Transform_target;

    [Space(0.2f)]
    [Header("Planet")]
    [SerializeField] Transform[] Transforms_planet;
    [SerializeField] Transform Transform_closestPlaent;

    [Space(0.2f)]
    [Header("설정")]
    [SerializeField] float cameraDistanceAgainstTarget = 5.0f;
    [SerializeField] float cameraDistanceAgainstPlaent = 2.0f;
    [SerializeField] float cameraFollowSpeed = 10.0f;
    [SerializeField] float cameraSpinSpeedX = 45.0f;
    [SerializeField] float cameraSpinSpeedY = 30.0f;

    [Space(0.2f)]
    [Header("확인용")]
    [SerializeField] bool isFollow = false;
    [SerializeField] bool isPlayerReady = true;
    public bool isUseGravity = false;
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        GameObject_player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        CameraInitialise();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isPlayerReady)
        {
            CameraInitialise();
        }
    }

    private void LateUpdate()
    {
        if (!isFollow)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            transform.RotateAround(GameObject_player.transform.position, GameObject_player.transform.up, mouseX * cameraSpinSpeedX * Time.deltaTime);

            if (!isUseGravity)
            {
                float rotationX = transform.rotation.eulerAngles.x - GameObject_player.transform.rotation.eulerAngles.x;

                if (rotationX > 180.0f)
                {
                    rotationX -= 360.0f;
                }
                if (mouseY > 0 && rotationX < 0.0f || mouseY < 0 && rotationX > 70.0f)
                {
                    mouseY = 0;
                }

            }
            transform.RotateAround(GameObject_player.transform.position, transform.right, -mouseY * cameraSpinSpeedY * Time.deltaTime);
        }
    }

    #region 상태
    /// <summary>
    /// Ready 상태를 확인합니다.
    /// </summary>
    public void ConfirmReady()
    {
        isPlayerReady = true;
    }

    /// <summary>
    /// !Ready 상태를 확인합니다.
    /// </summary>
    public void ConfirmShoot()
    {
        isPlayerReady = false;
    }
    #endregion


    #region 카메라 움직임
    /// <summary>
    /// 카메라의 위치를 초기화합니다.
    /// </summary>
    public void CameraInitialise()
    {
        isFollow = false;

        Vector3 playerPosition = GameObject_player.transform.position;

        Vector3 playerUpDirection = GameObject_player.transform.up;

        Vector3 playerBackwardDirection = -GameObject_player.transform.forward;

        Vector3 cameraPosition = playerPosition + (playerUpDirection * cameraDistanceAgainstPlaent) + (playerBackwardDirection * cameraDistanceAgainstTarget);

        transform.SetPositionAndRotation(cameraPosition, GameObject_player.transform.rotation);
    }

    /// <summary>
    /// player_shot을 하면 Player를 따라다니는 코루틴을 실행합니다.
    /// </summary>
    public void CameraFollow()
    {
        isFollow = true;
        offset = transform.position - GameObject_player.transform.position;
        StartCoroutine(Co_CameraFollow());
    }

    IEnumerator Co_CameraFollow()
    {
        while (isFollow)
        {
            transform.position = Vector3.Lerp(transform.position, GameObject_player.transform.position + offset, Time.deltaTime * cameraFollowSpeed);
            // CameraInitialise();
            yield return null;
        }
    }

    #endregion
}
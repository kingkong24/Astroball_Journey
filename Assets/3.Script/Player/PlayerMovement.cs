using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("플레이어 오브젝트")]
    [SerializeField] GameObject gameobject_ball;
    [SerializeField] GameObject gameobject_arrow;

    [Space(2f)]
    [Header("Target")]
    public Transform Transform_target;

    [Space(2f)]
    [Header("Planet")]
    [SerializeField] Transform[] Transforms_planet;
    public Transform Transform_closestPlaent;

    [Space(2f)]
    [Header("설정")]
    public float MaxPower = 2.0f;
    public float MinPower = 0.5f;
    [SerializeField] float thresholdSpeed = 0.01f;
    [SerializeField] float waitTime = 0.5f;

    [Space(2f)]
    [Header("화살표 회전")]
    [SerializeField] float rotationSpeedUpDown = 90.0f;
    [SerializeField] float minRotationSpeedLeftRight = 90.0f;
    [SerializeField] float maxRotationSpeedLeftRight = 360.0f;

    [Space(2f)]
    [Header("발사 설정")]
    [SerializeField] float arrowScaleSpeed = 1.0f;
    [SerializeField] float ArrowScaleForceRatio = 10.0f;

    [Space(2f)]
    [Header("확인용")]
    [SerializeField] Rigidbody rigidbody_ball;
    [SerializeField] Quaternion SaveRotation;
    [SerializeField] Vector3 SavePosition;
    [SerializeField] float arrowScale;
    [Space(5f)]
    [SerializeField] bool isReady = true;
    [SerializeField] bool isCharging = false;
    [SerializeField] bool isLaunch = false;
    [SerializeField] bool isArrowLarging = false;
    [SerializeField] bool isGamePause = false;
    [SerializeField] bool isGameClear = false;

    [Space(2f)]
    [Header("이벤트")] 
    public UnityEvent Event_Ready;
    public UnityEvent Event_shot;
    public UnityEvent Event_Respawn;

    private void Awake()
    {
        FindPlanets();
        FindClosestPlanet();
        Initialise();
        rigidbody_ball = gameobject_ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        FollowBall();

        if (isGamePause || isGameClear)
        {
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }

        if (isReady)
        {
            // A키와 D키를 입력받아 왼쪽 또는 오른쪽으로 회전
            if (Input.GetKey(KeyCode.A))
            {
                PlayerGetHorizontal(-1f);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                PlayerGetHorizontal(1f);
            }

            // W키와 S키를 입력받아 왼쪽 또는 오른쪽으로 회전
            if (Input.GetKey(KeyCode.W))
            {
                PlayerGetVertocal(1f);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                PlayerGetVertocal(-1f);
            }

        }

        // 마우스 왼쪽 버튼을 누를 때
        if (Input.GetMouseButtonDown(0) && !isLaunch)
        {
            StartCharging();
        }

        // 마우스 왼쪽 버튼을 떼었을 때
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            SaveTransform();
            Shoot();
        }

        // 화살표 스케일링
        if (isCharging)
        {
            ScaleArrow();
        }
    }


    #region 초기화

    /// <summary>
    /// 각종 변수 초기화
    /// </summary>
    private void Initialise()
    {
        isReady = true;
        isCharging = false;
        isLaunch = false;
        isArrowLarging = false;
        isGamePause = false;
        isGameClear = false;
        SaveTransform();
    }


    /// <summary>
    /// "Planet" 태그를 가진 모든 오브젝트의 Transform을 Transforms_planet에 저장합니다.
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
    /// 가장 가까운 Planet의 Transform을 Transform_closestPlaent에 저장합니다.
    /// </summary>
    public void FindClosestPlanet()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestPlanet = null;

        foreach (Transform planet in Transforms_planet)
        {
            float distance = Vector3.Distance(gameObject.transform.position, planet.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlanet = planet;
            }
        }

        Transform_closestPlaent = closestPlanet;
    }

    /// <summary>
    /// Target의 Transform을 Transform_target으로 정합니다.
    /// </summary>
    /// <param name="Transform_target"> 정해줄 Target의 Transform </param>
    public void SetTarget(Transform Transform_target)
    {
        this.Transform_target = Transform_target;
    }

    #endregion

    #region 플레이어 움직임

    private void FollowBall()
    {
        transform.position = gameobject_ball.transform.position;

        // Player 오브젝트 방향 초기화.;
        FindClosestPlanet();

        Vector3 UpVector = transform.position - Transform_closestPlaent.position;

        Vector3 targetForward = Vector3.ProjectOnPlane(transform.forward, UpVector).normalized;
        Quaternion targetRotationUP = Quaternion.LookRotation(targetForward, UpVector);
        transform.rotation = targetRotationUP;
    }
    /// <summary>
    /// Arrow의 방향을 좌우로 움직입니다.
    /// </summary>
    /// <param name="horizon"> horizon 만큼 </param>
    public void PlayerGetHorizontal(float horizon)
    {
        float distanceToUp = Vector3.Distance(transform.up, Vector3.up);
        float adjustedRotationSpeed = Mathf.Lerp(minRotationSpeedLeftRight, maxRotationSpeedLeftRight, distanceToUp);

        if (transform.up.y > 0f)
        {
            transform.Rotate(transform.up, horizon * adjustedRotationSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Rotate(transform.up, -horizon * adjustedRotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    /// <summary>
    /// Arrow의 방향을 위아래로 움직입니다.
    /// </summary>
    /// <param name="vertical"> vertical 만큼 </param>
    public void PlayerGetVertocal(float vertical)
    {
        float rotationX = gameobject_arrow.transform.localRotation.eulerAngles.x; 
        if (rotationX > 180.0f)
        {
            rotationX -= 360.0f;
        }
        if (vertical > 0 && rotationX < -70.0f ||
            vertical < 0 && rotationX > 70.0f)
        {
            vertical = 0;
        }

        gameobject_arrow.transform.RotateAround(transform.position, -transform.right, vertical * rotationSpeedUpDown * Time.deltaTime);
    }

    /// <summary>
    /// 발사를 위한 충전을 시작합니다
    /// </summary>
    public void StartCharging()
    {
        isReady = false;
        isCharging = true;
    }


    /// <summary>
    /// Player를 발사합니다.
    /// </summary>
    public void Shoot()
    {
        isLaunch = true;

        isCharging = false;
        Vector3 shootDirection = gameobject_arrow.transform.forward;
        float shootPower = gameobject_arrow.transform.localScale.z * ArrowScaleForceRatio;
        rigidbody_ball.AddForce(shootDirection * shootPower, ForceMode.Impulse);
        gameobject_arrow.SetActive(false);

        Event_shot.Invoke();

        StartCoroutine(WaitPlayerReady());
    }

    /// <summary>
    /// Player가 준비될때까지 기다립니다.
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitPlayerReady()
    {
        yield return new WaitForSeconds(0.1f);
        float timer = 0f;
        while (timer < waitTime)
        {
            while (IsObjectSpeedBelowThreshold(rigidbody_ball, thresholdSpeed))
            {
                timer = 0f;
                yield return null;
            }
            timer += Time.deltaTime;
        }

        PlayerInitialise();
        isLaunch = false;
        isReady = true;
        gameobject_arrow.SetActive(true);
        Event_Ready.Invoke();
    }

    /// <summary>
    /// Arrow의 스케일을 오실레이션합니다.
    /// </summary>
    public void ScaleArrow()
    {
        if (isArrowLarging)
        {
            arrowScale = gameobject_arrow.transform.localScale.z + arrowScaleSpeed * Time.deltaTime;
            SetArrow(arrowScale);
            if (arrowScale > MaxPower)
            {
                isArrowLarging = false;
            }
        }
        else
        {
            arrowScale = gameobject_arrow.transform.localScale.z - arrowScaleSpeed * Time.deltaTime;
            SetArrow(arrowScale);
            if (arrowScale < MinPower)
            {
                isArrowLarging = true;
            }
        }
    }

    /// <summary>
    /// Arrow의 Scale을 수정합니다.
    /// </summary>
    /// <param name="arrowScale"></param>
    public void SetArrow(float arrowScale)
    {
        gameobject_arrow.transform.localScale = new Vector3(1.0f, 1.0f, arrowScale);
    }

    /// <summary>
    /// 플레이어의 상태를 준비상태로 초기화합니다.
    /// </summary>
    public void PlayerInitialise()
    {
        // velocity 초기화
        ResetObjectVelocity(rigidbody_ball);

        // Arrow 오브젝트 초기화
        SetArrow(1.0f);
        ArrowInitialise();
    }

    /// <summary>
    /// Arrow를 준비상태로 초기화합니다.
    /// </summary>
    public void ArrowInitialise()
    {
        gameobject_arrow.transform.rotation = transform.rotation;
        gameobject_arrow.transform.localPosition = new Vector3(0, 0, 0.7f);
    }

    /// <summary>
    /// 속도가 임계값보다 낮으면 false 반환
    /// </summary>
    /// <param name="rigidbody"> 속도를 확인할 오브젝트의 rigidbody </param>
    /// <param name="threshold"> 임계값 </param>
    /// <returns></returns>
    public bool IsObjectSpeedBelowThreshold(Rigidbody rigidbody, float threshold)
    {
        if (rigidbody != null)
        {
            float speed = rigidbody.velocity.magnitude;
            if (speed <= threshold)
            {
                return false;
            }
            return true;
        }
        return true;
    }

    #endregion

    #region 플레이어 리스폰
    /// <summary>
    /// 플레이어의 현재 위치와 회전을 저장합니다.
    /// </summary>
    private void SaveTransform()
    {
        SavePosition = gameobject_ball.transform.position;
        SaveRotation = gameobject_ball.transform.rotation;
    }

    /// <summary>
    /// player를 이전 위치로 reset합니다.
    /// </summary>
    public void ResetPlayer()
    {
        gameobject_ball.transform.SetPositionAndRotation(SavePosition, SaveRotation);
        ResetObjectVelocity(rigidbody_ball);

        Event_Respawn.Invoke();
    }




    #endregion

    #region 게임 진행 관련
    /// <summary>
    /// 게임을 일시정지합니다.
    /// </summary>
    public void GamePause()
    {
        isGamePause = true;
    }

    /// <summary>
    /// 게임을 재개합니다.
    /// </summary>
    public void GameResume()
    {
        isGamePause = false;
    }

    public void GameClear()
    {
        isGameClear = true;
    }


    #endregion

    #region ETC
    /// <summary>
    /// 오브젝트의 속도를 0으로 초기화
    /// </summary>
    /// <param name="rigidbody"> 초기화 할 오브젝트의 rigidbody </param>
    public void ResetObjectVelocity(Rigidbody rigidbody)
    {
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
            rigidbody.isKinematic = false;
        }
    }


    #endregion
}

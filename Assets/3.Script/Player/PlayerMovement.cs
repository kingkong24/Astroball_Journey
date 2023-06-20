using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject Arrow;

    [Space(0.2f)]
    [Header("Target")]
    [SerializeField] Transform Transform_Firsttarget; // 게임 메니저 생성시 지울 것.
    public Transform Transform_target;

    [Space(0.2f)]
    [Header("Planet")]
    [SerializeField] Transform[] Transforms_planet;
    public Transform Transform_closestPlaent;

    [Space(0.2f)]
    [Header("설정")]
    public float MaxPower = 2.0f;
    public float MinPower = 0.5f;
    [SerializeField] float threshold = 0.1f;

    [Space(0.2f)]
    [Header("화살표 회전")]
    [SerializeField] float rotationSpeed = 90.0f;

    [Space(0.2f)]
    [Header("발사 설정")]
    [SerializeField] float arrowScaleSpeed = 1.0f;
    [SerializeField] float ArrowScaleForceRatio = 10.0f;

    [Space(0.2f)]
    [Header("확인용")]
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] float arrowScale;
    [SerializeField] bool isReady = true;
    [SerializeField] bool isCharging = false;
    [SerializeField] bool isLaunch = false;
    [SerializeField] bool isArrowLarging = false;

    [Space(0.2f)]
    [Header("이벤트")]
    public UnityEvent Event_Ready;
    public UnityEvent Event_shot;

    private void Awake()
    {
        FindPlanets();
        FindClosestPlanet();
        SetTarget(Transform_Firsttarget);
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        float horizon = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(isReady)
        {
            // a키와 d키를 입력받아 왼쪽 또는 오른쪽으로 회전
            if (horizon != 0)
            {
                PlayerGetHorizontal(horizon);
            }

            // w키와 s키를 입력받아 위로 또는 아래로 회전
            if (vertical != 0)
            {
                PlayerGetVertocal(vertical);
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
    /// <summary>
    /// Arrow의 방향을 좌우로 움직입니다.
    /// </summary>
    /// <param name="horizon"> horizon 만큼 </param>
    public void PlayerGetHorizontal(float horizon)
    {
        transform.Rotate(transform.up, horizon * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Arrow의 방향을 위아래로 움직입니다.
    /// </summary>
    /// <param name="vertical"> vertical 만큼 </param>
    public void PlayerGetVertocal(float vertical)
    {
        float rotationX = Arrow.transform.rotation.eulerAngles.x;
        if(rotationX > 180.0f)
        {
            rotationX -= 360.0f;
        }
        if (vertical > 0 && rotationX < -70.0f ||
            vertical < 0 && rotationX > 70.0f)
        {
            vertical = 0;
        }
        
        Arrow.transform.RotateAround(transform.position, -transform.right, vertical * rotationSpeed * Time.deltaTime);
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
        Vector3 shootDirection = Arrow.transform.forward;
        float shootPower = Arrow.transform.localScale.z * ArrowScaleForceRatio;
        rigidbody.AddForce(shootDirection * shootPower, ForceMode.Impulse);
        Arrow.SetActive(false);

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
        while(IsObjectSpeedBelowThreshold(rigidbody, threshold))
        {
            yield return null;
        }
        PlayerInitialise();
        isLaunch = false;
        isReady = true;
        Arrow.SetActive(true);

        Event_Ready.Invoke();
    }

    /// <summary>
    /// Arrow의 스케일을 오실레이션합니다.
    /// </summary>
    public void ScaleArrow()
    {
        if(isArrowLarging)
        {
            arrowScale = Arrow.transform.localScale.z + arrowScaleSpeed * Time.deltaTime;
            SetArrow(arrowScale);
            if (arrowScale > MaxPower)
            {
                isArrowLarging = false;
            }
        }
        else
        {
            arrowScale = Arrow.transform.localScale.z - arrowScaleSpeed * Time.deltaTime;
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
        Arrow.transform.localScale = new Vector3(1.0f, 1.0f, arrowScale);
    }

    /// <summary>
    /// 플레이어의 상태를 준비상태로 초기화합니다.
    /// </summary>
    public void PlayerInitialise()
    {
        // velocity 초기화
        ResetObjectVelocity(rigidbody);

        // Player 오브젝트와 Ball 오브젝트의 방향 초기화.
        Quaternion ballRotation = Ball.transform.rotation;
        FindClosestPlanet();
        Vector3 UpVector = gameObject.transform.position - Transform_closestPlaent.position;
        Quaternion UPRotation = Quaternion.LookRotation(UpVector, Vector3.up);
        float yRotation = UPRotation.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        Ball.transform.rotation = ballRotation;

        // Arrow 오브젝트 초기화
        SetArrow(1.0f);
        ArrowInitialise();
    }

    /// <summary>
    /// Arrow를 준비상태로 초기화합니다.
    /// </summary>
    public void ArrowInitialise()
    {
        Arrow.transform.rotation = transform.rotation;
        Arrow.transform.localPosition = new Vector3(0, 0, 0.7f);
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

    /// <summary>
    /// 오브젝트의 속도를 0으로 초기화
    /// </summary>
    /// <param name="rigidbody"> 초기화 할 오브젝트의 rigidbody </param>
    public void ResetObjectVelocity(Rigidbody rigidbody)
    {
        if (rigidbody != null)
        {
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;
        }
    }

    #endregion
}

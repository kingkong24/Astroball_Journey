using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("�÷��̾�")]
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject Arrow;

    [Space(0.2f)]
    [Header("Target")]
    [SerializeField] Transform Transform_Firsttarget; // ���� �޴��� ������ ���� ��.
    public Transform Transform_target;

    [Space(0.2f)]
    [Header("Planet")]
    [SerializeField] Transform[] Transforms_planet;
    public Transform Transform_closestPlaent;

    [Space(0.2f)]
    [Header("����")]
    public float MaxPower = 2.0f;
    public float MinPower = 0.5f;
    [SerializeField] float threshold = 0.1f;

    [Space(0.2f)]
    [Header("ȭ��ǥ ȸ��")]
    [SerializeField] float rotationSpeed = 90.0f;

    [Space(0.2f)]
    [Header("�߻� ����")]
    [SerializeField] float arrowScaleSpeed = 1.0f;
    [SerializeField] float ArrowScaleForceRatio = 10.0f;

    [Space(0.2f)]
    [Header("Ȯ�ο�")]
    [SerializeField] new Rigidbody rigidbody;
    [SerializeField] float arrowScale;
    [SerializeField] bool isReady = true;
    [SerializeField] bool isCharging = false;
    [SerializeField] bool isLaunch = false;
    [SerializeField] bool isArrowLarging = false;

    [Space(0.2f)]
    [Header("�̺�Ʈ")]
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
            // aŰ�� dŰ�� �Է¹޾� ���� �Ǵ� ���������� ȸ��
            if (horizon != 0)
            {
                PlayerGetHorizontal(horizon);
            }

            // wŰ�� sŰ�� �Է¹޾� ���� �Ǵ� �Ʒ��� ȸ��
            if (vertical != 0)
            {
                PlayerGetVertocal(vertical);
            }

        }

        // ���콺 ���� ��ư�� ���� ��
        if (Input.GetMouseButtonDown(0) && !isLaunch)
        {
            StartCharging();
        }

        // ���콺 ���� ��ư�� ������ ��
        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            Shoot();
        }

        // ȭ��ǥ �����ϸ�
        if (isCharging)
        {
            ScaleArrow();
        }
    }

    #region �ʱ�ȭ
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
    /// Target�� Transform�� Transform_target���� ���մϴ�.
    /// </summary>
    /// <param name="Transform_target"> ������ Target�� Transform </param>
    public void SetTarget(Transform Transform_target)
    {
        this.Transform_target = Transform_target;
    }

    #endregion

    #region �÷��̾� ������
    /// <summary>
    /// Arrow�� ������ �¿�� �����Դϴ�.
    /// </summary>
    /// <param name="horizon"> horizon ��ŭ </param>
    public void PlayerGetHorizontal(float horizon)
    {
        transform.Rotate(transform.up, horizon * rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Arrow�� ������ ���Ʒ��� �����Դϴ�.
    /// </summary>
    /// <param name="vertical"> vertical ��ŭ </param>
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
    /// �߻縦 ���� ������ �����մϴ�
    /// </summary>
    public void StartCharging()
    {
        isReady = false;
        isCharging = true;
    }


    /// <summary>
    /// Player�� �߻��մϴ�.
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
    /// Player�� �غ�ɶ����� ��ٸ��ϴ�.
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
    /// Arrow�� �������� ���Ƿ��̼��մϴ�.
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
    /// Arrow�� Scale�� �����մϴ�.
    /// </summary>
    /// <param name="arrowScale"></param>
    public void SetArrow(float arrowScale)
    {
        Arrow.transform.localScale = new Vector3(1.0f, 1.0f, arrowScale);
    }

    /// <summary>
    /// �÷��̾��� ���¸� �غ���·� �ʱ�ȭ�մϴ�.
    /// </summary>
    public void PlayerInitialise()
    {
        // velocity �ʱ�ȭ
        ResetObjectVelocity(rigidbody);

        // Player ������Ʈ�� Ball ������Ʈ�� ���� �ʱ�ȭ.
        Quaternion ballRotation = Ball.transform.rotation;
        FindClosestPlanet();
        Vector3 UpVector = gameObject.transform.position - Transform_closestPlaent.position;
        Quaternion UPRotation = Quaternion.LookRotation(UpVector, Vector3.up);
        float yRotation = UPRotation.eulerAngles.y;
        gameObject.transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        Ball.transform.rotation = ballRotation;

        // Arrow ������Ʈ �ʱ�ȭ
        SetArrow(1.0f);
        ArrowInitialise();
    }

    /// <summary>
    /// Arrow�� �غ���·� �ʱ�ȭ�մϴ�.
    /// </summary>
    public void ArrowInitialise()
    {
        Arrow.transform.rotation = transform.rotation;
        Arrow.transform.localPosition = new Vector3(0, 0, 0.7f);
    }

    /// <summary>
    /// �ӵ��� �Ӱ谪���� ������ false ��ȯ
    /// </summary>
    /// <param name="rigidbody"> �ӵ��� Ȯ���� ������Ʈ�� rigidbody </param>
    /// <param name="threshold"> �Ӱ谪 </param>
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
    /// ������Ʈ�� �ӵ��� 0���� �ʱ�ȭ
    /// </summary>
    /// <param name="rigidbody"> �ʱ�ȭ �� ������Ʈ�� rigidbody </param>
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

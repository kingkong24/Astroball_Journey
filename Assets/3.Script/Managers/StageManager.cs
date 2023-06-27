using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{ 
    [Header("Player")]
    [SerializeField] PlayerMovement playerMovement;

    [Space(0.2f)]
    [Header("Targets")]
    public GameObject[] GameObject_Targets;
    public int targetCounter;

    [Space(0.2f)]
    [Header("Ȯ�ο�")]
    [SerializeField] bool isGameStart = false;
    [SerializeField] bool isGamePause = false;
    [SerializeField] bool isPlayerReady = true;

    [Space(0.2f)]
    [Header("���� �̺�Ʈ")]
    public UnityEvent Event_GameStart;
    public UnityEvent Event_GamePause;
    public UnityEvent Event_GameResume;
    public UnityEvent Event_GameClearExit;
    public UnityEvent Event_GameClear;

    private void Awake()
    {
        Initialise();
        playerMovement.SetTarget(GameObject_Targets[targetCounter].transform);
    }


    void Start()
    {
        GameManager.instance.HideCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPlayerReady)
        {
            if (!isGamePause)
            {
                GamePause();

            }
            else
            {
                GameResume();
            }
        }

    }

    /// <summary>
    /// �ʱ�ȭ�մϴ�.
    /// </summary>
    private void Initialise()
    {
        isGameStart = false;
        isGamePause = false;
        targetCounter = 0;
    }

    /// <summary>
    /// ���� ���� �̺�Ʈ�� �ѹ��� �߻���ŵ�ϴ�.
    /// </summary>
    public void GameStart()
    {
        if (!isGameStart)
        {
            Event_GameStart.Invoke();
            isGameStart = true;
        }
    }

    /// <summary>
    /// ������ �Ͻ������մϴ�.
    /// </summary>
    public void GamePause()
    {
        isGamePause = true;
        Event_GamePause.Invoke();
    }

    /// <summary>
    /// ������ �簳�մϴ�.
    /// </summary>
    public void GameResume()
    {
        isGamePause = false;
        Event_GameResume.Invoke();
    }

    /// <summary>
    /// ���� Ÿ���� �����մϴ�.
    /// </summary>
    public void NextTarget()
    {
        targetCounter += 1;
        GameObject_Targets[targetCounter].SetActive(true);
        playerMovement.SetTarget(GameObject_Targets[targetCounter].transform);
    }

    /// <summary>
    /// Ready ���¸� Ȯ���մϴ�.
    /// </summary>
    public void ConfirmReady()
    {
        isPlayerReady = true;
    }

    /// <summary>
    /// !Ready ���¸� Ȯ���մϴ�.
    /// </summary>
    public void ConfirmShoot()
    {
        isPlayerReady = false;
    }

    /// <summary>
    /// ���� Ŭ���� �̺�Ʈ�� �߻��մϴ�.
    /// </summary>
    public void GameClear()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySFX("SFX_Clapping");   
        Event_GameClear.Invoke();
    }

    /// <summary>
    /// ���콺 Ŀ���� ����ϴ�.
    /// </summary>
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// ���콺 Ŀ���� �巯���ϴ�.
    /// </summary>
    public void ViewCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}


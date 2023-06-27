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
    [Header("확인용")]
    [SerializeField] bool isGameStart = false;
    [SerializeField] bool isGamePause = false;
    [SerializeField] bool isPlayerReady = true;

    [Space(0.2f)]
    [Header("게임 이벤트")]
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
    /// 초기화합니다.
    /// </summary>
    private void Initialise()
    {
        isGameStart = false;
        isGamePause = false;
        targetCounter = 0;
    }

    /// <summary>
    /// 게임 시작 이벤트를 한번만 발생시킵니다.
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
    /// 게임을 일시정지합니다.
    /// </summary>
    public void GamePause()
    {
        isGamePause = true;
        Event_GamePause.Invoke();
    }

    /// <summary>
    /// 게임을 재개합니다.
    /// </summary>
    public void GameResume()
    {
        isGamePause = false;
        Event_GameResume.Invoke();
    }

    /// <summary>
    /// 다음 타겟을 설정합니다.
    /// </summary>
    public void NextTarget()
    {
        targetCounter += 1;
        GameObject_Targets[targetCounter].SetActive(true);
        playerMovement.SetTarget(GameObject_Targets[targetCounter].transform);
    }

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

    /// <summary>
    /// 게임 클리어 이벤트를 발생합니다.
    /// </summary>
    public void GameClear()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlaySFX("SFX_Clapping");   
        Event_GameClear.Invoke();
    }

    /// <summary>
    /// 마우스 커서를 숨깁니다.
    /// </summary>
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// 마우스 커서를 드러냅니다.
    /// </summary>
    public void ViewCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}


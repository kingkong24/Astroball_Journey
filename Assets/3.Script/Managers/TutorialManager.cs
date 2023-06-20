using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] PlayerMovement playerMovement;

    [Header("Targets")]
    [SerializeField] GameObject[] GameObject_Targets;
    [SerializeField] int targetCounter;

    private void Awake()
    {
        targetCounter = 0;
        playerMovement.SetTarget(GameObject_Targets[targetCounter].transform);
    }


    void Start()
    {
        GameManager.instance.HideCursor();
    }

    /// <summary>
    /// 다음 타겟을 설정합니다.
    /// </summary>
    public void NextTarget()
    {
        targetCounter += 1;
        playerMovement.SetTarget(GameObject_Targets[targetCounter].transform);
    }
}


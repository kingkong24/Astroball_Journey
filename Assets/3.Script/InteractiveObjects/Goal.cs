using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] StageManager stageManager;

    private void Awake()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && stageManager.targetCounter < stageManager.GameObject_Targets.Length)
        {
            GameObject currentTarget = stageManager.GameObject_Targets[stageManager.targetCounter];

            if (currentTarget == gameObject)
            {
                stageManager.Event_GameClear.Invoke();
                Debug.Log("°ÔÀÓ ³¡³µ´Ù");
            }
        }
    }


}

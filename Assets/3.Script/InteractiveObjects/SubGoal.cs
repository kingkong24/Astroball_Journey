using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubGoal : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] StageManager stageManager;

    private void Awake()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("���� �� ���� ���Ծ��");
        if (other.CompareTag("Player") && stageManager.targetCounter < stageManager.GameObject_Targets.Length)
        {
            
            Debug.Log("�÷��̾ �� ���� ���Ծ��");
            GameObject currentTarget = stageManager.GameObject_Targets[stageManager.targetCounter];

            if (currentTarget == gameObject)
            {
                stageManager.NextTarget();
                gameObject.SetActive(false);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubGoal : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] StageManager stageManager;

    [Header("�� ��ü�� �ݶ��̴��� 'Player'�� ������ �� �߻��ϴ� �̺�Ʈ")]
    public UnityEvent Event_ThisSubGoalEnter;

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
                Event_ThisSubGoalEnter.Invoke();
                stageManager.NextTarget();
                PlayerControl playerMovement = other.GetComponent<PlayerControl>();
                playerMovement.SavePosition = transform.position - new Vector3(0, transform.localPosition.y - 1f, 0);

                gameObject.SetActive(false);
            }
        }
    }

}

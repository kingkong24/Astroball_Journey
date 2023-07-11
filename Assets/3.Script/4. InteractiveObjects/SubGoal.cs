using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubGoal : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] StageManager stageManager;

    [Header("다른 곳에 저장")]
    [SerializeField] Transform transform_save;
    [SerializeField] bool isDifferent = false;

    [Header("이 객체의 콜라이더에 'Player'가 들어왔을 때 발생하는 이벤트")]
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

                if (isDifferent && transform_save != null)
                {
                    playerMovement.SavePosition = transform_save.position;
                }
                gameObject.SetActive(false);
            }
        }
    }

}

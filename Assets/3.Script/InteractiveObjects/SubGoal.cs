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

        if (other.CompareTag("Player") && stageManager.targetCounter < stageManager.GameObject_Targets.Length)
        {
            GameObject currentTarget = stageManager.GameObject_Targets[stageManager.targetCounter];

            if (currentTarget == gameObject)
            {
                
                stageManager.NextTarget();
                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
                playerMovement.SavePosition = transform.position - new Vector3(0, transform.localPosition.y - 1f, 0);
                gameObject.SetActive(false);
            }
        }
    }

}

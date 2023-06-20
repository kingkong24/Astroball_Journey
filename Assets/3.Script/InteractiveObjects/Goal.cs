using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] TutorialManager tutorialManager;

    private void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && tutorialManager.targetCounter < tutorialManager.GameObject_Targets.Length)
        {
            GameObject currentTarget = tutorialManager.GameObject_Targets[tutorialManager.targetCounter];

            if (currentTarget == gameObject)
            {
                Debug.Log("°ÔÀÓ ³¡³µ´Ù");
            }
        }
    }
}

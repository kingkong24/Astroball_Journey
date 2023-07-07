using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainGoalUI : MonoBehaviour
{
    [SerializeField] Text text_remainGoal;
    [SerializeField] int remainNum;

    private void Awake()
    {
        StageManager stageManager = FindAnyObjectByType<StageManager>();
        remainNum = stageManager.GameObject_Targets.Length;
        CountDown();
    }

    public void CountDown()
    {
        text_remainGoal.text = "Number of goals : " + remainNum;
        if (remainNum ==0)
        {
            return;
        }
        remainNum--;
    }
}


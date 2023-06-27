using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalDistanceUI : MonoBehaviour
{
    [SerializeField] Text text_distance;
    [SerializeField] Transform transform_player;
    [SerializeField] Transform transform_goal;

    private void Awake()
    {
        text_distance = GetComponent<Text>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform_player.position, transform_goal.position);
        if (distance < 100.0f)
        {
            text_distance.text = distance.ToString("00.00") + "m";
        }
        else if (distance < 1000.0f)
        {
            text_distance.text = distance.ToString("000.00") + "m";
        }
        else
        {
            text_distance.text = distance.ToString("0000.00") + "m";
        }
    }
}

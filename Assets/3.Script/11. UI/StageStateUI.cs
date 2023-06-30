using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageStateUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Text Text_TimeCount;
    [SerializeField] Text Text_TryCount;

    [Space(0.2f)]
    [Header("Data")]
    public float float_elapsedTime;
    public int int_trycount;

    [Space(0.2f)]
    [Header("Ȯ�ο�")]
    private bool isTimerOn;

    private void Start()
    {
        isTimerOn = false;
        float_elapsedTime = 0;
        int_trycount = 0;
        Text_TimeCount.text = "00 : 00 : 00";
        Text_TryCount.text = "0 times";
    }

    private void Update()
    {
        if(isTimerOn)
        {
            AddTime();
        }
    }

    /// <summary>
    /// �ð� ����� �����մϴ�.
    /// </summary>
    public void TimeCountStart()
    {
        isTimerOn = true;
    }
    
    /// <summary>
    /// �ð� ����� ����ϴ�.
    /// </summary>
    public void TimeCountStop()
    {
        isTimerOn = false;
    }

    /// <summary>
    /// �帥 �ð��� ���մϴ�.
    /// </summary>
    private void AddTime()
    {
        float_elapsedTime += Time.deltaTime;

        int hours = (int)(float_elapsedTime / 3600);
        int minutes = (int)((float_elapsedTime % 3600) / 60);
        int seconds = (int)(float_elapsedTime % 60);

        string timerString = string.Format("{0:D2} : {1:D2} : {2:D2}", hours, minutes, seconds);
        Text_TimeCount.text = timerString;
    }

    /// <summary>
    /// �õ� Ƚ���� ���մϴ�.
    /// </summary>
    public void AddCount()
    {
        int_trycount += 1;
        Text_TryCount.text = string.Format("{0} times", int_trycount);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectpushMovement : MonoBehaviour
{
    [Header("오브젝트")]
    [SerializeField] GameObject targetObject;

    [Header("설정")]
    [SerializeField] Vector3 targetPosition;
    [SerializeField] float moveSpeed;
    [SerializeField] float returnSpeed;
    [SerializeField] float cooldownTime;
    [SerializeField] float waitForSecond;

    [Header("확인용")]
    [SerializeField] Vector3 startPosition;
    [SerializeField] WaitForSeconds waitForSeconds;
    [SerializeField] float Timer;

    private void Awake()
    {
        startPosition = targetObject.transform.localPosition;
        waitForSeconds = new WaitForSeconds(waitForSecond);
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= cooldownTime)
        {
            Timer -= cooldownTime;
            StartMovement();
        }
    }

    private void StartMovement()
    {
        StartCoroutine(Co_StartMovement());
    }

    IEnumerator Co_StartMovement()
    {
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / moveSpeed;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;
            targetObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        targetObject.transform.localPosition = targetPosition;

        yield return waitForSeconds;

        duration = distance / returnSpeed;
        timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;
            targetObject.transform.localPosition = Vector3.Lerp(targetPosition, startPosition, t);
            yield return null;
        }
        targetObject.transform.localPosition = startPosition;

    }
}



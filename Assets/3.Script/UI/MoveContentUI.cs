using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveContentUI : MonoBehaviour
{
    [Header("좌우이동할 UI")]
    [SerializeField] RectTransform rectTransform_Contents;

    [Header("확인용")]
    [SerializeField] float moveSpeed = 4800f;
    [SerializeField] float posXThresholdLeft = 2400f;
    [SerializeField] float posXThresholdRight = -2400f;
    [SerializeField] float moveAmountThreshold = 960f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();

        }

    }

    /// <summary>
    /// Contents를 왼쪽으로 움직입니다.
    /// </summary>
    public void MoveLeft()
    {
        if (rectTransform_Contents.localPosition.x < posXThresholdLeft)
        {
            targetPosition = rectTransform_Contents.localPosition + new Vector3(moveAmountThreshold, 0, 0);
            StartCoroutine(MoveToTargetPosition());
        }
    }

    /// <summary>
    /// Contents를 오른쪽으로 움직입니다.
    /// </summary>
    public void MoveRight()
    {
        if (rectTransform_Contents.localPosition.x > posXThresholdRight)
        {

            targetPosition = rectTransform_Contents.localPosition - new Vector3(moveAmountThreshold, 0, 0);
            StartCoroutine(MoveToTargetPosition());
        }
    }

    private IEnumerator MoveToTargetPosition()
    {
        isMoving = true;

        float distance = Vector3.Distance(rectTransform_Contents.localPosition, targetPosition);
        float duration = distance / moveSpeed;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            rectTransform_Contents.localPosition = Vector3.Lerp(rectTransform_Contents.localPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform_Contents.localPosition = targetPosition;
        isMoving = false;
    }
}


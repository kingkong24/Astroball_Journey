using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_6Manager : MonoBehaviour
{
    [Header("바꿀 선형 역장")]
    [SerializeField] LinearForceField linearForceField;
    [SerializeField] ObjectFollowOffset objectFollow;

    [Header("확인용")]
    [SerializeField] GameObject gameObject_ball;
    [SerializeField] PlayerCameraControl playerCameraControl;

    private void Awake()
    {
        gameObject_ball = GameObject.FindGameObjectWithTag("Ball");
        playerCameraControl = FindObjectOfType<PlayerCameraControl>();
    }

    private void Start()
    {
        Rigidbody rigidbody_ball = gameObject_ball.GetComponent<Rigidbody>();
        rigidbody_ball.useGravity = false;
        playerCameraControl.isUseGravity = true;
    }

    /// <summary>
    /// 역장의 방향을 바꿉니다.
    /// </summary>
    /// <param name="DirectionVector3"> "힘의 크기"/"Vector.x"/"Vector.y"/"Vector.z"의 형태로 입력하세요. </param>
    public void TranslateLinearForceField(string DirectionVector3)
    {
        char splitLetter = '/';

        string[] splitStrings = DirectionVector3.Split(splitLetter);

        float[] splitNumbers = new float[splitStrings.Length];

        try
        {
            if (splitStrings.Length == splitStrings.Length)
            {
                for (int i = 0; i < splitStrings.Length; i++)
                {
                    float.TryParse(splitStrings[i], out splitNumbers[i]);
                }
            }
            else
            {
                Debug.Log("형식에 맞지 않습니다.");
            }

            linearForceField.forceAmount =  splitNumbers[0];
            linearForceField.forceDirection.x = splitNumbers[1];
            linearForceField.forceDirection.y = splitNumbers[2];
            linearForceField.forceDirection.z = splitNumbers[3];

            objectFollow.Offset.x = 5 * splitNumbers[1];
            objectFollow.Offset.y = 5 * splitNumbers[2];
            objectFollow.Offset.z = 5 * splitNumbers[3];

        }
        catch (Exception e)
        {
            Debug.Log("오류 발생 : " + e.Message);
            return;
        }
    }


}

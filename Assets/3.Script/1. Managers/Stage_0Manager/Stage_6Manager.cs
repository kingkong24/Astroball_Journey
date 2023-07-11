using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_6Manager : MonoBehaviour
{
    [Header("�ٲ� ���� ����")]
    [SerializeField] LinearForceField linearForceField;
    [SerializeField] ObjectFollowOffset objectFollow;

    [Header("Ȯ�ο�")]
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
    /// ������ ������ �ٲߴϴ�.
    /// </summary>
    /// <param name="DirectionVector3"> "���� ũ��"/"Vector.x"/"Vector.y"/"Vector.z"�� ���·� �Է��ϼ���. </param>
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
                Debug.Log("���Ŀ� ���� �ʽ��ϴ�.");
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
            Debug.Log("���� �߻� : " + e.Message);
            return;
        }
    }


}

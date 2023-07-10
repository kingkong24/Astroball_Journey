using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowOffset : MonoBehaviour
{
    [Header("타겟")]
    [SerializeField] Transform transform_target;

    [Header("설정")]
    [SerializeField] Vector3 Offset;

    private void Update()
    {
        transform.position = transform_target.position + Offset;
    }


    /// <summary>
    /// offset을 수정합니다.
    /// </summary>
    /// <param name="offset"> 수정할 offset "/"를 넣어 구분해주세요. </param>
    public void SetOffset(string offset)
    {
        char splitLetter = '/';

        string[] splitStrings = offset.Split(splitLetter);

        float result1 = 0, result2 = 0, result3 = 0;

        try
        {
            if (splitStrings.Length == 3)
            {
                float.TryParse(splitStrings[0], out result1);
                float.TryParse(splitStrings[1], out result2);
                float.TryParse(splitStrings[2], out result3);

                Offset = new Vector3(result1, result2, result3);
            }
            else
            {
                Debug.Log("형식에 맞지 않습니다.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("오류 발생: " + e.Message);
            return;
        }

        Offset = new(result1, result2, result3);
    }
}

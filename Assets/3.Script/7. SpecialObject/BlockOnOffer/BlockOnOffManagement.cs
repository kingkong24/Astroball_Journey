using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOnOffManagement : MonoBehaviour
{
    [Header("블록들")]
    [SerializeField] GameObject[] gameObjects_block;
    /// <summary>
    /// 처음 활성화할 블록
    /// </summary>
    [SerializeField] string OnBlock;

    private void Awake()
    {
        OnOffBlocks(OnBlock);
    }


    /// <summary>
    /// 해당 번호의 블록을 On 또는 Off 합니다.
    /// </summary>
    /// <param name="OnOffNum"> On 또는 Off할 블록을 "/"를 넣어 구분해주세요</param>
    public void OnOffBlocks(string OnOffNum)
    {
        char splitLetter = '/';

        string[] splitStrings = OnOffNum.Split(splitLetter);

        int[] splitNumbers = new int[splitStrings.Length];

        try
        {
            if (splitStrings.Length == splitStrings.Length)
            {
                for(int i = 0; i < splitStrings.Length; i++)
                {
                    int.TryParse(splitStrings[i], out splitNumbers[i]);
                }
            }
            else
            {
                Debug.Log("형식에 맞지 않습니다.");
            }

            for (int i = 0; i < splitNumbers.Length; i++)
            {
                if (gameObjects_block[splitNumbers[i]].activeSelf)
                {
                    gameObjects_block[splitNumbers[i]].SetActive(false);
                }
                else
                {
                    gameObjects_block[splitNumbers[i]].SetActive(true);
                }

            }
        }
        catch (Exception e)
        {
            Debug.Log("오류 발생 : " + e.Message);
            return;
        }
    }
}

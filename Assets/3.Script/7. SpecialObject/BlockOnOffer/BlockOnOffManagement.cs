using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOnOffManagement : MonoBehaviour
{
    [Header("��ϵ�")]
    [SerializeField] GameObject[] gameObjects_block;
    /// <summary>
    /// ó�� Ȱ��ȭ�� ���
    /// </summary>
    [SerializeField] string OnBlock;

    private void Awake()
    {
        OnOffBlocks(OnBlock);
    }


    /// <summary>
    /// �ش� ��ȣ�� ����� On �Ǵ� Off �մϴ�.
    /// </summary>
    /// <param name="OnOffNum"> On �Ǵ� Off�� ����� "/"�� �־� �������ּ���</param>
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
                Debug.Log("���Ŀ� ���� �ʽ��ϴ�.");
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
            Debug.Log("���� �߻� : " + e.Message);
            return;
        }
    }
}

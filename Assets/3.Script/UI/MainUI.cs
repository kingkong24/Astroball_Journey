using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioManager audioManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// GameObject�� Ȱ��ȭ�մϴ�.
    /// </summary>
    /// <param name="gameObjects_On"> Ȱ��ȭ�� ������Ʈ </param>
    public void UIOn(GameObject gameObjects_On)
    {
        RectTransform rectTransform = gameObjects_On.GetComponent<RectTransform>();
        rectTransform.position += new Vector3(10000, 0, 0);

    }

    /// <summary>
    /// GameObject�� ��Ȱ��ȭ�մϴ�
    /// </summary>
    /// <param name="gameObjects_Off"> ��Ȱ��ȭ�� ������Ʈ </param>
    public void UIOff(GameObject gameObjects_Off)
    {

        RectTransform rectTransform = gameObjects_Off.GetComponent<RectTransform>();
        rectTransform.position -= new Vector3(10000, 0, 0);

    }



    /// <summary>
    /// MasterVolume�� �����մϴ�.
    /// </summary>
    /// <param name="volumes"> Master �Ҹ� ũ�� </param>
    public void SetMasterVolumes(float volumes)
    {
        gameManager.MasterVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.BGMVolumes);
        audioManager.SetBgmVolume(gameManager.SFXVolumes);

    }

    /// <summary>
    /// BGMVolume�� �����մϴ�.
    /// </summary>
    /// <param name="volumes"> BGM �Ҹ� ũ�� </param>
    public void SetBGMBolumes(float volumes)
    {
        gameManager.BGMVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.BGMVolumes);
    }

    /// <summary>
    /// SFXVolume�� �����մϴ�.
    /// </summary>
    /// <param name="volumes"> SFX �Ҹ� ũ�� </param>
    public void SetSFXBolumes(float volumes)
    {
        gameManager.SFXVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.SFXVolumes);
    }

    /// <summary>
    /// ���α׷��� �����մϴ�.
    /// </summary>
    public void ProgramQuit()
    {
        Application.Quit();
    }
}

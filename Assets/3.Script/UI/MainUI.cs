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
    /// GameObject를 활성화합니다.
    /// </summary>
    /// <param name="gameObjects_On"> 활성화할 오브젝트 </param>
    public void UIOn(GameObject gameObjects_On)
    {
        RectTransform rectTransform = gameObjects_On.GetComponent<RectTransform>();
        rectTransform.position += new Vector3(10000, 0, 0);

    }

    /// <summary>
    /// GameObject를 비활성화합니다
    /// </summary>
    /// <param name="gameObjects_Off"> 비활성화할 오브젝트 </param>
    public void UIOff(GameObject gameObjects_Off)
    {

        RectTransform rectTransform = gameObjects_Off.GetComponent<RectTransform>();
        rectTransform.position -= new Vector3(10000, 0, 0);

    }



    /// <summary>
    /// MasterVolume을 설정합니다.
    /// </summary>
    /// <param name="volumes"> Master 소리 크기 </param>
    public void SetMasterVolumes(float volumes)
    {
        gameManager.MasterVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.BGMVolumes);
        audioManager.SetBgmVolume(gameManager.SFXVolumes);

    }

    /// <summary>
    /// BGMVolume을 설정합니다.
    /// </summary>
    /// <param name="volumes"> BGM 소리 크기 </param>
    public void SetBGMBolumes(float volumes)
    {
        gameManager.BGMVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.BGMVolumes);
    }

    /// <summary>
    /// SFXVolume을 설정합니다.
    /// </summary>
    /// <param name="volumes"> SFX 소리 크기 </param>
    public void SetSFXBolumes(float volumes)
    {
        gameManager.SFXVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.SFXVolumes);
    }

    /// <summary>
    /// 프로그램을 종료합니다.
    /// </summary>
    public void ProgramQuit()
    {
        Application.Quit();
    }
}

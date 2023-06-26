using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Managers")]
    [SerializeField] AudioManager audioManager;

    [Header("Volumes")]
    public float MasterVolumes = 0.6f;
    public float BGMVolumes = 0.6f;
    public float SFXVolumes = 0.6f;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        audioManager = FindObjectOfType<AudioManager>();
    }

    /// <summary>
    /// 마우스 커서를 숨깁니다.
    /// </summary>
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// MasterVolum을 설정합니다.
    /// </summary>
    /// <param name="value"></param>
    public void SetMasterVolums(float value)
    {
        MasterVolumes = value;
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.SetBgmVolume(MasterVolumes * BGMVolumes);
        audioManager.SetSFXVolume(MasterVolumes * SFXVolumes);
        audioManager.PlaySFX("SFX_TestClip");
    }

    /// <summary>
    /// BGM Volume을 설정합니다.
    /// </summary>
    /// <param name="value"></param>
    public void SetBGMVolums(float value)
    {
        BGMVolumes = value;
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.bgmVolume = BGMVolumes * MasterVolumes;
        audioManager.SetBgmVolume(audioManager.bgmVolume);
    }

    /// <summary>
    /// SFX Volume을 설정합니다.
    /// </summary>
    /// <param name="value"></param>
    public void SetSFXVolums(float value)
    {
        SFXVolumes = value;
        audioManager = FindObjectOfType<AudioManager>();

        audioManager.bgmVolume = SFXVolumes * MasterVolumes;
        audioManager.SetSFXVolume(audioManager.bgmVolume);
        audioManager.PlaySFX("SFX_TestClip");
    }
}

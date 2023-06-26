using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        Debug.Log("Master : " + value);
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if(audioManager != null)
        {
            audioManager.SetBgmVolume();
            audioManager.SetSFXVolume();
        }
    }    
    
    /// <summary>
    /// BGM Volume을 설정합니다.
    /// </summary>
    /// <param name="value"></param>
    public void SetBGMVolums(float value)
    {
        BGMVolumes = value;
        Debug.Log("BGM : " + value);
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if(audioManager != null)
        {
            audioManager.SetBgmVolume();
        }
    }    
    
    /// <summary>
    /// SFX Volume을 설정합니다.
    /// </summary>
    /// <param name="value"></param>
    public void SetSFXVolums(float value)
    {
        SFXVolumes = value;
        Debug.Log("SFX : " + value);
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if(audioManager != null)
        {
            audioManager.SetSFXVolume();
        }
    }
}

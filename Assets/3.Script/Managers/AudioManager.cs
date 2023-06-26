using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound
{
    public string name;
    public AudioClip clip;
}
public class AudioManager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameManager gameManager;

    [Header("BGM")]
    [SerializeField] Sound[] bgmClips;
    public float bgmVolume;
    [SerializeField] AudioSource BGMplayer;

    [Space(30)]
    [Header("SFX")]
    [SerializeField] float sfxVolume;
    public Sound[] sfxClips;
    [SerializeField] AudioSource SFXplayer;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        PlayBGM("BGM_Test");
    }

    /// <summary>
    /// �ʱ�ȭ�մϴ�.
    /// </summary>
    private void Init()
    {
        // GameManager
        gameManager = FindObjectOfType<GameManager>();

        // BGM Player  ����
        BGMplayer.playOnAwake = false;
        BGMplayer.loop = true;
        SetBgmVolume(gameManager.MasterVolumes * gameManager.BGMVolumes);

        // SFX Player  ����
        SFXplayer.playOnAwake = false;
        SetSFXVolume(gameManager.SFXVolumes * gameManager.BGMVolumes);
    }


    /// <summary>
    /// BGM�� ����մϴ�.
    /// </summary>
    /// <param name="name"></param>
    public void PlayBGM(string name)
    {
        // �̹� ������� BGM�� ������ ����
        StopBGM();

        // �̸� ��ġ�ϴ� BGM ã�Ƽ� ���
        foreach (Sound s in bgmClips)
        {
            if (s.name.Equals(name))
            {
                BGMplayer.clip = s.clip;
                BGMplayer.Play();
                break;
            }
        }
    }

    /// <summary>
    /// BGM�� ����ϴ�.
    /// </summary>
    public void StopBGM()
    {
        if (BGMplayer.isPlaying)
        {
            BGMplayer.Stop();
        }
    }

    /// <summary>
    /// BGM Volume�� �����մϴ�.
    /// </summary>
    /// <param name="volume"></param>
    public void SetBgmVolume(float volume)
    {
        BGMplayer.volume = volume;
    }

    /// <summary>
    /// ȿ������ ����մϴ�.
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX(string name)
    {
        // �̸� ��ġ�ϴ� SFX ã�Ƽ� ���
        foreach (Sound s in sfxClips)
        {
            if (s.name.Equals(name))
            {
                SFXplayer.clip = s.clip;
                SFXplayer.Play();
                break;
            }
        }
    }

    /// <summary>
    /// ȿ������ �����մϴ�.
    /// </summary>
    /// <param name="volume"></param>
    public void SetSFXVolume(float volume)
    {
        Debug.Log("audioManager" + volume);
        SFXplayer.volume = volume;
    }
}

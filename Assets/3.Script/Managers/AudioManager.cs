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
    [SerializeField] float bgmVolume;
    [SerializeField] AudioSource BGMplayer;

    [Space(30)]
    [Header("SFX")]
    [SerializeField] float sfxVolume;
    [SerializeField] Sound[] sfxClips;
    [SerializeField] AudioSource SFXplayer;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// �ʱ�ȭ�մϴ�.
    /// </summary>
    private void Init()
    {
        // GameManager
        gameManager = FindObjectOfType<GameManager>();

        // BGM Player ���� �� ����
        GameObject bgm_obj = GetBgm_obj();
        bgm_obj.transform.parent = transform;
        BGMplayer = bgm_obj.AddComponent<AudioSource>();
        BGMplayer.playOnAwake = false;
        BGMplayer.loop = true;
        SetBgmVolume(gameManager.MasterVolumes * gameManager.BGMVolumes);

        // SFX Player ���� �� ����
        GameObject sfx_obj = new("SfxPlayer");
        sfx_obj.transform.parent = transform;
        SFXplayer = sfx_obj.AddComponent<AudioSource>();
        SFXplayer.playOnAwake = false;
        SetSFXVolume(gameManager.SFXVolumes * gameManager.BGMVolumes);
    }

    private static GameObject GetBgm_obj()
    {
        return new GameObject("BgmPlayer");
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
    void SetBgmVolume(float volume = 1.0f)
    {
        SFXplayer.volume = volume;
    }

    /// <summary>
    /// ȿ������ ����մϴ�.
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX(string name)
    {
        // �̸� ��ġ�ϴ� SFX ã�Ƽ� ���
        foreach (Sound s in bgmClips)
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
    void SetSFXVolume(float volume = 1.0f)
    {
        SFXplayer.volume = volume;
    }
}

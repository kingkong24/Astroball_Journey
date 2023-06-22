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

    private void Init()
    {
        // GameManager
        gameManager = FindObjectOfType<GameManager>();

        // BGM Player ���� �� ����
        GameObject bgm_obj = new GameObject("BgmPlayer");
        bgm_obj.transform.parent = transform;
        BGMplayer = bgm_obj.AddComponent<AudioSource>();
        BGMplayer.playOnAwake = false;
        BGMplayer.loop = true;
        SetBgmVolume(gameManager.MasterVolumes * gameManager.BGMVolumes);

        // SFX Player ���� �� ����
        GameObject sfx_obj = new GameObject("SfxPlayer");
        sfx_obj.transform.parent = transform;
        SFXplayer = sfx_obj.AddComponent<AudioSource>();
        SFXplayer.playOnAwake = false;
        SetSFXVolume(gameManager.SFXVolumes * gameManager.BGMVolumes);
    }

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
    public void StopBGM()
    {
        if (BGMplayer.isPlaying)
        {
            BGMplayer.Stop();
        }
    }

    void SetBgmVolume(float volume = 1.0f)
    {
        SFXplayer.volume = volume;
    }

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

    void SetSFXVolume(float volume = 1.0f)
    {
        SFXplayer.volume = volume;
    }
}

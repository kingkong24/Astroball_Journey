using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingSound : MonoBehaviour
{
    [Header("소리")]
    [Tooltip("3개의 소리를 넣습니다.")]
    [SerializeField] AudioClip[] audioClips_swing;
    [Tooltip("2개의 비율을 넣습니다.")]
    [SerializeField] float[] ratio_sound;

    [Header("확인용")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] PlayerControl playerControl;
    [SerializeField] float powerScale;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerControl = GetComponent<PlayerControl>();
        powerScale = playerControl.MaxPower - playerControl.MinPower;
    }

    private void Start()
    {
        audioSource.volume = GameManager.instance.MasterVolumes * GameManager.instance.SFXVolumes;
    }

    public void SwingSound()
    {
        float power = (playerControl.gameobject_arrow.transform.localScale.z - playerControl.MinPower) / powerScale;

        if(power <= ratio_sound[0])
        {
            audioSource.PlayOneShot(audioClips_swing[0]);
        }
        else if(power <= ratio_sound[1])
        {
            audioSource.PlayOneShot(audioClips_swing[1]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips_swing[2]);
        }
    }
}

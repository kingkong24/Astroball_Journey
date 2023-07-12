using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwingSound : MonoBehaviour
{
    [Header("�Ҹ�")]
    [Tooltip("3���� �Ҹ��� �ֽ��ϴ�.")]
    [SerializeField] AudioClip[] audioClips_swing;
    [Tooltip("2���� ������ �ֽ��ϴ�.")]
    [SerializeField] float[] ratio_sound;

    [Header("Ȯ�ο�")]
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

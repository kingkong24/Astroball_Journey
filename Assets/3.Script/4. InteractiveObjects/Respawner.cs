using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [Header("소리")]
    [SerializeField] AudioClip audioClip;

    [Header("확인용")]
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.volume = GameManager.instance.MasterVolumes * GameManager.instance.SFXVolumes * 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl playerMovement = other.GetComponent<PlayerControl>();
            if (playerMovement != null)
            {
                playerMovement.ResetPlayer();
                audioSource.PlayOneShot(audioClip);
            }
        }
    }
}

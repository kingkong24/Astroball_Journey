using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitSound : MonoBehaviour
{

    [Header("오디오 클립")]
    [SerializeField] AudioClip audioClip_HitSound;

    [Header("확인용")]
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] new Rigidbody rigidbody;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            audioSource.volume = gameManager.MasterVolumes * gameManager.SFXVolumes;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float relativeSpeed = rigidbody.velocity.magnitude;
        audioSource.volume = relativeSpeed / 10f;
        BallHitSoundChanger ballHitSoundChanger = collision.gameObject.GetComponent<BallHitSoundChanger>();
        if (ballHitSoundChanger != null && ballHitSoundChanger.audioClip != null)
        {
            audioSource.PlayOneShot(ballHitSoundChanger.audioClip);
        }
        else if (audioClip_HitSound != null)
        {
            audioSource.PlayOneShot(audioClip_HitSound);
        }
    }
}

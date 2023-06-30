using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitSound : MonoBehaviour
{

    [Header("오디오 클립")]
    [SerializeField] AudioClip[] audioClips_HitSounds;
    public int HitAudioNumber;

    [Header("확인용")]
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] new Rigidbody rigidbody;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        if(gameManager != null)
        {
            audioSource.volume = gameManager.MasterVolumes * gameManager.SFXVolumes;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float relativeSpeed = rigidbody.velocity.magnitude;

        audioSource.volume = relativeSpeed / 10f;

        if (HitAudioNumber >= 0 && HitAudioNumber < audioClips_HitSounds.Length)
        {
            audioSource.PlayOneShot(audioClips_HitSounds[HitAudioNumber]);
        }
    }


    public void ChangeAudioClip(int num)
    {
        HitAudioNumber = num;
    }
}

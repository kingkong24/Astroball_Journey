using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitSound : MonoBehaviour
{

    [Header("오디오 클립")]
    [SerializeField] AudioClip[] audioClips_HitSounds;
    public int HitAudioNumber;

    [Header("확인용")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        // 여기에 게임 메니저를 넣고 소리 조절;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float relativeSpeed = collision.relativeVelocity.magnitude;

        audioSource.volume = relativeSpeed / 10f;

        if (HitAudioNumber >= 0 && HitAudioNumber < audioClips_HitSounds.Length)
        {
            audioSource.clip = audioClips_HitSounds[HitAudioNumber];
            audioSource.Play();
        }
    }


    public void ChangeAudioClip(int num)
    {
        HitAudioNumber = num;
    }
}

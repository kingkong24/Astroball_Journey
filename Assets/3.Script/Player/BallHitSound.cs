using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitSound : MonoBehaviour
{

    [Header("����� Ŭ��")]
    [SerializeField] AudioClip[] audioClips_HitSounds;
    public int HitAudioNumber;

    [Header("Ȯ�ο�")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        // ���⿡ ���� �޴����� �ְ� �Ҹ� ����;
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

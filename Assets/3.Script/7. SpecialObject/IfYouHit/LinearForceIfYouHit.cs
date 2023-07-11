using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearForceIfYouHit : MonoBehaviour
{
    [Header("힘 크기와 방향")]
    [SerializeField] Vector3 direction;
    [SerializeField] float force;

    [Header("소리")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    [Header("확인용")]
    [SerializeField] new Collider collider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
        direction = direction.normalized;
    }

    private void Start()
    {
        audioSource.volume = GameManager.instance.MasterVolumes * GameManager.instance.SFXVolumes;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball"))
        {
            audioSource.PlayOneShot(audioClip);
            col.attachedRigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}

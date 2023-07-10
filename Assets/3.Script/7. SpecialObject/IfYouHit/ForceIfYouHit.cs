using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceIfYouHit : MonoBehaviour
{
    [Header("힘 크기")]
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
            Vector3 forceDirection = col.transform.position - collider.bounds.center;
            forceDirection = forceDirection.normalized;
            col.attachedRigidbody.AddForce(forceDirection * force, ForceMode.Impulse);
        }
    }
}

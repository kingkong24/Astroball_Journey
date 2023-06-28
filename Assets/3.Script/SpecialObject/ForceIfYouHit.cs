using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceIfYouHit : MonoBehaviour
{
    [Header("�� ũ��")]
    [SerializeField] float force;

    [Header("�Ҹ�")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    [Header("Ȯ�ο�")]
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

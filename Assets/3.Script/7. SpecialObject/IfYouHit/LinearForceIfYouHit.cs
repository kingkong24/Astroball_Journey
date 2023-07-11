using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearForceIfYouHit : MonoBehaviour
{
    [Header("�� ũ��� ����")]
    [SerializeField] Vector3 direction;
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

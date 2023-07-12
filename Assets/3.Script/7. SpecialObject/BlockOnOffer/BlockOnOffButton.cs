using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockOnOffButton : MonoBehaviour
{

    [SerializeField] BlockOnOffManagement blockOnOffManagement;
    [SerializeField] string OnOffNum;
    [SerializeField] AudioClip audioClip;

    [Header("È®ÀÎ¿ë")]
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.volume = GameManager.instance.MasterVolumes * GameManager.instance.SFXVolumes;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            blockOnOffManagement.OnOffBlocks(OnOffNum);
            audioSource.PlayOneShot(audioClip);
        }
    }
}

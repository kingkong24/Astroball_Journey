using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("상대 포탈")]
    [Tooltip("Cylinder를 넣을 것")]
    [SerializeField] Transform connectedPortal;

    [Header("설정")]
    [SerializeField] AudioClip audioClip;
    [SerializeField] float cooltime = 0.2f;

    [Header("확인용")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] Portal Portal_connected;
    public bool isPortalOn;
    WaitForSeconds cooldownDuration;

    private void Awake()
    {
        isPortalOn = true;
        cooldownDuration = new WaitForSeconds(cooltime);
        Portal_connected = connectedPortal.GetComponent<Portal>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.volume = GameManager.instance.MasterVolumes * GameManager.instance.SFXVolumes;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ball") && isPortalOn)
        {
            Quaternion rotation = Quaternion.FromToRotation(transform.forward, connectedPortal.forward);

            Rigidbody ballRigidbody = col.GetComponent<Rigidbody>();

            Vector3 ballDirection = ballRigidbody.velocity.normalized;
            float ballSpeed = ballRigidbody.velocity.magnitude;

            ballDirection = Quaternion.Euler(0f, 180f, 0f) * ballDirection;
            ballDirection = rotation * ballDirection;

            ballRigidbody.isKinematic = true;
            ballRigidbody.isKinematic = false;

            StartCoroutine(PortalColltime());

            col.transform.position = connectedPortal.position;
            ballRigidbody.velocity = ballSpeed * ballDirection;

            audioSource.PlayOneShot(audioClip);


        }
    }


    IEnumerator PortalColltime()
    {
        Portal_connected.isPortalOn = false;
        isPortalOn = false;

        yield return cooldownDuration;

        Portal_connected.isPortalOn = true;
        isPortalOn = true;
    }




}

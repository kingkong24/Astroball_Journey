using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisappearTime : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] float cooltime = 5.0f;

    [Header("확인용")]
    [SerializeField] float timer;
    [SerializeField] BoxCollider boxCollider;
    [SerializeField] MeshRenderer meshRenderer;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            timer += Time.deltaTime;
            if (timer >= cooltime)
            {
                if (boxCollider != null)
                    boxCollider.enabled = false;
                if (meshRenderer != null)
                    meshRenderer.enabled = false;
            }

        }
    }

    public void OnObject()
    {
        if (boxCollider != null)
            boxCollider.enabled = true;
        if (meshRenderer != null)
            meshRenderer.enabled = true;
    }
}

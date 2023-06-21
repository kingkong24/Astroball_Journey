using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalDistanceUI : MonoBehaviour
{
    [Header("distanceText")]
    [SerializeField] TMP_Text distanceText;

    [Header("camera")]
    [SerializeField] Camera camera;

    [Header("����")]
    [SerializeField] float minSize = 0.0002f;
    [SerializeField] float maxSize = 0.002f;
    [SerializeField] float ParentDistance = 100.0f;

    private Transform playerTransform;

    private void OnEnable()
    {
        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (camera == null)
        {
            camera = cam;
        }
        else
        {
            Debug.LogError("ī�޶� ������Ʈ�� ã�� �� �����ϴ�.");
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("�÷��̾� ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void Update()
    {
        if (playerTransform == null || camera == null)
        {
            return;
        }

        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward,
            camera.transform.rotation * Vector3.up);

        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <100.0f)
        {
            distanceText.text = distance.ToString("00.00") + "m";
        }
        else
        {
            distanceText.text = distance.ToString("000.00") + "m";
        }

        float ratio = Mathf.Clamp01(distance / ParentDistance);
        float size = Mathf.Lerp(minSize, maxSize, ratio);
        transform.localScale = new Vector3(20 * size, size, size);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldForceChecker : MonoBehaviour
{
    [Header("���� ����� ���⸦ ǥ���� ������Ʈ")]
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject zero;

    [SerializeField] float MaxZScale = 2.0f;

    [Header("������ ����")]
    [SerializeField] LinearForceField[] LinearForceFields;

    private void Awake()
    {
        LinearForceFields = FindObjectsOfType<LinearForceField>();
    }

    private void Start()
    {
        Recalculate();
    }

    /// <summary>
    /// LinearForceField���� ���� �����Ͽ� ��Ÿ���ϴ�.
    /// </summary>
    public void Recalculate()
    {
        Vector3 totalForce = Vector3.zero;

        foreach (LinearForceField forceField in LinearForceFields)
        {
            if (forceField.isPlayerOn)
            {
                totalForce += forceField.forceAmount * forceField.forceDirection;
            }
        }

        if (totalForce.magnitude == 0f)
        {
            Arrow.SetActive(false);
            zero.SetActive(true);
        }
        else
        {
            Arrow.SetActive(true);
            zero.SetActive(false);
            Arrow.transform.rotation = Quaternion.LookRotation(totalForce);
            float zScale = Mathf.Min(totalForce.magnitude, MaxZScale);
            float normalizedScale = zScale / MaxZScale;
            Vector3 newScale = new(normalizedScale, normalizedScale, zScale);
            Arrow.transform.localScale = newScale;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldForceChecker : MonoBehaviour
{
    [Header("힘의 방향과 세기를 표현할 오브젝트")]
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject zero;

    [SerializeField] float MaxZScale = 2.0f;

    [Header("포함할 역장")]
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
    /// LinearForceField들의 힘을 재계산하여 나타냅니다.
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

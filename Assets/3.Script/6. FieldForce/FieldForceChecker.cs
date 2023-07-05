using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldForceChecker : MonoBehaviour
{
    [Header("힘의 방향과 세기를 표현할 오브젝트")]
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject zero;
    [SerializeField] float MaxZScale = 2.0f;

    [Header("제외할 역장")]
    [SerializeField] SpiralForceField[] spiralForceFields_exclude;
    [SerializeField] LinearForceField[] LinearForceFields_exclude;
    [SerializeField] GravityField[] gravityFields_exclude;
    
    [Header("포함할 역장")]
    [SerializeField] LinearForceField[] LinearForceFields;
    [SerializeField] SpiralForceField[] spiralForceFields;
    [SerializeField] GravityField[] gravityFields;

    private void Awake()
    {
        LinearForceFields = FindObjectsOfType<LinearForceField>();
        spiralForceFields = FindObjectsOfType<SpiralForceField>();
        gravityFields = FindObjectsOfType<GravityField>();

        LinearForceFields = LinearForceFields.Except(LinearForceFields_exclude.AsEnumerable()).ToArray();
        spiralForceFields = spiralForceFields.Except(spiralForceFields_exclude.AsEnumerable()).ToArray();
        gravityFields = gravityFields.Except(gravityFields_exclude.AsEnumerable()).ToArray();
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
        
        foreach (SpiralForceField forceField in spiralForceFields)
        {
            if (forceField.isPlayerOn)
            {
                totalForce += forceField.forceAmountUP * forceField.transform.up;
                totalForce += forceField.directsionRight;

            }
        }
        foreach (GravityField forceField in gravityFields)
        {
            if (forceField.isPlayerOn)
            {
                totalForce += forceField.gracityForceAmount * forceField.direction;
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

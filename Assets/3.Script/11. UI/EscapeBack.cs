using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EscapeBack : MonoBehaviour
{
    [Header("Event")]
    public UnityEvent unityEvent_EscapeBack;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            unityEvent_EscapeBack.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] GameObject GameObject_menu;
    [SerializeField] GameObject GameObject_exit;

    public void OnMenu()
    {
        GameObject_menu.SetActive(true);
    }

    public void OffMenu()
    {
        GameObject_menu.SetActive(false);
    }

    public void OnExit()
    {
        GameObject_exit.SetActive(true);
    }

    public void OffExit()
    {
        GameObject_exit.SetActive(false);
    }


}

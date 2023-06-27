using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] GameObject GameObject_exit;
    [SerializeField] GameObject GameObject_Save;

    public void OnExit()
    {
        GameObject_exit.SetActive(true);
    }

    public void OffExit()
    {
        GameObject_exit.SetActive(false);
    }

    public void OnSave()
    {
        GameObject_Save.SetActive(true);
    }

    public void OffSave()
    {
        GameObject_Save.SetActive(false);
    }

    public void SceneLoad(string name)
    {
        SceneLoad(name);
    }




}

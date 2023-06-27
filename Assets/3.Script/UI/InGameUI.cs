using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] GameObject GameObject_exit;

    public void OnExit()
    {
        GameObject_exit.SetActive(true);
    }

    public void OffExit()
    {
        GameObject_exit.SetActive(false);
    }

    public void SceneLoad(string name)
    {
        SceneLoad(name);
    }


}

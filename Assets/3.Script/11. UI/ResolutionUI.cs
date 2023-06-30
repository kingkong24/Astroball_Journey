using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionUI : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Toggle fullscreenToggle;

    [Header("È®ÀÎ¿ë")]
    [SerializeField] bool isFullScreen = true;


    private void Start()
    {
        fullscreenToggle.isOn = isFullScreen;
        SetResolution(0);
    }

    public void SetResolution(int resolutionIndex)
    {
        switch (resolutionIndex)
        {
            case 0:
                Screen.SetResolution(1920, 1080, isFullScreen);
                Debug.Log("1920");
                break;
            case 1:
                Screen.SetResolution(1600, 900, isFullScreen);
                Debug.Log("1600");
                break;
            case 2:
                Screen.SetResolution(1280, 720, isFullScreen);
                Debug.Log("1280");
                break;
            default:
                break;
        }
    }

    public void ToggleFullscreen(bool isFullscreen)
    {
        Debug.Log("Fullscreen : " + isFullscreen);
        Screen.fullScreen = isFullscreen;
        isFullScreen = isFullscreen;
    }
}

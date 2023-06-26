using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioManager audioManager;

    [Header("Canvus")]
    [SerializeField] CanvasScaler canvasScaler;

    [Header("확인용")]
    [SerializeField] Vector3 targetPosition;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        canvasScaler = GetComponent<CanvasScaler>();
    }

    /// <summary>
    /// GameObject를 활성화하고 가져옵니다.
    /// </summary>
    /// <param name="gameObjects_On"> 가져올 오브젝트 </param>
    public void UIOn(GameObject gameObjects_On)
    {
        RectTransform rectTransform = gameObjects_On.GetComponent<RectTransform>();
        targetPosition = rectTransform.position + new Vector3(0, canvasScaler.referenceResolution.y * rectTransform.localScale.y, 0);
        StartCoroutine(MoveObject(rectTransform, targetPosition, true));
    }

    /// <summary>
    /// GameObject를 내보내고 비활성화합니다.
    /// </summary>
    /// <param name="gameObjects_Off"> 내보낼 오브젝트 </param>
    public void UIOff(GameObject gameObjects_Off)
    {
        RectTransform rectTransform = gameObjects_Off.GetComponent<RectTransform>();
        targetPosition = rectTransform.position - new Vector3(0, canvasScaler.referenceResolution.y * rectTransform.localScale.y, 0);
        StartCoroutine(MoveObject(rectTransform, targetPosition, false));
    }

    /// <summary>
    /// UI를 0.5초간 부드럽게 이동시킨 활성화하거나 비활성화합니다.
    /// </summary>
    /// <param name="rectTransform"> 해당 UI의 rectTransform </param>
    /// <param name="targetPosition"> 움직일 위치 </param>
    /// <param name="isUIOn"> 활성화 여부 </param>
    /// <returns></returns>
    private IEnumerator MoveObject(RectTransform rectTransform, Vector3 targetPosition, bool isUIOn)
    {
        if(isUIOn) rectTransform.gameObject.SetActive(true);

        Vector3 startPosition = rectTransform.position;
        float elapsedTime = 0f;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            rectTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }

        rectTransform.position = targetPosition;

        if(!isUIOn) rectTransform.gameObject.SetActive(false);
    }

    /// <summary>
    /// UI를 활성화합니다.
    /// </summary>
    /// <param name="gameObjects_ActiveTrue">활성화할 UI</param>
    public void UIActivetrue(GameObject gameObjects_ActiveTrue)
    {
        gameObjects_ActiveTrue.SetActive(true);
    }

    /// <summary>
    /// UI를 비활성화합니다.
    /// </summary>
    /// <param name="gameObjects_ActiveFalse">비활성화할 UI</param>
    public void UIActiveFalse(GameObject gameObjects_ActiveFalse)
    {
        gameObjects_ActiveFalse.SetActive(false);
    }

    /// <summary>
    /// MasterVolume을 설정합니다.
    /// </summary>
    /// <param name="volumes"> Master 소리 크기 </param>
    public void SetMasterVolumes(float volumes)
    {
        gameManager.MasterVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.BGMVolumes);
        audioManager.SetBgmVolume(gameManager.SFXVolumes);

    }

    /// <summary>
    /// BGMVolume을 설정합니다.
    /// </summary>
    /// <param name="volumes"> BGM 소리 크기 </param>
    public void SetBGMBolumes(float volumes)
    {
        gameManager.BGMVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.BGMVolumes);
    }

    /// <summary>
    /// SFXVolume을 설정합니다.
    /// </summary>
    /// <param name="volumes"> SFX 소리 크기 </param>
    public void SetSFXBolumes(float volumes)
    {
        gameManager.SFXVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.SFXVolumes);
    }

    /// <summary>
    /// 프로그램을 종료합니다.
    /// </summary>
    public void ProgramQuit()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

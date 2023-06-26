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

    [Header("Ȯ�ο�")]
    [SerializeField] Vector3 targetPosition;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        canvasScaler = GetComponent<CanvasScaler>();
    }

    /// <summary>
    /// GameObject�� Ȱ��ȭ�ϰ� �����ɴϴ�.
    /// </summary>
    /// <param name="gameObjects_On"> ������ ������Ʈ </param>
    public void UIOn(GameObject gameObjects_On)
    {
        RectTransform rectTransform = gameObjects_On.GetComponent<RectTransform>();
        targetPosition = rectTransform.position + new Vector3(0, canvasScaler.referenceResolution.y * rectTransform.localScale.y, 0);
        StartCoroutine(MoveObject(rectTransform, targetPosition, true));
    }

    /// <summary>
    /// GameObject�� �������� ��Ȱ��ȭ�մϴ�.
    /// </summary>
    /// <param name="gameObjects_Off"> ������ ������Ʈ </param>
    public void UIOff(GameObject gameObjects_Off)
    {
        RectTransform rectTransform = gameObjects_Off.GetComponent<RectTransform>();
        targetPosition = rectTransform.position - new Vector3(0, canvasScaler.referenceResolution.y * rectTransform.localScale.y, 0);
        StartCoroutine(MoveObject(rectTransform, targetPosition, false));
    }

    /// <summary>
    /// UI�� 0.5�ʰ� �ε巴�� �̵���Ų Ȱ��ȭ�ϰų� ��Ȱ��ȭ�մϴ�.
    /// </summary>
    /// <param name="rectTransform"> �ش� UI�� rectTransform </param>
    /// <param name="targetPosition"> ������ ��ġ </param>
    /// <param name="isUIOn"> Ȱ��ȭ ���� </param>
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
    /// UI�� Ȱ��ȭ�մϴ�.
    /// </summary>
    /// <param name="gameObjects_ActiveTrue">Ȱ��ȭ�� UI</param>
    public void UIActivetrue(GameObject gameObjects_ActiveTrue)
    {
        gameObjects_ActiveTrue.SetActive(true);
    }

    /// <summary>
    /// UI�� ��Ȱ��ȭ�մϴ�.
    /// </summary>
    /// <param name="gameObjects_ActiveFalse">��Ȱ��ȭ�� UI</param>
    public void UIActiveFalse(GameObject gameObjects_ActiveFalse)
    {
        gameObjects_ActiveFalse.SetActive(false);
    }

    /// <summary>
    /// MasterVolume�� �����մϴ�.
    /// </summary>
    /// <param name="volumes"> Master �Ҹ� ũ�� </param>
    public void SetMasterVolumes(float volumes)
    {
        gameManager.MasterVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.BGMVolumes);
        audioManager.SetBgmVolume(gameManager.SFXVolumes);

    }

    /// <summary>
    /// BGMVolume�� �����մϴ�.
    /// </summary>
    /// <param name="volumes"> BGM �Ҹ� ũ�� </param>
    public void SetBGMBolumes(float volumes)
    {
        gameManager.BGMVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.BGMVolumes);
    }

    /// <summary>
    /// SFXVolume�� �����մϴ�.
    /// </summary>
    /// <param name="volumes"> SFX �Ҹ� ũ�� </param>
    public void SetSFXBolumes(float volumes)
    {
        gameManager.SFXVolumes = volumes;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.SetBgmVolume(gameManager.SFXVolumes);
    }

    /// <summary>
    /// ���α׷��� �����մϴ�.
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

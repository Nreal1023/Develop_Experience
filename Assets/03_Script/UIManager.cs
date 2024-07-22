using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �� ����
using System.Collections;

public class UIManager : MonoBehaviour
{
    public CanvasGroup mainUIPanel; // ���� UI �г��� CanvasGroup
    public GameObject settingsPanel; // ���� �г�
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton; // '������' ��ư

    public PlayerMovement playerMovement; // �÷��̾��� ������ ��ũ��Ʈ

    private void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ���
        startButton.onClick.AddListener(OnStartButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked); // '������' ��ư �̺�Ʈ ���

        // �ʱ� ���� ����
        playerMovement.enabled = false;
        settingsPanel.SetActive(false);
    }

    private void OnStartButtonClicked()
    {
        StartCoroutine(FadeOutAndStartGame());
    }

    private IEnumerator FadeOutAndStartGame()
    {
        float duration = 1f; // ���̵� �ƿ� ���� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mainUIPanel.alpha = 1f - Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        mainUIPanel.gameObject.SetActive(false);
        playerMovement.enabled = true; // �÷��̾� ������ Ȱ��ȭ
    }

    private void OnSettingsButtonClicked()
    {
        settingsPanel.SetActive(true);
    }
    private void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ����� ���ӿ��� ����
#endif
    }

    public void OnBackButtonClicked()
    {
        // ���� �г��� ��Ȱ��ȭ�մϴ�.
        settingsPanel.SetActive(false);
    }
}
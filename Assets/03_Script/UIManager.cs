using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // 씬 관리
using System.Collections;

public class UIManager : MonoBehaviour
{
    public CanvasGroup mainUIPanel; // 메인 UI 패널의 CanvasGroup
    public GameObject settingsPanel; // 설정 패널
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton; // '나가기' 버튼

    public PlayerMovement playerMovement; // 플레이어의 움직임 스크립트

    private void Start()
    {
        // 버튼 클릭 이벤트 등록
        startButton.onClick.AddListener(OnStartButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked); // '나가기' 버튼 이벤트 등록

        // 초기 상태 설정
        playerMovement.enabled = false;
        settingsPanel.SetActive(false);
    }

    private void OnStartButtonClicked()
    {
        StartCoroutine(FadeOutAndStartGame());
    }

    private IEnumerator FadeOutAndStartGame()
    {
        float duration = 1f; // 페이드 아웃 지속 시간
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mainUIPanel.alpha = 1f - Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }

        mainUIPanel.gameObject.SetActive(false);
        playerMovement.enabled = true; // 플레이어 움직임 활성화
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
        Application.Quit(); // 빌드된 게임에서 종료
#endif
    }

    public void OnBackButtonClicked()
    {
        // 설정 패널을 비활성화합니다.
        settingsPanel.SetActive(false);
    }
}
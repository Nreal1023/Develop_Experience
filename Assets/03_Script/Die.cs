using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요
using UnityEngine.UI; // UI 제어에 필요

public class Die : MonoBehaviour
{
    public GameObject gameOverPanel; // 패널 참조
    public GameObject scoreText; // 스코어 참조

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("충돌");
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("게임 종료: 충돌한 오브젝트 태그는 Car입니다.");
            ShowGameOverPanel();
        }
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        scoreText.SetActive(false);
        GetComponent<PlayerMovement>().enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
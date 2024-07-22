using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ �ʿ�
using UnityEngine.UI; // UI ��� �ʿ�

public class Die : MonoBehaviour
{
    public GameObject gameOverPanel; // �г� ����
    public GameObject scoreText; // ���ھ� ����

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("�浹");
        if (collision.gameObject.CompareTag("Car"))
        {
            Debug.Log("���� ����: �浹�� ������Ʈ �±״� Car�Դϴ�.");
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
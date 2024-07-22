using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽�
using DG.Tweening; // DOTween ���ӽ����̽�

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 10.0f;
    public float moveDuration = 0.5f; // �̵� �ִϸ��̼��� ���� �ð�
    public float bounceStrength = 0.5f; // ƨ��� ����

    public delegate void MoveAction();
    public static event MoveAction OnMove;

    public InfinityMap tileManager;

    public TMP_Text scoreText1; // ù ��° ���� �ؽ�Ʈ
    public TMP_Text scoreText2; // �� ��° ���� �ؽ�Ʈ

    private int score = 0; // ���� ����

    private Animator animator; // Animator ������Ʈ

    private bool isMoving = false; // ���� �̵� ������ ����

    void Start()
    {
        // ������ UI �ؽ�Ʈ�� �ݿ�
        UpdateScoreUI();

        // Animator ������Ʈ �ʱ�ȭ
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            // �̵� ���̶�� �ִϸ��̼� ���¸� ������Ʈ
            UpdateAnimationState(true);
            return;
        }

        bool moveRequested = false;

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.back);
            moveRequested = true;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.right);
            tileManager?.SpawnTile();
            IncreaseScore(); // ���� ����
            moveRequested = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.forward);
            moveRequested = true;
        }

        // �ִϸ��̼� ���� ������Ʈ
        if (!moveRequested)
        {
            UpdateAnimationState(false);
        }
    }

    void Move(Vector3 direction)
    {
        if (isMoving)
        {
            return; // ���� �̵� ���̸� ���ο� �̵� ��û�� ����
        }

        Vector3 targetPosition = transform.position + direction * moveDistance;
        isMoving = true; // �̵� �� ���� ����

        // DOTween�� ����Ͽ� �ε巴�� ƨ����� �̵�
        transform.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.InOutSine) // �ε巯�� �ִϸ��̼�
            .OnComplete(() => {
                isMoving = false; // �̵� �Ϸ� �� ���� ������Ʈ
                UpdateAnimationState(false);
            });

        OnMove?.Invoke();
    }

    void IncreaseScore()
    {
        score += 1; // ���� 1�� ����
        UpdateScoreUI(); // ���� UI ������Ʈ
    }

    void UpdateScoreUI()
    {
        if (scoreText1 != null)
        {
            scoreText1.text = "" + score; // ù ��° �ؽ�Ʈ ������Ʈ
        }
        if (scoreText2 != null)
        {
            scoreText2.text = "" + score; // �� ��° �ؽ�Ʈ ������Ʈ
        }
    }

    void UpdateAnimationState(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving); // isMoving �Ķ���� ����
            animator.SetBool("isIDLE", !isMoving); // isIDLE �Ķ���� ����
        }
    }
}

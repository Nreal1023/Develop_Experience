using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스
using DG.Tweening; // DOTween 네임스페이스

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 10.0f;
    public float moveDuration = 0.5f; // 이동 애니메이션의 지속 시간
    public float bounceStrength = 0.5f; // 튕기는 강도

    public delegate void MoveAction();
    public static event MoveAction OnMove;

    public InfinityMap tileManager;

    public TMP_Text scoreText1; // 첫 번째 점수 텍스트
    public TMP_Text scoreText2; // 두 번째 점수 텍스트

    private int score = 0; // 점수 변수

    private Animator animator; // Animator 컴포넌트

    private bool isMoving = false; // 현재 이동 중인지 여부

    void Start()
    {
        // 점수를 UI 텍스트에 반영
        UpdateScoreUI();

        // Animator 컴포넌트 초기화
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            // 이동 중이라면 애니메이션 상태를 업데이트
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
            IncreaseScore(); // 점수 증가
            moveRequested = true;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.forward);
            moveRequested = true;
        }

        // 애니메이션 상태 업데이트
        if (!moveRequested)
        {
            UpdateAnimationState(false);
        }
    }

    void Move(Vector3 direction)
    {
        if (isMoving)
        {
            return; // 현재 이동 중이면 새로운 이동 요청을 무시
        }

        Vector3 targetPosition = transform.position + direction * moveDistance;
        isMoving = true; // 이동 중 상태 설정

        // DOTween을 사용하여 부드럽고 튕기듯이 이동
        transform.DOMove(targetPosition, moveDuration)
            .SetEase(Ease.InOutSine) // 부드러운 애니메이션
            .OnComplete(() => {
                isMoving = false; // 이동 완료 후 상태 업데이트
                UpdateAnimationState(false);
            });

        OnMove?.Invoke();
    }

    void IncreaseScore()
    {
        score += 1; // 점수 1점 증가
        UpdateScoreUI(); // 점수 UI 업데이트
    }

    void UpdateScoreUI()
    {
        if (scoreText1 != null)
        {
            scoreText1.text = "" + score; // 첫 번째 텍스트 업데이트
        }
        if (scoreText2 != null)
        {
            scoreText2.text = "" + score; // 두 번째 텍스트 업데이트
        }
    }

    void UpdateAnimationState(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving); // isMoving 파라미터 설정
            animator.SetBool("isIDLE", !isMoving); // isIDLE 파라미터 설정
        }
    }
}

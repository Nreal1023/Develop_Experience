using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Transform startPoint; // 출발 지점
    private Transform endPoint; // 도착 지점
    private float speed; // 차량 속도

    private bool isInitialized = false; // 초기화 여부

    public void Initialize(Transform start, Transform end, float speed)
    {
        if (isInitialized) return; // 이미 초기화된 경우, 중복 초기화 방지

        startPoint = start;
        endPoint = end;

        this.speed = speed; // 차량 속도를 설정

        // 차량의 초기 위치를 출발 지점으로 설정
        transform.position = startPoint.position;

        isInitialized = true; // 초기화 완료
    }

    void Update()
    {
        if (!isInitialized) return;

        // 출발 지점에서 도착 지점으로 차량을 이동시킴
        MoveTowardsEndPoint();
    }

    void MoveTowardsEndPoint()
    {
        // 차량을 도착 지점으로 이동
        float step = speed * Time.deltaTime; // 이동 거리
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, step);

        // 도착 지점에 도달하면 차량 삭제
        if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
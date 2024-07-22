using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private Transform startPoint; // ��� ����
    private Transform endPoint; // ���� ����
    private float speed; // ���� �ӵ�

    private bool isInitialized = false; // �ʱ�ȭ ����

    public void Initialize(Transform start, Transform end, float speed)
    {
        if (isInitialized) return; // �̹� �ʱ�ȭ�� ���, �ߺ� �ʱ�ȭ ����

        startPoint = start;
        endPoint = end;

        this.speed = speed; // ���� �ӵ��� ����

        // ������ �ʱ� ��ġ�� ��� �������� ����
        transform.position = startPoint.position;

        isInitialized = true; // �ʱ�ȭ �Ϸ�
    }

    void Update()
    {
        if (!isInitialized) return;

        // ��� �������� ���� �������� ������ �̵���Ŵ
        MoveTowardsEndPoint();
    }

    void MoveTowardsEndPoint()
    {
        // ������ ���� �������� �̵�
        float step = speed * Time.deltaTime; // �̵� �Ÿ�
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, step);

        // ���� ������ �����ϸ� ���� ����
        if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
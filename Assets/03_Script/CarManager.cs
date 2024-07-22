using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject[] carPrefabs; // ���� ������ �迭
    public Transform carSpawn1; // ���� ���� ���� 1
    public Transform carSpawn2; // ���� ���� ���� 2

    public float minSpawnInterval = 0.5f; // �ּ� ��ȯ �ֱ�
    public float maxSpawnInterval = 2f; // �ִ� ��ȯ �ֱ�
    public int initialCarCount = 5; // �ʱ� ������ ���� ����

    public float minCarSpeed = 20f; // ���� �ּ� �ӵ�
    public float maxCarSpeed = 40f; // ���� �ִ� �ӵ�

    private float timer; // ���� Ÿ�̸�
    private float carSpeed; // ���� �ӵ� (��� ������ �����ϰ� ����� �ӵ�)

    void Start()
    {
        // ���� ���� �ӵ� ����
        carSpeed = Random.Range(minCarSpeed, maxCarSpeed);

        // �ʱ� ���� ����
        for (int i = 0; i < initialCarCount; i++)
        {
            SpawnCar();
        }

        // ���� Ÿ�̸� �ʱ�ȭ
        timer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Ÿ�̸� ������Ʈ
        timer -= Time.deltaTime;

        // Ÿ�̸Ӱ� 0 ���ϰ� �Ǹ� ������ �����ϰ� Ÿ�̸Ӹ� ����
        if (timer <= 0f)
        {
            SpawnCar();
            // ���ο� ���� �ֱ�� Ÿ�̸� ����
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnCar()
    {
        // CarManager�� �ִ� ������Ʈ�� �±׿� ���� ��� ���� ����
        Transform startPoint = null;
        Transform endPoint = null;

        if (CompareTag("LeftRoad"))
        {
            startPoint = carSpawn1;
            endPoint = carSpawn2;
        }
        else if (CompareTag("RightRoad"))
        {
            startPoint = carSpawn2;
            endPoint = carSpawn1;
        }
        else
        {
            Debug.LogWarning("Invalid CarManager tag. Use 'LeftRoad' or 'RightRoad'.");
            return;
        }

        // �����ϰ� ���� �������� ����
        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // ���� ����
        GameObject car = Instantiate(carPrefab, startPoint.position, Quaternion.identity);

        // ������ ���� ����
        if (startPoint == carSpawn1)
        {
            car.transform.rotation = Quaternion.Euler(0, 180, 0); // Y�� 180���� ȸ��
        }

        // CarMovement ��ũ��Ʈ�� ������ ��� ������ ���� ���� �� �ӵ� ����
        CarMovement carMovement = car.GetComponent<CarMovement>();
        if (carMovement != null)
        {
            carMovement.Initialize(startPoint, endPoint, carSpeed); // ��� ������ ������ �ӵ� ����
        }
    }
}

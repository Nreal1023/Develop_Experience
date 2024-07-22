using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject[] carPrefabs; // 차량 프리팹 배열
    public Transform carSpawn1; // 차량 스폰 지점 1
    public Transform carSpawn2; // 차량 스폰 지점 2

    public float minSpawnInterval = 0.5f; // 최소 소환 주기
    public float maxSpawnInterval = 2f; // 최대 소환 주기
    public int initialCarCount = 5; // 초기 생성할 차량 개수

    public float minCarSpeed = 20f; // 차량 최소 속도
    public float maxCarSpeed = 40f; // 차량 최대 속도

    private float timer; // 생성 타이머
    private float carSpeed; // 차량 속도 (모든 차량에 동일하게 적용될 속도)

    void Start()
    {
        // 최초 차량 속도 설정
        carSpeed = Random.Range(minCarSpeed, maxCarSpeed);

        // 초기 차량 생성
        for (int i = 0; i < initialCarCount; i++)
        {
            SpawnCar();
        }

        // 생성 타이머 초기화
        timer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // 타이머 업데이트
        timer -= Time.deltaTime;

        // 타이머가 0 이하가 되면 차량을 생성하고 타이머를 리셋
        if (timer <= 0f)
        {
            SpawnCar();
            // 새로운 랜덤 주기로 타이머 설정
            timer = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnCar()
    {
        // CarManager가 있는 오브젝트의 태그에 따라 출발 지점 결정
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

        // 랜덤하게 차량 프리팹을 선택
        GameObject carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // 차량 생성
        GameObject car = Instantiate(carPrefab, startPoint.position, Quaternion.identity);

        // 차량의 방향 설정
        if (startPoint == carSpawn1)
        {
            car.transform.rotation = Quaternion.Euler(0, 180, 0); // Y축 180도로 회전
        }

        // CarMovement 스크립트에 도로의 출발 지점과 도착 지점 및 속도 설정
        CarMovement carMovement = car.GetComponent<CarMovement>();
        if (carMovement != null)
        {
            carMovement.Initialize(startPoint, endPoint, carSpeed); // 모든 차량에 동일한 속도 적용
        }
    }
}

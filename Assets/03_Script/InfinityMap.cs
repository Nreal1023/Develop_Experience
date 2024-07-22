using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityMap : MonoBehaviour
{
    public GameObject[] tilePrefabs; // 타일 프리팹 배열
    public int initialTileCount = 20; // 초기 생성할 타일 개수
    private Vector3 lastTilePosition; // 마지막 타일의 위치

    void Start()
    {
        lastTilePosition = Vector3.zero;

        Instantiate(tilePrefabs[0], lastTilePosition, Quaternion.identity);

        for (int i = 1; i < initialTileCount; i++)
        {
            Vector3 newTilePosition = lastTilePosition + new Vector3(10, 0, 0);

            InstantiateRandomTile(newTilePosition);

            lastTilePosition = newTilePosition;

            Debug.Log($"Initial Tile {i + 1}: {newTilePosition}");
        }
    }

    public void SpawnTile()
    {
        Vector3 newTilePosition = lastTilePosition + new Vector3(10, 0, 0);

        InstantiateRandomTile(newTilePosition);

        lastTilePosition = newTilePosition;

        Debug.Log("SpawnTile called. New Tile Position: " + newTilePosition);
    }

    void InstantiateRandomTile(Vector3 position)
    {
        // 타일 프리팹 배열에서 랜덤하게 선택
        GameObject selectedTile = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
        Instantiate(selectedTile, position, Quaternion.identity);

        Debug.Log($"Random Tile Instantiated at: {position}");
    }
}
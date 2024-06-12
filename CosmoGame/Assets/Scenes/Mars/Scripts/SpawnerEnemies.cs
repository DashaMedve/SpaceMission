using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �����
    public Transform[] spawnPoints; // ����� ������
    public float spawnInterval = 5f; // �������� ������

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = new Vector3(spawnPoints[spawnIndex].position.x, spawnPoints[spawnIndex].position.y, 0); // ��������� ���������� Z �� 0
            Instantiate(enemyPrefab, spawnPosition, spawnPoints[spawnIndex].rotation);
        }
    }
}

using System.Collections; // ѕодключение библиотеки дл€ использовани€ корутин
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ѕрефаб врага, который будет спавнитьс€
    public Transform[] spawnPoints; // ћассив точек спавна врагов
    public float spawnInterval = 5f; // »нтервал между спавнами врагов в секундах

    void Start()
    {
        // «апускаем корутину дл€ спавна врагов
        StartCoroutine(SpawnEnemies());
        // орутины работают путем выполнени€ части своего кода, затем приостановки (yield) и возобновлени€ через определенный интервал времени.
    }

    //  орутин дл€ спавна врагов
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // ∆дем указанный интервал времени перед спавном следующего врага
            yield return new WaitForSeconds(spawnInterval);

            // ¬ыбираем случайный индекс из массива точек спавна
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            // ќпредел€ем позицию спавна на основе выбранного индекса
            Vector3 spawnPosition = new Vector3(
                spawnPoints[spawnIndex].position.x,
                spawnPoints[spawnIndex].position.y,
                0 // ”становка координаты Z на 0
            );

            // —оздаем врага в выбранной позиции и с выбранной ориентацией
            Instantiate(enemyPrefab, spawnPosition, spawnPoints[spawnIndex].rotation);
        }
    }
}

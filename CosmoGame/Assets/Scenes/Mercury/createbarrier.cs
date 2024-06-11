using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class createbarrier : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Шаблоны объектов для генерации
    public float timeSpawn; // Интервал времени между созданием объектов

    private GameObject star1; // Переменная для хранения звезды
    private GameObject star2; // Переменная для хранения звезды
    private bool creat; // Переменная для проверки создались ли звёзды в определённой промежуток времени
    private float timer; // Таймер считающий от timeSpawn до 0

    /**
     * Инициализация переменных класса
     **/
    void Start()
    {
        timeSpawn = 6f;
        creat = false;
        timer = timeSpawn;
    }

    /**
     * Создание объекта "Star"
     * 
     * @param star - переменная в которой будет создаваться новый объект "Star"
     * @param position - вектор координат нового объекта "Star"
     **/
    void init_star(GameObject star, Vector3 position)
    {
        Collider Collider; // Создание коллайдера
        star = Instantiate(enemyPrefab[3], position, Quaternion.identity); // Создание объекта "Star"
        star.transform.localScale = new Vector3(15f, 15f, 15f); // Изменение размера объекта "Star"
        star.AddComponent<rotatebarrier>(); // Добавления компонента "rotatebarrier" к объекту "Star"
        star.AddComponent<Star>(); // Добавление компонента "Star" к объекту "Star"
        star.AddComponent<BoxCollider>(); // Добавление компонента "BoxCollider" к объекту "Star"
        Collider = star.GetComponent<BoxCollider>();
        Collider.isTrigger = true; // Изменение режима коллайдера
    }

    /**
     * Обновление кадра
     **/
    void Update()
    {
        timer -= Time.deltaTime; // Изменение значения таймера
        System.Random random_generator1 = new System.Random(); // Создание объекта, генерирующего случайные значения
        System.Random random_generator2 = new System.Random(); // Создание объекта, генерирующего случайные значения
        if (timer <= 3) // Условие для того, чтобы звёзды не столкнулись с барьером
        {
            if (!creat) // Условие для создания только один раз за один timer
            {
                var random_spawn_star = random_generator2.Next(0, 3); // Генерация индекса дороги
                Vector3 position1, position2;
                int pos = 0;
                for (int i = 0; i < 3; i++) // На дороге random_spawn_star создаётся три раза по две звезды
                {
                    if (random_spawn_star == 0)
                    {
                        position1 = transform.position - new Vector3(91, -30f, pos);
                        position2 = transform.position - new Vector3(91, -30f, -pos);
                    }
                    else if (random_spawn_star == 1)
                    {
                        position1 = transform.position + new Vector3(0, 30f, pos);
                        position2 = transform.position + new Vector3(0, 30f, -pos);
                    }
                    else
                    {
                        position1 = transform.position + new Vector3(91, 30f, pos);
                        position2 = transform.position + new Vector3(91, 30f, -pos);
                    }
                    init_star(star1, position1);
                    init_star(star2, position2);
                    pos -= 350; // Изменение переменной, задающей позицию звёзд 
                }
                creat = true;
            }
        }
        if (timer <= 0) // Если timer досчитал до 0
        {
            timer = timeSpawn; // Запуск таймера сначала
            var random = random_generator1.Next(0,3); // Генерация случайного индекса для генерации препятствия

            // Генерация трёх объектов Downbarrier
            if (random == 0)
            {
                Instantiate(enemyPrefab[0], transform.position + new Vector3(0, -8, 0), Quaternion.identity);
                Instantiate(enemyPrefab[0], transform.position + new Vector3(91, -8, 0), Quaternion.identity);
                Instantiate(enemyPrefab[0], transform.position - new Vector3(91, 8, 0), Quaternion.identity);
            }

            else if (random == 1)
            {
                List<int> road = new List<int>() { 0, 1, 2 };
                var randomroad1 = random_generator1.Next(0, 3); // Генерация индекса дороги
                road.RemoveAt(randomroad1);
                var randomroad2 = random_generator1.Next(0, 2); // Генерация индекса дороги
                road.RemoveAt(randomroad2);

                // Генерация двух объектов "asteroid"
                if (road[0] == 0)
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(0, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(91, 70, 0), Quaternion.identity);
                }

                else if (road[0] == 1)
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(-91, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(91, 70, 0), Quaternion.identity);
                }

                else
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(0, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(-91, 70, 0), Quaternion.identity);
                }
            }

            // Генерация объекта "BigBarrier"
            else
            {
                var random_place = random_generator1.Next(-91, 92);
                Instantiate(enemyPrefab[2], transform.position + new Vector3(random_place, 70, 0), Quaternion.identity);
            }
            creat = false;
        }
    }
}

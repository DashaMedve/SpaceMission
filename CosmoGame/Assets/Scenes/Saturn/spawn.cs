using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System;
using UnityEngine.UI;

public class spawn : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Шаблоны объектов для генерации
    public float timeSpawn; // Интервал времени между созданием объектов

    private bool creat; // Переменная для проверки создались ли звёзды в определённой промежуток времени
    private GameObject star; // Переменная для хранения звезды
    private float timer; // Таймер считающий от timeSpawn до 0
    private System.Random random; // Создание объекта, генерирующего случайные значения

    /**
     * Инициализация переменных класса
     **/
    void Start()
    {
        timeSpawn = 4f;
        timer = timeSpawn;
        creat = false;
        random = new System.Random();
    }

    /**
     * Обновление кадра
     **/
    void Update()
    {
        timer -= Time.deltaTime; // Изменение значения таймера

        Collider Collider; // Создание коллайдера
        if (timer <= 2)
        {
            if (!creat)
            {
                var randomx = random.Next(-150, 150); // Случайная генерация координаты x
                var randomy = random.Next(-34, 34); // Случайная генерация координаты y
                star = Instantiate(enemyPrefab[0], transform.position + new Vector3(randomx, randomy, 0), Quaternion.identity); // Создание объекта "Star"
                star.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f); // Изменение размера объекта "Star"
                star.AddComponent<Star>(); // Добавление компонента "Star" к объекту "Star"
                star.AddComponent<BoxCollider>(); // Добавление компонента "BoxCollider" к объекту "Star"
                star.AddComponent<create>(); // Добавление компонента "create" к объекту "Star"
                Collider = star.GetComponent<BoxCollider>();
                Collider.isTrigger = true; // Изменение режима коллайдера
                creat = true;
            }
        }
        if (timer <= 0)
        {
            timer = timeSpawn; // Запуск таймера сначала
            var randomx = random.Next(-150, 150); // Случайная генерация координаты x
            var randomy = random.Next(-34, 34); // Случайная генерация координаты y
            star = Instantiate(enemyPrefab[1], transform.position + new Vector3(randomx, randomy, 0), Quaternion.identity); // Создание объекта "Rings"
            star.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f); // Изменение размера объекта "Rings"
            star.transform.Rotate(-253.5f, 0.6f, 0, 0); // Поворот объекта "Rings"
            star.AddComponent<Ring>(); // Добавление компонента "Ring" к объекту "Rings"
            star.AddComponent<BoxCollider>(); // Добавление компонента "BoxCollider" к объекту "Rings"
            star.AddComponent<create>(); // Добавление компонента "create" к объекту "Rings"
            Collider = star.GetComponent<BoxCollider>();
            Collider.isTrigger = true; // Изменение режима коллайдера
            creat = false;
        }
    }
}

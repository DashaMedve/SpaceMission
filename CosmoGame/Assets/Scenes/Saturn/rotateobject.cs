using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create : MonoBehaviour
{
    private float timer; // Время движения объекта

    /**
     * Инициализация переменных класса
     **/
    void Start()
    {
        timer = 7f;
    }

    /**
     * Обновление кадра
     **/
    void Update()
    {
        timer -= Time.deltaTime; // Изменение времени
        if (timer <= 0) // Время движения объекта закончилось
            Destroy(gameObject); // Уничтожение объекта
        transform.position -= new Vector3(0, 0, 3f); // Изменение позиции объекта
    }
}
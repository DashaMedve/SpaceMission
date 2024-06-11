using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatebarrier : MonoBehaviour
{
    private float timer; // Время движения объекта

    /**
     * Инициализация переменных класса
     **/
    void Start()
    {
        timer = 6f;
    }

    /**
     * Реализация движения препятствий в сторону объекта "Player"
     **/
    void Update()
    {
        timer -= Time.deltaTime; // Изменение времени
        if (timer <= 0) // Время движения объекта закончилось
            Destroy(gameObject); // Уничтожение объекта
        transform.position -= new Vector3(0, 0, 8f); // Изменение позиции объекта
    }
}

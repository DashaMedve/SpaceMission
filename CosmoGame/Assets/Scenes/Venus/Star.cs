using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    private TMP_Text ScoreText; // Переменная для вывода количества очков

    /**
     * Инициализация переменных класса
     **/
    private void Start()
    {
        ScoreText = GameObject.Find("Counter").GetComponent<TMP_Text>();
    }

    /**
     * Обновление кадра
     **/
    private void Update()
    {
        this.transform.Rotate(0,4f,0); // Вращение объекта "Star"
    }

    /**
     * Обработка столкновения "Star" c объектом "Player"
     **/
    private void OnTriggerEnter(Collider other)
    {
        int score = Convert.ToInt32(ScoreText.text);
        score += 1; // Изменение значение счётчика
        ScoreText.text = score.ToString();
        Destroy(this.gameObject); // Уничтожение объекта "Star"
    }
}

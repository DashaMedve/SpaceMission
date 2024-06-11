using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ring : MonoBehaviour
{
    private TMP_Text ScoreText; // Переменная для хранения и вывода текста о количестве собранных объектов "Rings"

    /**
     * Инициализация переменных класса
     **/
    void Start()
    {
        ScoreText = GameObject.Find("ScoreRing").GetComponent<TMP_Text>();
    }

    /**
     * Вызывается при прохождении объекта "Player" через объект "Rings"
     * 
     * @param other объект, с которым произошло столкновение
     **/
    private void OnTriggerEnter(Collider other)
    {
        int score = Convert.ToInt32(ScoreText.text);
        score += 1; // Изменение количества очков
        ScoreText.text = score.ToString();
        Destroy(this.gameObject); // Разрушение объекта
    }
}

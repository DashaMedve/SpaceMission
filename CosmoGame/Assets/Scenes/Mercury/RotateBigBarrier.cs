using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RotateBigBarrier : MonoBehaviour
{
    public Rigidbody rb; // // Физические свойства объекта "BigBarrier"
    private GameObject create; // Переменная для хранения create object
    private bool state; // Переменная отвечающая за направление объекта "BigBarrier"

    /**
     * Инициализация переменных класса
     **/
    void Start()
    {
        state = true;
        create = GameObject.Find("CreateBarrier");
    }

    /**
     * Обновление кадра
     * Реализация движения объекта "BigBarrier" в стороны
     **/
    void Update()
    {
        if (state) // Выбор направления движения объекта "BigBarrier"
        {
            if (transform.position.x - create.transform.position.x < 90)
                transform.position += new Vector3(1f, 0, 0); // Изменение положения объекта "BigBarrier"
            else
            {
                state = false; // Изменение направления движения объекта "BigBarrier"
                rb.velocity = Vector3.zero; // Зануление вектора скорости по всем осям объекта "BigBarrier"
            }
        }
        else
        {
            if (transform.position.x - create.transform.position.x > -90)
                transform.position -= new Vector3(1f, 0, 0); // Изменение положения объекта "BigBarrier"
            else
            {
                state = true; // Изменение направления движения объекта "BigBarrier"
                rb.velocity = Vector3.zero; // Зануление вектора скорости по всем осям объекта "BigBarrier"
            }
        }
    }
}

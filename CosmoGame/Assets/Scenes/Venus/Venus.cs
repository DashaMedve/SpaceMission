using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Venus : MonoBehaviour
{
    public Rigidbody rb; // Физические свойства объекта "Player"
    public float runSpeed; // Скорость движения объекта "Player"
    public float strafeSpeed; // Скорость поворота объекта "Player"

    protected bool strafeLeft; // Переменная показывающая нажата ли кнопка, отвечающая за поворот налево объекта "Player"
    protected bool strafeRight; // Переменная показывающая нажата ли кнопка, отвечающая за поворот направо объекта "Player"
    protected bool forward; // Переменная показывающая нажата ли кнопка, отвечающая за шаг вперёд объекта "Player"
    protected bool backward; // Переменная показывающая нажата ли кнопка, отвечающая за шаг назад объекта "Player"

    /**
     * Инициализация переменных класса
     **/
    void Start()
    {
        runSpeed = 100f;
        strafeSpeed = 100f;
        Time.timeScale = 0;
        GameObject.Find("Player/astronout/Canvas/Start/ButtonStart").GetComponent<Button>().onClick.AddListener(press_start);
        strafeLeft = false;
        strafeRight = false;
        forward = false;
        backward = false;
    }

    /**
     * Запуск игры
     **/
    private void press_start()
    {
        GameObject.Find("Player/astronout/Canvas/Start").SetActive(false);
        Time.timeScale = 1;
    }
    /**
     * Обновление кадра
     * Проверки на нажатия кнопок и изменение переменных отвечающих за соответствующие нажатия
     **/
    void Update()
    {
        if (Input.GetKey("d"))
            strafeRight = true; 
        else
            strafeRight = false;
     
        if (Input.GetKey("a"))
            strafeLeft = true;
        else
            strafeLeft = false; 

        if (Input.GetKey("w"))
            forward = true;
        else
            forward= false;

        if(Input.GetKey("s"))
            backward = true; 
        else
            backward = false;
    }

    /**
     *  Обновление физических параметров
     **/
    void FixedUpdate()
    {
        if (strafeLeft)
            rb.transform.Rotate(Vector3.down,  strafeSpeed * Time.deltaTime); // Поворот налево

        if (strafeRight)
            rb.transform.Rotate(Vector3.up, strafeSpeed * Time.deltaTime); // Поворот направо

        if (backward)
            rb.AddForce(-rb.transform.forward * runSpeed * Time.deltaTime * 100f, ForceMode.Impulse); // Шаг назад

        if (forward)
            rb.AddForce(rb.transform.forward*runSpeed* Time.deltaTime*100f, ForceMode.Impulse); // Шаг вперёд
        rb.velocity = Vector3.zero; // Зануление скорости по всем осям
    }
}

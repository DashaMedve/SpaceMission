using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rotatefly : MonoBehaviour
{
    public Rigidbody rb; // Физические свойства объекта "Player"
    public float strafeSpeed; // Скорость движения в стороны объекта "Player"

    protected bool strafeLeft; // Переменная показывающая нажата ли кнопка, отвечающая за поворот налево объекта "Player"
    protected bool strafeRight; // Переменная показывающая нажата ли кнопка, отвечающая за поворот направо объекта "Player"
    protected bool up; // Переменная показывающая нажата ли кнопка, отвечающая за подъём объекта "Player"
    protected bool down; // Переменная показывающая нажата ли кнопка, отвечающая за передвижение объекта "Player" вниз

    private GameObject final;
    private TMP_Text ScoreText;

    /**
     * Инициализация переменных класса
     **/
    void Start()
    {
        Time.timeScale = 1;
        strafeSpeed = 7500f;
        strafeLeft = false;
        strafeRight = false;
        ScoreText = GameObject.Find("ScoreRing").GetComponent<TMP_Text>();
        up = false;
    }

    /**
    * Срабатывает при нажатии кнопки "Вернуться в основное меню"
    * Открывает сцену меню
    **/
    void press()
    {
        SceneManager.LoadScene(0);
    }

    /**
     * Обновление кадра
     * Проверки на нажатия кнопок и изменение переменных отвечающих за соответствующие нажатия
     **/
    void Update()
    {
        int score = Convert.ToInt32(ScoreText.text);
        if (score >= 5) // Если собрано 5 колец, то игра заканчивается и выводится окно с результатами
        {
            Time.timeScale = 0;
            final = GameObject.Find("Player/Canvas/Final");
            final.SetActive(true);
            GameObject.Find("Player/Canvas/Final/Scr").GetComponent<Text>().text = "Вы собрали: " + GameObject.Find("Player/Canvas/Counter").GetComponent<TMP_Text>().text + " звездочек";
            GameObject.Find("Player/Canvas/Final/Buttonfin").GetComponent<Button>().onClick.AddListener(press);
        }
        if (Input.GetKey("d"))
            strafeRight = true;
        else
            strafeRight = false;

        if (Input.GetKey("a"))
            strafeLeft = true;
        else
            strafeLeft = false;

        if (Input.GetKey("w"))
            up = true;
        else
            up = false;
        if (Input.GetKey("s"))
            down = true;
        else
            down = false;
    }

    /**
    *  Обновление физических параметров
    **/
    void FixedUpdate()
    {
        if (strafeLeft)
            rb.AddForce(-rb.transform.right * strafeSpeed * Time.deltaTime, ForceMode.Impulse); // Поворот налево

        if (strafeRight)
            rb.AddForce(rb.transform.right * strafeSpeed * Time.deltaTime, ForceMode.Impulse); // Поворот направо

        if (up)
            rb.AddForce(rb.transform.up * strafeSpeed * Time.deltaTime, ForceMode.Impulse); // Движение наверх

        if (down)
            rb.AddForce(-rb.transform.up * strafeSpeed * Time.deltaTime, ForceMode.Impulse); // Движение вниз

        rb.velocity = Vector3.zero;
    }
}

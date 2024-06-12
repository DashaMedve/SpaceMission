using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Rotate : MonoBehaviour
{
    public Rigidbody rb; // Физические свойства объекта "Player"
    public float JumpForce; // Сила прыжка объекта "Player"

    private bool jump; // Переменная, отвечающая за прыжок объекта "Player". True, если кнопка "space" нажата 
    private int state; // Номер полосы, на которой находится объект "Player"
    private TMP_Text KmText; // Переменная для хранения и вывода текста о количестве пройденных километров объектом "Player"
    private bool isGrounded; // Переменная, в которой хранится положение объекта "Player" (если объект находится на поверхности планеты - true, если в прыжке - false)
    private Queue<int> queue = new Queue<int>(); // Очередь для хранения команд пользователя (нажатие кнопок)
   
    /**
     * Инициализация переменных класса
     **/
    private void Start()
    {
        Physics.gravity = new Vector3(0, -60f, 0); //Изменение гравитации
        state = 1;
        JumpForce = 1000f;
        jump = false;
        isGrounded = true;
        KmText = GameObject.Find("Player/Canvas/Km").GetComponent<TMP_Text>(); // Нахождение элемента текста о количестве пройденных километров
        Time.timeScale = 0;
        GameObject.Find("Player/Canvas/Start/ButtonStart").GetComponent<Button>().onClick.AddListener(press_start);
    }

    /**
     * Запуск игры
     **/
    void press_start()
    {
        GameObject.Find("Player/Canvas/Start").SetActive(false);
        Time.timeScale = 1;
    }

    /**
     * Функция для плавного перемещения объекта "Player" с одной полосы на другую
     * 
     * @param target - точка, к которой нужно двигаться объекту "Player"
     * @param delta - время, за которое нужно передвинуться объекту "Player" в точку target
     **/
    IEnumerator SmoothMove(Vector3 target, float delta)
    {
        float closeEnough = 0.2f; // Погрешность расстояния между target и объектом "Player"
        float distance = (rb.transform.position - target).magnitude; // Расчёт расстояния, которое нужно пройти с использованием магнитуды

        while (distance >= closeEnough) // Пока объект не достиг "Player" target
        {
            transform.position = Vector3.Lerp(new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z), target, delta); // Плавное перемещение объекта
            yield return StartCoroutine(SmoothMove(target, delta)); // Вызов данной функции для следующего кадра
            distance = (rb.transform.position - target).magnitude; // Расчёт расстояния, которое нужно пройти с использованием магнитуды
        }
        rb.transform.position = target; // Перемещение объекта "Player" в позицию target
    }

    /**
     * Перемещение объекта в одну из сторон
     * 
     * @param - направление движения объекта "Player"
     **/
    void Clicked(bool direction)
    {
        // Объявление и инициализация вектора перемещения
        Vector3 relativeLocation;
        if (direction)
            relativeLocation = new Vector3(-97, 0, 0);
        else
            relativeLocation = new Vector3(97, 0, 0);

        Vector3 targetLocation = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z) + relativeLocation; // Вычисление новой позиции объекта "Player"
        float timeDelta = 0.4f; // Время, за которое нужно передвинуться объекту "Player" в точку targetLocation
        this.StartCoroutine(SmoothMove(targetLocation, timeDelta)); // Вызов функции реализующей перемещение
    }

    /**
     * Срабатывает при нажатии кнопки "Начать сначала"
     * Запускает игру заново
     **/
    void press_die()
    {
        GameObject.Find("Player/Canvas/Die").SetActive(false);
        Physics.gravity = new Vector3(0, 0, 0); //Изменение гравитации
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /**
     * Срабатывает при нажатии кнопки "Вернуться в основное меню"
     * Открывает сцену меню
     **/
    void press_exit()
    {
        GameObject.Find("Player/Canvas/Final").SetActive(false);
        Physics.gravity = new Vector3(0, 0f, 0); //Изменение гравитации
        SceneManager.LoadScene("Space");
    }

    /**
     * Обновление кадра
     **/
    void Update()
    {
        // Изменение количества пройденных километров
        double score = double.Parse(KmText.text);
        score += 1; 
        KmText.text = score.ToString();

        if (score >= 10000) // Проверка количества пройденных километров
        {
            // Вывод результатов игры
            Time.timeScale = 0; // Пауза
            GameObject.Find("Player/Canvas/Final").SetActive(true);
            GameObject.Find("Player/Canvas/Final/line2").GetComponent<TMP_Text>().text = "Вы собрали " + 
                GameObject.Find("Player/Canvas/Counter").GetComponent<TMP_Text>().text + " звездочек";
            GameObject.Find("Player/Canvas/Final/ButtonFinal").GetComponent<Button>().onClick.AddListener(press_exit);
        }

        if (Input.GetKeyDown("space")) // Обновление переменной, отвечающей за прыжок объекта "Player", если произошло нажатие кнопки
            jump = true;

        if (Input.GetKeyDown("d")) // Проверка нажатия кнопки движения направо. В случае нажатия - добавление в очередь
            queue.Enqueue(0);
  
        if (Input.GetKeyDown("a")) // Проверка нажатия кнопки движения налево. В случае нажатия - добавление в очередь
            queue.Enqueue(1);
    }

    /**
     *  Обновление физических параметров
     **/
    void FixedUpdate()
    {
        // Если очередь не пустая, то объект передвигается на другую полосу
        if (queue.Count > 0)
        {
            int direct = queue.Dequeue();
            if (direct == 0)
            {
                if (state != 2)
                {
                    state+=1;
                    Clicked(false); // Передвижение объекта "Player" вправо
                }
            }
            else
            {
                if (state != 0)
                {
                    state-=1;
                    Clicked(true); // Передвижение объекта "Player" влево
                }
            }
            rb.velocity = Vector3.zero; // Зануление скорости по всем осям
        }
        
        // Совершение прыжка, в случае, если кнопка была нажата и объект "Player" находится на планете 
        if (jump)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(0, 80, 0); // Перемещение объекта "Player" наверх
                jump = false;
                isGrounded = false;
            }
        }
    }

    /**
     * Проверка: коллизия произошла с поверхностью планеты или нет.
     * Изменение значения переменной isGrounded
     * 
     * @param collision - объект, с которым произошло столкновение
     * @param value - значение, присваиваемое переменной isGrounded
     * @return true, если коллизия произошла с поверхностью планеты, false иначе
     **/
    private bool IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = value;
            return true;
        }
        return false;
    }

    /**
     * Вызывается при столкновении объекта "Player" с барьером
     * 
     * @param collision объект, с которым произошло столкновение
     **/
    void OnCollisionEnter(Collision collision)
    {
        bool check = IsGroundedUpate(collision, true);
        if (!check) // Если коллизия не с поверхностью планеты, то выводится окно с кнопкой "Начать сначала"
        {
            Time.timeScale = 0; // Пауза
            GameObject.Find("Player/Canvas/Die").SetActive(true);
            GameObject.Find("Player/Canvas/Die/ButtonRest").GetComponent<Button>().onClick.AddListener(press_die);
        }
    }

    /**
     * Обработка коллизии в момент отрыва от поверхности планеты
     * 
     * @param collision объект от которого оттолкнулся "Player"
     **/
    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false); // Указываем, что объект не на поверхность планеты
    }
}

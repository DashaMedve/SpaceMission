using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class AstronautScript : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Скорость движения астронавта
    private bool isGameOver = false; // Флаг для проверки окончания игры
    private float minX, maxX, minY, maxY; // Границы экрана
    private float elapsedTime = 0f; // Прошедшее время
    private int score = 0; // Очки

    // Прямоугольники для UI элементов
    private Rect gameOverRect;
    private Rect finalScoreRect;
    private Rect restartButtonRect;
    private Rect exitButtonRect;
    private Rect scoreTextRect;
    private GUIStyle guiStyle; 

    void Start()
    {
        // Получаем границы экрана
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        minX = lowerLeft.x;
        maxX = upperRight.x;
        minY = lowerLeft.y;
        maxY = upperRight.y;

        // Устанавливаем прямоугольники для UI элементов
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        gameOverRect = new Rect(centerX - 150, centerY - 100, 300, 50);
        finalScoreRect = new Rect(centerX - 150, centerY - 40, 300, 50);
        restartButtonRect = new Rect(centerX - 75, centerY + 20, 150, 50);
        exitButtonRect = new Rect(centerX - 75, centerY + 80, 150, 50);
        scoreTextRect = new Rect(centerX - 150, 10, 300, 50);

        // Настраиваем стиль GUI
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 36; // Шшрифт
        guiStyle.normal.textColor = Color.white;
        guiStyle.alignment = TextAnchor.MiddleCenter;
    }

    void Update()
    {
        // Если игра не окончена
        if (!isGameOver)
        {
            // Получаем позицию мыши и преобразуем её в игровые координаты
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;

            // Ограничиваем позицию астронавта в пределах экрана
            transform.position = ClampPosition(mousePos);

            // Обновляем очки на основе прошедшего времени
            elapsedTime += Time.deltaTime;
            score = Mathf.FloorToInt(elapsedTime);
        }
    }

    void OnGUI()
    {
        // Если игра не окончена
        if (!isGameOver)
        {
            // Отображаем текущий счет
            GUI.Label(scoreTextRect, "Score: " + score, guiStyle);
        }
        else
        {
            // Отображаем сообщение об окончании игры и финальный счет
            GUI.Box(gameOverRect, "Game Over", guiStyle);
            GUI.Label(finalScoreRect, "Final Score: " + score, guiStyle);

            // Кнопка для перезапуска игры
            if (GUI.Button(restartButtonRect, "Restart", guiStyle))
            {
                RestartGame();
            }

            // Кнопка для выхода в "Space"
            if (GUI.Button(exitButtonRect, "Exit", guiStyle))
            {
                ExitToSpace();
            }
        }
    }

    // Ограничиваем позицию астронавта в пределах экрана
    Vector3 ClampPosition(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        return position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем столкновение с объектом, имеющим тег "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Останавливаем движение персонажа
            isGameOver = true;

            // Останавливаем все объекты при столкновении
            Rigidbody2D[] rigidbodies = FindObjectsOfType<Rigidbody2D>();
            foreach (Rigidbody2D rb in rigidbodies)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            // Останавливаем время игры
            Time.timeScale = 0f;
        }
    }

    // Метод для перезагрузки сцены
    public void RestartGame()
    {
        // Перезапуск текущей сцены
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Возобновляем время игры
        Time.timeScale = 1f;
    }

    // Метод для перехода к сцене "Space"
    public void ExitToSpace()
    {
        // Переход к сцене "Space"
        SceneManager.LoadScene("Space");

        // Возобновляем время игры
        Time.timeScale = 1f;
    }
}

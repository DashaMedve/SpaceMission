using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class AstronautScript : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private bool isGameOver = false;
    private float minX, maxX, minY, maxY; // Границы экрана
    private float elapsedTime = 0f; // Прошедшее время
    private int score = 0; // Очки

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

        // Устанавливаем прямоугольники для UI элементов на одном уровне по оси X
        float centerX = Screen.width / 2;
        float centerY = Screen.height / 2;
        gameOverRect = new Rect(centerX - 150, centerY - 100, 300, 50);
        finalScoreRect = new Rect(centerX - 150, centerY - 40, 300, 50);
        restartButtonRect = new Rect(centerX - 75, centerY + 20, 150, 50);
        exitButtonRect = new Rect(centerX - 75, centerY + 80, 150, 50);
        scoreTextRect = new Rect(centerX - 150, 10, 300, 50); // Увеличенное окошко для счета

        // Настраиваем стиль GUI
        guiStyle = new GUIStyle();
        guiStyle.fontSize = 36; // Увеличенный шрифт
        guiStyle.normal.textColor = Color.white;
        guiStyle.alignment = TextAnchor.MiddleCenter; // Выровненный текст по центру
    }

    void Update()
    {
        if (!isGameOver)
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            transform.position = ClampPosition(mousePos);

            // Пример обновления очков
            elapsedTime += Time.deltaTime;
            score = Mathf.FloorToInt(elapsedTime);
        }
    }

    void OnGUI()
    {
        if (!isGameOver)
        {
            GUI.Label(scoreTextRect, "Score: " + score, guiStyle);
        }
        else
        {
            GUI.Box(gameOverRect, "Game Over", guiStyle);
            GUI.Label(finalScoreRect, "Final Score: " + score, guiStyle);

            if (GUI.Button(restartButtonRect, "Restart", guiStyle))
            {
                RestartGame();
            }

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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Останавливаем движение персонажа
            isGameOver = true;

            // Останавливаем объекты при столкновении
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
        // Перезапуск сцены
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

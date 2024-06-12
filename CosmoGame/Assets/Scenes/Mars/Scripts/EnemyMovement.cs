using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость движения марсианина
    private Transform target; // Цель (астронавт)
    private int flag_died = 0; // Флаг для отслеживания удаления марсианина

    void Start()
    {
        // Находим объект с тегом "Player" и устанавливаем его как цель
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Проверяем, установлена ли цель
        if (target != null)
        {
            // Рассчитываем направление к цели и нормализуем его
            Vector3 direction = (target.position - transform.position).normalized;
            // Перемещаем марсианина в направлении цели с заданной скоростью
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, произошло ли столкновение с марсианином (имя объекта содержит "Marsian")
        if (collision.gameObject.name.Contains("Marsian"))
        {
            // Если текущий объект - это не оригинальный марсианин (имя не "Marsian")
            if (gameObject.name != "Marsian")
            {
                // Удаляем текущий объект (марсианин)
                Destroy(gameObject);
                // Устанавливаем флаг, указывающий, что текущий объект был удален
                flag_died = 1;
            }
            // Если объект столкновения - это не оригинальный марсианин (имя не "Marsian") и текущий объект еще не был удален
            if (collision.gameObject.name != "Marsian" && flag_died == 0)
            {
                // Удаляем объект столкновения (другой марсианин)
                Destroy(collision.gameObject);
            }
            // Сбрасываем флаг после обработки столкновения
            flag_died = 0;
        }
    }
}

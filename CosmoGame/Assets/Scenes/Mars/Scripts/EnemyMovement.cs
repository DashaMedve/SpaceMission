using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость движения
    private Transform target; // Цель (астронавт)

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверка на имя Marsian(Clone)
        if (collision.gameObject.name == "Marsian(Clone)")
        {
            Debug.Log("Марсиане столкнулись!");

            // Проверка на то, что текущий объект не является оригинальным марсианином
            if (gameObject.name != "Marsian")
            {
                Destroy(gameObject);
            }

            // Удаляем клон марсианина
            Destroy(collision.gameObject);
        }
    }
}

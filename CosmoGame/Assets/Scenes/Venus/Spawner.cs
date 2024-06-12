using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public Rigidbody rb; // Физические свойства объекта "Spawner"
    public GameObject enemyPrefab; // Шаблон объекта "Star" для генерации

    private GameObject star; // Переменная для хранения сгенерированного объекта

    /**
     * Генерация объектов "Star"
     **/
    void Start()
    {
        float step = 50; // Расстояние между объектами "Star"
        Vector3 pos = transform.position + rb.transform.forward * step; // Вектор сдвига
        Collider Collider; // Объявление коллайдера
        while (true) {
            Collider[] intersecting = Physics.OverlapSphere(pos, 25f); // Cоздание сферы для проверки коллизии нового объекта "Star" и стен
            if (intersecting.Length == 0)
            {
                star = Instantiate(enemyPrefab, pos, Quaternion.identity); // Создание объекта "Star"
                star.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f); // Изменение размера объекта "Star"
                star.transform.position = star.transform.position + new Vector3(0, 7.0f, 0); // Изменение положения объекта "Star" по оси y
                star.AddComponent<Star>(); // Добавление компонента "Star" к объекту "Star"
                star.AddComponent<BoxCollider>(); // Добавление компонента "BoxCollider" к объекту "Star"
                Collider = star.GetComponent<BoxCollider>();
                Collider.isTrigger = true; // Изменение режима коллайдера
                pos += rb.transform.forward * step;
            }
            else
                break;
        }
    }
}

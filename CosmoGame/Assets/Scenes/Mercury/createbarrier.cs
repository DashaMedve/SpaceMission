using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class createbarrier : MonoBehaviour
{
    public GameObject[] enemyPrefab; // ������� �������� ��� ���������
    public float timeSpawn; // �������� ������� ����� ��������� ��������

    private GameObject star1; // ���������� ��� �������� ������
    private GameObject star2; // ���������� ��� �������� ������
    private bool creat; // ���������� ��� �������� ��������� �� ����� � ����������� ���������� �������
    private float timer; // ������ ��������� �� timeSpawn �� 0

    /**
     * ������������� ���������� ������
     **/
    void Start()
    {
        timeSpawn = 6f;
        creat = false;
        timer = timeSpawn;
    }

    /**
     * �������� ������� "Star"
     * 
     * @param star - ���������� � ������� ����� ����������� ����� ������ "Star"
     * @param position - ������ ��������� ������ ������� "Star"
     **/
    void init_star(GameObject star, Vector3 position)
    {
        Collider Collider; // �������� ����������
        star = Instantiate(enemyPrefab[3], position, Quaternion.identity); // �������� ������� "Star"
        star.transform.localScale = new Vector3(15f, 15f, 15f); // ��������� ������� ������� "Star"
        star.AddComponent<rotatebarrier>(); // ���������� ���������� "rotatebarrier" � ������� "Star"
        star.AddComponent<Star>(); // ���������� ���������� "Star" � ������� "Star"
        star.AddComponent<BoxCollider>(); // ���������� ���������� "BoxCollider" � ������� "Star"
        Collider = star.GetComponent<BoxCollider>();
        Collider.isTrigger = true; // ��������� ������ ����������
    }

    /**
     * ���������� �����
     **/
    void Update()
    {
        timer -= Time.deltaTime; // ��������� �������� �������
        System.Random random_generator1 = new System.Random(); // �������� �������, ������������� ��������� ��������
        System.Random random_generator2 = new System.Random(); // �������� �������, ������������� ��������� ��������
        if (timer <= 3) // ������� ��� ����, ����� ����� �� ����������� � ��������
        {
            if (!creat) // ������� ��� �������� ������ ���� ��� �� ���� timer
            {
                var random_spawn_star = random_generator2.Next(0, 3); // ��������� ������� ������
                Vector3 position1, position2;
                int pos = 0;
                for (int i = 0; i < 3; i++) // �� ������ random_spawn_star �������� ��� ���� �� ��� ������
                {
                    if (random_spawn_star == 0)
                    {
                        position1 = transform.position - new Vector3(91, -30f, pos);
                        position2 = transform.position - new Vector3(91, -30f, -pos);
                    }
                    else if (random_spawn_star == 1)
                    {
                        position1 = transform.position + new Vector3(0, 30f, pos);
                        position2 = transform.position + new Vector3(0, 30f, -pos);
                    }
                    else
                    {
                        position1 = transform.position + new Vector3(91, 30f, pos);
                        position2 = transform.position + new Vector3(91, 30f, -pos);
                    }
                    init_star(star1, position1);
                    init_star(star2, position2);
                    pos -= 350; // ��������� ����������, �������� ������� ���� 
                }
                creat = true;
            }
        }
        if (timer <= 0) // ���� timer �������� �� 0
        {
            timer = timeSpawn; // ������ ������� �������
            var random = random_generator1.Next(0,3); // ��������� ���������� ������� ��� ��������� �����������

            // ��������� ��� �������� Downbarrier
            if (random == 0)
            {
                Instantiate(enemyPrefab[0], transform.position + new Vector3(0, -8, 0), Quaternion.identity);
                Instantiate(enemyPrefab[0], transform.position + new Vector3(91, -8, 0), Quaternion.identity);
                Instantiate(enemyPrefab[0], transform.position - new Vector3(91, 8, 0), Quaternion.identity);
            }

            else if (random == 1)
            {
                List<int> road = new List<int>() { 0, 1, 2 };
                var randomroad1 = random_generator1.Next(0, 3); // ��������� ������� ������
                road.RemoveAt(randomroad1);
                var randomroad2 = random_generator1.Next(0, 2); // ��������� ������� ������
                road.RemoveAt(randomroad2);

                // ��������� ���� �������� "asteroid"
                if (road[0] == 0)
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(0, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(91, 70, 0), Quaternion.identity);
                }

                else if (road[0] == 1)
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(-91, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(91, 70, 0), Quaternion.identity);
                }

                else
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(0, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(-91, 70, 0), Quaternion.identity);
                }
            }

            // ��������� ������� "BigBarrier"
            else
            {
                var random_place = random_generator1.Next(-91, 92);
                Instantiate(enemyPrefab[2], transform.position + new Vector3(random_place, 70, 0), Quaternion.identity);
            }
            creat = false;
        }
    }
}

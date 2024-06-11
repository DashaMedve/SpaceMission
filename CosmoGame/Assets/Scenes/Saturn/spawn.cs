using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System;

public class spawn : MonoBehaviour
{
    public GameObject[] enemyPrefab; // ������� �������� ��� ���������
    public float timeSpawn; // �������� ������� ����� ��������� ��������

    private bool creat; // ���������� ��� �������� ��������� �� ����� � ����������� ���������� �������
    private GameObject star; // ���������� ��� �������� ������
    private float timer; // ������ ��������� �� timeSpawn �� 0
    private System.Random random; // �������� �������, ������������� ��������� ��������

    /**
     * ������������� ���������� ������
     **/
    void Start()
    {
        timeSpawn = 4f;
        timer = timeSpawn;
        creat = false;
        random = new System.Random();
    }

    /**
     * ���������� �����
     **/
    void Update()
    {
        timer -= Time.deltaTime; // ��������� �������� �������

        Collider Collider; // �������� ����������
        if (timer <= 2)
        {
            if (!creat)
            {
                var randomx = random.Next(-150, 150); // ��������� ��������� ���������� x
                var randomy = random.Next(-34, 34); // ��������� ��������� ���������� y
                star = Instantiate(enemyPrefab[0], transform.position + new Vector3(randomx, randomy, 0), Quaternion.identity); // �������� ������� "Star"
                star.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f); // ��������� ������� ������� "Star"
                star.AddComponent<Star>(); // ���������� ���������� "Star" � ������� "Star"
                star.AddComponent<BoxCollider>(); // ���������� ���������� "BoxCollider" � ������� "Star"
                star.AddComponent<create>(); // ���������� ���������� "create" � ������� "Star"
                Collider = star.GetComponent<BoxCollider>();
                Collider.isTrigger = true; // ��������� ������ ����������
                creat = true;
            }
        }
        if (timer <= 0)
        {
            timer = timeSpawn; // ������ ������� �������
            var randomx = random.Next(-150, 150); // ��������� ��������� ���������� x
            var randomy = random.Next(-34, 34); // ��������� ��������� ���������� y
            star = Instantiate(enemyPrefab[1], transform.position + new Vector3(randomx, randomy, 0), Quaternion.identity); // �������� ������� "Rings"
            star.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f); // ��������� ������� ������� "Rings"
            star.transform.Rotate(-253.5f, 0.6f, 0, 0); // ������� ������� "Rings"
            star.AddComponent<Ring>(); // ���������� ���������� "Ring" � ������� "Rings"
            star.AddComponent<BoxCollider>(); // ���������� ���������� "BoxCollider" � ������� "Rings"
            star.AddComponent<create>(); // ���������� ���������� "create" � ������� "Rings"
            Collider = star.GetComponent<BoxCollider>();
            Collider.isTrigger = true; // ��������� ������ ����������
            creat = false;
        }
    }
}

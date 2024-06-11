using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    public Rigidbody rb; // ���������� �������� ������� "Spawner"
    public GameObject enemyPrefab; // ������ ������� "Star" ��� ���������

    private GameObject star; // ���������� ��� �������� ���������������� �������

    /**
     * ��������� �������� "Star"
     **/
    void Start()
    {
        float step = 50; // ���������� ����� ��������� "Star"
        Vector3 pos = transform.position + rb.transform.forward * step; // ������ ������
        Collider Collider; // ���������� ����������
        while (true) {
            Collider[] intersecting = Physics.OverlapSphere(pos, 25f); // C������� ����� ��� �������� �������� ������ ������� "Star" � ����
            if (intersecting.Length == 0)
            {
                star = Instantiate(enemyPrefab, pos, Quaternion.identity); // �������� ������� "Star"
                star.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f); // ��������� ������� ������� "Star"
                star.transform.position = star.transform.position + new Vector3(0, 7.0f, 0); // ��������� ��������� ������� "Star" �� ��� y
                star.AddComponent<Star>(); // ���������� ���������� "Star" � ������� "Star"
                star.AddComponent<BoxCollider>(); // ���������� ���������� "BoxCollider" � ������� "Star"
                Collider = star.GetComponent<BoxCollider>();
                Collider.isTrigger = true; // ��������� ������ ����������
                pos += rb.transform.forward * step;
            }
            else
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RotateBigBarrier : MonoBehaviour
{
    public Rigidbody rb; // // ���������� �������� ������� "BigBarrier"
    private GameObject create; // ���������� ��� �������� create object
    private bool state; // ���������� ���������� �� ����������� ������� "BigBarrier"

    /**
     * ������������� ���������� ������
     **/
    void Start()
    {
        state = true;
        create = GameObject.Find("CreateBarrier");
    }

    /**
     * ���������� �����
     * ���������� �������� ������� "BigBarrier" � �������
     **/
    void Update()
    {
        if (state) // ����� ����������� �������� ������� "BigBarrier"
        {
            if (transform.position.x - create.transform.position.x < 90)
                transform.position += new Vector3(1f, 0, 0); // ��������� ��������� ������� "BigBarrier"
            else
            {
                state = false; // ��������� ����������� �������� ������� "BigBarrier"
                rb.velocity = Vector3.zero; // ��������� ������� �������� �� ���� ���� ������� "BigBarrier"
            }
        }
        else
        {
            if (transform.position.x - create.transform.position.x > -90)
                transform.position -= new Vector3(1f, 0, 0); // ��������� ��������� ������� "BigBarrier"
            else
            {
                state = true; // ��������� ����������� �������� ������� "BigBarrier"
                rb.velocity = Vector3.zero; // ��������� ������� �������� �� ���� ���� ������� "BigBarrier"
            }
        }
    }
}

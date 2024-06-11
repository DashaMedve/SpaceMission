using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Venus : MonoBehaviour
{
    public Rigidbody rb; // ���������� �������� ������� "Player"
    public float runSpeed; // �������� �������� ������� "Player"
    public float strafeSpeed; // �������� �������� ������� "Player"

    protected bool strafeLeft; // ���������� ������������ ������ �� ������, ���������� �� ������� ������ ������� "Player"
    protected bool strafeRight; // ���������� ������������ ������ �� ������, ���������� �� ������� ������� ������� "Player"
    protected bool forward; // ���������� ������������ ������ �� ������, ���������� �� ��� ����� ������� "Player"
    protected bool backward; // ���������� ������������ ������ �� ������, ���������� �� ��� ����� ������� "Player"

    /**
     * ������������� ���������� ������
     **/
    void Start()
    {
        runSpeed = 100f;
        strafeSpeed = 100f;

        strafeLeft = false;
        strafeRight = false;
        forward = false;
        backward = false;
    }

    /**
     * ���������� �����
     * �������� �� ������� ������ � ��������� ���������� ���������� �� ��������������� �������
     **/
    void Update()
    {
        if (Input.GetKey("d"))
            strafeRight = true; 
        else
            strafeRight = false;
     
        if (Input.GetKey("a"))
            strafeLeft = true;
        else
            strafeLeft = false; 

        if (Input.GetKey("w"))
            forward = true;
        else
            forward= false;

        if(Input.GetKey("s"))
            backward = true; 
        else
            backward = false;
    }

    /**
     *  ���������� ���������� ����������
     **/
    void FixedUpdate()
    {
        if (strafeLeft)
            rb.transform.Rotate(Vector3.down,  strafeSpeed * Time.deltaTime); // ������� ������

        if (strafeRight)
            rb.transform.Rotate(Vector3.up, strafeSpeed * Time.deltaTime); // ������� �������

        if (backward)
            rb.AddForce(-rb.transform.forward * runSpeed * Time.deltaTime * 100f, ForceMode.Impulse); // ��� �����

        if (forward)
            rb.AddForce(rb.transform.forward*runSpeed* Time.deltaTime*100f, ForceMode.Impulse); // ��� �����
        rb.velocity = Vector3.zero; // ��������� �������� �� ���� ����
    }
}

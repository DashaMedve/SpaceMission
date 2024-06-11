using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class Rotate : MonoBehaviour
{
    public Rigidbody rb; // ���������� �������� ������� "Player"
    public float JumpForce; // ���� ������ ������� "Player"

    private bool jump; // ����������, ���������� �� ������ ������� "Player". True, ���� ������ "space" ������ 
    private int state; // ����� ������, �� ������� ��������� ������ "Player"
    private TMP_Text KmText; // ���������� ��� �������� � ������ ������ � ���������� ���������� ���������� �������� "Player"
    private bool isGrounded; // ����������, � ������� �������� ��������� ������� "Player" (���� ������ ��������� �� ����������� ������� - true, ���� � ������ - false)
    private Queue<int> queue = new Queue<int>(); // ������� ��� �������� ������ ������������ (������� ������)
   
    /**
     * ������������� ���������� ������
     **/
    private void Start()
    {
        state = 1;
        JumpForce = 1000f;
        jump = false;
        isGrounded = true;
        Time.timeScale = 1; // ��������� ����
        KmText = GameObject.Find("Player/Canvas/Km").GetComponent<TMP_Text>(); // ���������� �������� ������ � ���������� ���������� ����������
    }

    /**
     * ������� ��� �������� ����������� ������� "Player" � ����� ������ �� ������
     * 
     * @param target - �����, � ������� ����� ��������� ������� "Player"
     * @param delta - �����, �� ������� ����� ������������� ������� "Player" � ����� target
     **/
    IEnumerator SmoothMove(Vector3 target, float delta)
    {
        float closeEnough = 0.2f; // ����������� ���������� ����� target � �������� "Player"
        float distance = (rb.transform.position - target).magnitude; // ������ ����������, ������� ����� ������ � �������������� ���������

        while (distance >= closeEnough) // ���� ������ �� ������ "Player" target
        {
            transform.position = Vector3.Lerp(new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z), target, delta); // ������� ����������� �������
            yield return StartCoroutine(SmoothMove(target, delta)); // ����� ������ ������� ��� ���������� �����
            distance = (rb.transform.position - target).magnitude; // ������ ����������, ������� ����� ������ � �������������� ���������
        }
        rb.transform.position = target; // ����������� ������� "Player" � ������� target
    }

    /**
     * ����������� ������� � ���� �� ������
     * 
     * @param - ����������� �������� ������� "Player"
     **/
    void Clicked(bool direction)
    {
        // ���������� � ������������� ������� �����������
        Vector3 relativeLocation;
        if (direction)
            relativeLocation = new Vector3(-97, 0, 0);
        else
            relativeLocation = new Vector3(97, 0, 0);

        Vector3 targetLocation = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z) + relativeLocation; // ���������� ����� ������� ������� "Player"
        float timeDelta = 0.4f; // �����, �� ������� ����� ������������� ������� "Player" � ����� targetLocation
        this.StartCoroutine(SmoothMove(targetLocation, timeDelta)); // ����� ������� ����������� �����������
    }

    /**
     * ����������� ��� ������� ������ "������ �������"
     * ��������� ���� ������
     **/
    void press_die()
    {
        GameObject.Find("Player/Canvas/Die").SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /**
     * ����������� ��� ������� ������ "��������� � �������� ����"
     * ��������� ����� ����
     **/
    void press_exit()
    {
        GameObject.Find("Player/Canvas/Final").SetActive(false);
        SceneManager.LoadScene(0);
    }

    /**
     * ���������� �����
     **/
    void Update()
    {
        // ��������� ���������� ���������� ����������
        double score = double.Parse(KmText.text);
        score += 1; 
        KmText.text = score.ToString();

        if (score >= 10000) // �������� ���������� ���������� ����������
        {
            // ����� ����������� ����
            Time.timeScale = 0; // �����
            GameObject.Find("Player/Canvas/Final").SetActive(true);
            GameObject.Find("Player/Canvas/Final/line2").GetComponent<TMP_Text>().text = "�� ������� " + 
                GameObject.Find("Player/Canvas/Counter").GetComponent<TMP_Text>().text + " ���������";
            GameObject.Find("Player/Canvas/Final/ButtonFinal").GetComponent<Button>().onClick.AddListener(press_exit);
        }

        if (Input.GetKeyDown("space")) // ���������� ����������, ���������� �� ������ ������� "Player", ���� ��������� ������� ������
            jump = true;

        if (Input.GetKeyDown("d")) // �������� ������� ������ �������� �������. � ������ ������� - ���������� � �������
            queue.Enqueue(0);
  
        if (Input.GetKeyDown("a")) // �������� ������� ������ �������� ������. � ������ ������� - ���������� � �������
            queue.Enqueue(1);
    }

    /**
     *  ���������� ���������� ����������
     **/
    void FixedUpdate()
    {
        // ���� ������� �� ������, �� ������ ������������� �� ������ ������
        if (queue.Count > 0)
        {
            int direct = queue.Dequeue();
            if (direct == 0)
            {
                if (state != 2)
                {
                    state+=1;
                    Clicked(false); // ������������ ������� "Player" ������
                }
            }
            else
            {
                if (state != 0)
                {
                    state-=1;
                    Clicked(true); // ������������ ������� "Player" �����
                }
            }
            rb.velocity = Vector3.zero; // ��������� �������� �� ���� ����
        }
        
        // ���������� ������, � ������, ���� ������ ���� ������ � ������ "Player" ��������� �� ������� 
        if (jump)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(0, 80, 0); // ����������� ������� "Player" ������
                jump = false;
                isGrounded = false;
            }
        }
    }

    /**
     * ��������: �������� ��������� � ������������ ������� ��� ���.
     * ��������� �������� ���������� isGrounded
     * 
     * @param collision - ������, � ������� ��������� ������������
     * @param value - ��������, ������������� ���������� isGrounded
     * @return true, ���� �������� ��������� � ������������ �������, false �����
     **/
    private bool IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            isGrounded = value;
            return true;
        }
        return false;
    }

    /**
     * ���������� ��� ������������ ������� "Player" � ��������
     * 
     * @param collision ������, � ������� ��������� ������������
     **/
    void OnCollisionEnter(Collision collision)
    {
        bool check = IsGroundedUpate(collision, true);
        if (!check) // ���� �������� �� � ������������ �������, �� ��������� ���� � ������� "������ �������"
        {
            Time.timeScale = 0; // �����
            GameObject.Find("Player/Canvas/Die").SetActive(true);
            GameObject.Find("Player/Canvas/Die/ButtonRest").GetComponent<Button>().onClick.AddListener(press_die);
        }
    }

    /**
     * ��������� �������� � ������ ������ �� ����������� �������
     * 
     * @param collision ������ �� �������� ����������� "Player"
     **/
    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false); // ���������, ��� ������ �� �� ����������� �������
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rotatefly : MonoBehaviour
{
    public Rigidbody rb; // ���������� �������� ������� "Player"
    public float strafeSpeed; // �������� �������� � ������� ������� "Player"

    protected bool strafeLeft; // ���������� ������������ ������ �� ������, ���������� �� ������� ������ ������� "Player"
    protected bool strafeRight; // ���������� ������������ ������ �� ������, ���������� �� ������� ������� ������� "Player"
    protected bool up; // ���������� ������������ ������ �� ������, ���������� �� ������ ������� "Player"
    protected bool down; // ���������� ������������ ������ �� ������, ���������� �� ������������ ������� "Player" ����

    private GameObject final;
    private TMP_Text ScoreText;

    /**
     * ������������� ���������� ������
     **/
    void Start()
    {
        Time.timeScale = 1;
        strafeSpeed = 7500f;
        strafeLeft = false;
        strafeRight = false;
        ScoreText = GameObject.Find("ScoreRing").GetComponent<TMP_Text>();
        up = false;
    }

    /**
    * ����������� ��� ������� ������ "��������� � �������� ����"
    * ��������� ����� ����
    **/
    void press()
    {
        SceneManager.LoadScene(0);
    }

    /**
     * ���������� �����
     * �������� �� ������� ������ � ��������� ���������� ���������� �� ��������������� �������
     **/
    void Update()
    {
        int score = Convert.ToInt32(ScoreText.text);
        if (score >= 5) // ���� ������� 5 �����, �� ���� ������������� � ��������� ���� � ������������
        {
            Time.timeScale = 0;
            final = GameObject.Find("Player/Canvas/Final");
            final.SetActive(true);
            GameObject.Find("Player/Canvas/Final/Scr").GetComponent<Text>().text = "�� �������: " + GameObject.Find("Player/Canvas/Counter").GetComponent<TMP_Text>().text + " ���������";
            GameObject.Find("Player/Canvas/Final/Buttonfin").GetComponent<Button>().onClick.AddListener(press);
        }
        if (Input.GetKey("d"))
            strafeRight = true;
        else
            strafeRight = false;

        if (Input.GetKey("a"))
            strafeLeft = true;
        else
            strafeLeft = false;

        if (Input.GetKey("w"))
            up = true;
        else
            up = false;
        if (Input.GetKey("s"))
            down = true;
        else
            down = false;
    }

    /**
    *  ���������� ���������� ����������
    **/
    void FixedUpdate()
    {
        if (strafeLeft)
            rb.AddForce(-rb.transform.right * strafeSpeed * Time.deltaTime, ForceMode.Impulse); // ������� ������

        if (strafeRight)
            rb.AddForce(rb.transform.right * strafeSpeed * Time.deltaTime, ForceMode.Impulse); // ������� �������

        if (up)
            rb.AddForce(rb.transform.up * strafeSpeed * Time.deltaTime, ForceMode.Impulse); // �������� ������

        if (down)
            rb.AddForce(-rb.transform.up * strafeSpeed * Time.deltaTime, ForceMode.Impulse); // �������� ����

        rb.velocity = Vector3.zero;
    }
}

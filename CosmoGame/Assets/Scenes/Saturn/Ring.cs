using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ring : MonoBehaviour
{
    private TMP_Text ScoreText; // ���������� ��� �������� � ������ ������ � ���������� ��������� �������� "Rings"

    /**
     * ������������� ���������� ������
     **/
    void Start()
    {
        ScoreText = GameObject.Find("ScoreRing").GetComponent<TMP_Text>();
    }

    /**
     * ���������� ��� ����������� ������� "Player" ����� ������ "Rings"
     * 
     * @param other ������, � ������� ��������� ������������
     **/
    private void OnTriggerEnter(Collider other)
    {
        int score = Convert.ToInt32(ScoreText.text);
        score += 1; // ��������� ���������� �����
        ScoreText.text = score.ToString();
        Destroy(this.gameObject); // ���������� �������
    }
}

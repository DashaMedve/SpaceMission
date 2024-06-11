using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    /**
     * ��������� ������������ "Heart" c �������� "Player"
     * 
     * ����� ����������� � ������ "��������� � �������� ����"
     **/
    void OnCollisionEnter(Collision collision)
    {
        Time.timeScale = 0; // �����
        GameObject.Find("astronout/Canvas/Image").SetActive(true);
        GameObject.Find("astronout/Canvas/Image/ButtonFin").GetComponent<Button>().onClick.AddListener(onpress);
        GameObject.Find("astronout/Canvas/Image/line2").GetComponent<Text>().text = "�� �������: " +
            GameObject.Find("astronout/Canvas/Counter").GetComponent<TMP_Text>().text + " ���������";
    }

    /**
     * ����������� ��� ������� ������ "��������� � �������� ����"
     * ��������� ����� ����
     **/
    void onpress()
    {
        GameObject.Find("astronout/Canvas/Image").SetActive(false);
        SceneManager.LoadScene(0);
    }
}

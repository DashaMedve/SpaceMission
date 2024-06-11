using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    /**
     * Обработка столкновения "Heart" c объектом "Player"
     * 
     * Вывод результатов и кнопки "Вернуться в основное меню"
     **/
    void OnCollisionEnter(Collision collision)
    {
        Time.timeScale = 0; // Пауза
        GameObject.Find("astronout/Canvas/Image").SetActive(true);
        GameObject.Find("astronout/Canvas/Image/ButtonFin").GetComponent<Button>().onClick.AddListener(onpress);
        GameObject.Find("astronout/Canvas/Image/line2").GetComponent<Text>().text = "Вы собрали: " +
            GameObject.Find("astronout/Canvas/Counter").GetComponent<TMP_Text>().text + " звездочек";
    }

    /**
     * Срабатывает при нажатии кнопки "Вернуться в основное меню"
     * Открывает сцену меню
     **/
    void onpress()
    {
        GameObject.Find("astronout/Canvas/Image").SetActive(false);
        SceneManager.LoadScene(0);
    }
}

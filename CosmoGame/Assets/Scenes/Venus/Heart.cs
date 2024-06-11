using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
            Time.timeScale = 0;
            GameObject.Find("astronout/Canvas/Image").SetActive(true);
            GameObject.Find("astronout/Canvas/Image/ButtonFin").GetComponent<Button>().onClick.AddListener(onpress);
            GameObject.Find("astronout/Canvas/Image/line2").GetComponent<Text>().text = "Вы собрали: " + GameObject.Find("astronout/Canvas/Counter").GetComponent<TMP_Text>().text + " звездочек";
    }

    void onpress()
    {
        GameObject.Find("astronout/Canvas/Image").SetActive(false);
        SceneManager.LoadScene(0);
    }
}

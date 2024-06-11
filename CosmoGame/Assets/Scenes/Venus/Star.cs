using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    private TMP_Text ScoreText;
    private void Start()
    {
        ScoreText = GameObject.Find("Counter").GetComponent<TMP_Text>();
    }

    private void Update()
    {
        this.transform.Rotate(0,4f,0);
    }
    private void OnTriggerEnter(Collider other)
    {
        int score = Convert.ToInt32(ScoreText.text);
        score += 1;
        ScoreText.text = score.ToString();
        Destroy(this.gameObject);
    }
}

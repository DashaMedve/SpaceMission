using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ring : MonoBehaviour
{
    private GameObject final;
    private TMP_Text ScoreText;
    private string str;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GameObject.Find("ScoreRing").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        int score = Convert.ToInt32(ScoreText.text);
        score += 1;
        ScoreText.text = score.ToString();
        Destroy(this.gameObject);
    }
}

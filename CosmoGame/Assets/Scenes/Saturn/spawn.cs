using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using System;

public class spawn : MonoBehaviour
{
    private GameObject star;
    // Start is called before the first frame update
    public GameObject[] enemyPrefab;
    private bool creat = false;
    public float timeSpawn = 6f;
    private float timer;
    private System.Random rnrd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        timer = timeSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        Collider Collider;
        if (timer <= 3)
        {
            if (!creat)
            {
                var randomx = rnrd.Next(-150, 150);
                var randomy = rnrd.Next(-34, 34);
                star = Instantiate(enemyPrefab[0], transform.position + new Vector3(randomx, randomy, 0), Quaternion.identity);
                star.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
                star.AddComponent<Star>();
                star.AddComponent<BoxCollider>();
                star.AddComponent<rotatebarrier>();
                Collider = star.GetComponent<BoxCollider>();
                Collider.isTrigger = true;
                creat = true;
            }
        }
        if (timer <= 0)
        {
            timer = timeSpawn;
            var randomx = rnrd.Next(-150, 150);
            var randomy = rnrd.Next(-34, 34);
            creat = false;
        }
    }
}

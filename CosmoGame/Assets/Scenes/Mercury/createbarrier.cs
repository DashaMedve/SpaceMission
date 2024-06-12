using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class createbarrier : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    private GameObject star1;
    private GameObject star2;
    private bool creat = false;
    public float timeSpawn = 6f;
    private float timer;
    void Start()
    {
        timer = timeSpawn;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        System.Random rng = new System.Random();
        System.Random rnrd = new System.Random();
        if (timer <= 3)
        {
            if (!creat)
            {
                var randomspawnstar = rnrd.Next(0, 3);
                Vector3 position1;
                Vector3 position2;
                Collider Collider;
                int pos = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (randomspawnstar == 0)
                    {
                        position1 = transform.position - new Vector3(91, 0, -pos);
                        position2 = transform.position - new Vector3(91, 0, pos);
                    }
                    else if (randomspawnstar == 1)
                    {
                        position1 = transform.position + new Vector3(0, 0, pos);
                        position2 = transform.position + new Vector3(0, 0, -pos);
                    }
                    else
                    {
                        position1 = transform.position + new Vector3(91, 0, pos);
                        position2 = transform.position + new Vector3(91, 0, -pos);
                    }
                    star1 = Instantiate(enemyPrefab[3], position1, Quaternion.identity);
                    star2 = Instantiate(enemyPrefab[3], position2, Quaternion.identity);
                    star1.transform.localScale = new Vector3(15f, 15f, 15f);
                    star1.transform.position = star1.transform.position + new Vector3(0, 30f, 0); ;
                    star1.AddComponent<rotatebarrier>();
                    star1.AddComponent<Star>();
                    star1.AddComponent<BoxCollider>();
                    Collider = star1.GetComponent<BoxCollider>();
                    Collider.isTrigger = true;
                    star2.transform.localScale = new Vector3(15f, 15f, 15f);
                    star2.transform.position = star2.transform.position + new Vector3(0, 30f, 0); ;
                    star2.AddComponent<rotatebarrier>();
                    star2.AddComponent<Star>();
                    star2.AddComponent<BoxCollider>();
                    Collider = star2.GetComponent<BoxCollider>();
                    Collider.isTrigger = true;
                    pos -= 350;
                }
                creat = true;
            }
        }
        if (timer <= 0)
        {
            timer = timeSpawn;
            var random = rng.Next(0,3);
            if (random == 0)
            {
                Instantiate(enemyPrefab[0], transform.position, Quaternion.identity);
                Instantiate(enemyPrefab[0], transform.position + new Vector3(91, 0, 0), Quaternion.identity);
                Instantiate(enemyPrefab[0], transform.position - new Vector3(91, 0, 0), Quaternion.identity);
            }
            else if (random == 1)
            {
                List<int> road = new List<int>() { 1, 2, 3 };
                var randomroad1 = rng.Next(0, 3);
                road.RemoveAt(randomroad1);
                var randomroad2 = rng.Next(0, 2);
                road.RemoveAt(randomroad2);
                if (road[0] == 0)
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(0, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(91, 70, 0), Quaternion.identity);
                }
                else if (road[0] == 1)
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(-91, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(91, 70, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(0, 70, 0), Quaternion.identity);
                    Instantiate(enemyPrefab[1], transform.position + new Vector3(-91, 70, 0), Quaternion.identity);
                }
            }
            else
            {
                var randomPlace = rng.Next(-91, 92);
                Instantiate(enemyPrefab[2], transform.position + new Vector3(randomPlace, 70, 0), Quaternion.identity);
            }
            creat = false;
        }
    }
}

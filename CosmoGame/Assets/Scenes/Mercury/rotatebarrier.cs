using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatebarrier : MonoBehaviour
{
    private float timer;
    void Start()
    {
        timer = 6f;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            Destroy(gameObject);
        transform.position -= new Vector3(0, 0, 8f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RotateBigBarrier : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject create;
    private bool state = true;
    void Start()
    {
        create = GameObject.Find("CreateBarrier");
    }

    void Update()
    {
        if (state)
        {
            if (transform.position.x - create.transform.position.x < 90)
                transform.position += new Vector3(1f, 0, 0);
            else
            {
                state = false;
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            if (transform.position.x - create.transform.position.x > -90)
                transform.position -= new Vector3(1f, 0, 0);
            else
            {
                state = true;
                rb.velocity = Vector3.zero;
            }
        }
    }
}

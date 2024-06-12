using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Rotate : MonoBehaviour
{
    public Rigidbody rb;
    public float JumpForce = 1000f;
    public float RotateForce;
    private Coroutine a;
    protected bool jump = false;
    private int state = 1;
    private TMP_Text KmText;
    private bool _isGrounded = true;
    private Queue<int> que = new Queue<int>();

    private void Start()
    {
        KmText = GameObject.Find("Player/Canvas/Km").GetComponent<TMP_Text>();
    }
    void Update()
    {
        double score = double.Parse(KmText.text);
        Console.WriteLine(score);
        score += 1;
        KmText.text = score.ToString();
        if (score >= 10000)
        {

        }
        if (Input.GetKeyDown("space"))
            jump = true;
        if (Input.GetKeyDown("d"))
        {
            que.Enqueue(0);
        }
        if (Input.GetKeyDown("a"))
        {
            que.Enqueue(1);
        }
    }
    void FixedUpdate()
    {
        if (que.Count > 0)
        {
            int direct = que.Dequeue();
            if (direct == 0)
            {
                if (state != 2)
                {
                    state+=1;
                    Clicked(false);
                }
            }
            else
            {
                if (state != 0)
                {
                    state-=1;
                    Clicked(true);
                }
            }
            rb.velocity = Vector3.zero;
        }
        if (jump)
        {
            if (_isGrounded)
            {
                rb.velocity = new Vector3(0, 80, 0);
                jump = false;
                _isGrounded = false;
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        bool check = IsGroundedUpate(collision, true);
        if (!check)
        {
            GameObject.Find("Player/Canvas/Die").SetActive(true);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private bool IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
            return true;
        }
        return false;
    }

    IEnumerator SmoothMove(Vector3 target, float delta)
    {
        float closeEnough = 0.2f;
        float distance = (rb.transform.position - target).magnitude;

        while (distance >= closeEnough)
        {
            transform.position = Vector3.Lerp(new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z), target, delta);
            yield return StartCoroutine(SmoothMove(target, delta));
            distance = (rb.transform.position - target).magnitude;
        }
        rb.transform.position = target;
    }
    void Clicked(bool left)
    {
        Vector3 relativeLocation = new Vector3(97, 0, 0);
        if (left) relativeLocation = new Vector3(-97, 0, 0);

        Vector3 targetLocation = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z) + relativeLocation;
        float timeDelta = 0.4f; 
        a = this.StartCoroutine(SmoothMove(targetLocation, timeDelta));
    }
}

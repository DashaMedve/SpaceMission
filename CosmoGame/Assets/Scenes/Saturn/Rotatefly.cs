using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatefly : MonoBehaviour
{
    public Rigidbody rb;
    public float strafeSpeed;

    protected bool strafeLeft;
    protected bool strafeRight;
    protected bool up;
    protected bool down;
    // Start is called before the first frame update
    void Start()
    {
        strafeSpeed = 100f;

        strafeLeft = false;
        strafeRight = false;
        up = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
            strafeRight = true;
        else
            strafeRight = false;

        if (Input.GetKey("a"))
            strafeLeft = true;
        else
            strafeLeft = false;

        if (Input.GetKey("w"))
            up = true;
        else
            up = false;
        if (Input.GetKey("s"))
            down = true;
        else
            down = false;
    }

    void FixedUpdate()
    {
        if (strafeLeft)
            rb.AddForce(-rb.transform.right * strafeSpeed * Time.deltaTime * 50f, ForceMode.Impulse);

        if (strafeRight)
            rb.AddForce(rb.transform.right * strafeSpeed * Time.deltaTime * 50f, ForceMode.Impulse);
        if (up)
        {
            rb.AddForce(rb.transform.up * strafeSpeed * Time.deltaTime * 50f, ForceMode.Impulse);
        }
        if (down)
        {
            rb.AddForce(-rb.transform.up * strafeSpeed * Time.deltaTime * 50f, ForceMode.Impulse);
        }
        rb.velocity = Vector3.zero;
    }
}

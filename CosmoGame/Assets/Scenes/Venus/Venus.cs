using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Venus : MonoBehaviour
{
    public Rigidbody rb;
    public float runSpeed;
    public float strafeSpeed;

    protected bool strafeLeft;
    protected bool strafeRight;
    protected bool forward;
    protected bool backward;

   // Coordinates coordinates = new Coordinates();

    // Start is called before the first frame update
    void Start()
    {
        runSpeed = 100f;
        strafeSpeed = 100f;

        strafeLeft = false;
        strafeRight = false;
        forward = false;
        backward = false;
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
            forward = true;
        else
            forward= false;

        if(Input.GetKey("s"))
            backward = true; 
        else
            backward = false;
    }

    void FixedUpdate()
    {
        if (strafeLeft)
            rb.transform.Rotate(Vector3.down,  strafeSpeed * Time.deltaTime);

        if (strafeRight)
            rb.transform.Rotate(Vector3.up, strafeSpeed * Time.deltaTime);

        if (backward)
            rb.AddForce(-rb.transform.forward * runSpeed * Time.deltaTime * 100f, ForceMode.Impulse);

        if (forward)
        {
            rb.AddForce(rb.transform.forward*runSpeed* Time.deltaTime*100f, ForceMode.Impulse);
        }
        rb.velocity = Vector3.zero;
    }
}

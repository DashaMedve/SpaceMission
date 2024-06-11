using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameObject ch1, ch2, ch3, ch4;
    public Rigidbody rb;
    // Start is called before the first frame update
    public GameObject[] enemyPrefab;
    void Start()
    {
        float iter = 50;
        Vector3 pos = transform.position + rb.transform.forward * iter;
        Collider Collider;
        while (true) {
            Collider[] intersecting = Physics.OverlapSphere(pos, 25f);
            if (intersecting.Length == 0)
            {
                ch1 = Instantiate(enemyPrefab[0], pos, Quaternion.identity);
                ch1.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
                ch1.transform.position = ch1.transform.position + new Vector3(0, 7.0f, 0); ;
                ch1.AddComponent<Star>();
                ch1.AddComponent<BoxCollider>();
                Collider = ch1.GetComponent<BoxCollider>();
                Collider.isTrigger = true;
                pos += rb.transform.forward * iter;
            }
            else
                break;
    }
    }

}

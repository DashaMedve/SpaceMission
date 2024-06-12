using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour {
    Animator animator;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {}

    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.CompareTag("Player")) {
            animator.SetTrigger("character_nearby");
        }
    }
}

using UnityEngine;

public class ExitTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<RaceManager>().ExitScene();
        }
    }
}

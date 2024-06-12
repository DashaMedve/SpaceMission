using UnityEngine;

public class RestartTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<RaceManager>().RestartScene();
        }
    }
}

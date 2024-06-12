using UnityEngine;

public class StartTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<RaceManager>().StartRace();
        }
    }
}

using UnityEngine;

public class FinishTrigger : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<RaceManager>().FinishRace();
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour {
    public Text timerText;
    private float startTime;
    private bool raceFinished = false;
    private bool raceStarted = false;

    void Start() {}

    void Update() {
        if (!raceFinished && raceStarted) {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
    }

    public void StartRace() {
        raceStarted = true;
        startTime = Time.time;
    }

    public void FinishRace() {
        raceFinished = true;
    }
}

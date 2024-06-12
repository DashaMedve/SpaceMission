using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceManager : MonoBehaviour {
    public Text timerText;
    public GameObject FinishPanel;
    public Text FinishText;
    private float startTime;
    private bool raceFinished = false;
    private bool raceStarted = false;

    void Start() {
        FinishPanel.SetActive(raceFinished);
    }

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
        FinishText.text = "Ура! Поздравляем!\n";
        FinishText.text += "Вы справились за\n";
        FinishText.text += timerText.text;
        FinishPanel.SetActive(raceFinished);
        Destroy(timerText);
    }

    public void ExitScene() {
        SceneManager.LoadScene(0);
    }

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

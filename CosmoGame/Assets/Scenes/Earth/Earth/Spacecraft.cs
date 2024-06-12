using UnityEngine;
using UnityEngine.SceneManagement;

public class Spacecraft : MonoBehaviour {
    private ItemDisplayManager itemDisplayManager;

    // Start is called before the first frame update
    void Start() {
        itemDisplayManager = FindObjectOfType<ItemDisplayManager>();
    }

    // Update is called once per frame
    void Update() {}

    void OnMouseDown() {
        Debug.Log("Полетели в космос!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Space");
    }
}

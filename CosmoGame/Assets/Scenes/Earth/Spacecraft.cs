using UnityEngine;

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
    }
}

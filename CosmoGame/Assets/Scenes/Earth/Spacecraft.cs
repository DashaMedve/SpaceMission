using UnityEngine;

public class Spacecraft : MonoBehaviour {
    private ItemDisplayManager itemDisplayManager;
    private Renderer renderer;
    private Color original_color;
    public Color new_color;

    // Start is called before the first frame update
    void Start() {
        itemDisplayManager = FindObjectOfType<ItemDisplayManager>();
        renderer = GetComponent<Renderer>();
        original_color = renderer.material.color;
    }

    // Update is called once per frame
    void Update() {}

    void OnMouseDown() {
        Debug.Log("Fly to the Space");
    }

    void OnMouseEnter() {
        renderer.material.color = new_color;
    }

    void OnMouseExit() {
        renderer.material.color = original_color;
    }
}

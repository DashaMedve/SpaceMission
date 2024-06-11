using UnityEngine;

public class SpacecraftDetail : MonoBehaviour {
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
        string itemName = GetComponent<Collider>().gameObject.name;
        itemDisplayManager.ItemCollected(itemName);
        Destroy(gameObject);
        Debug.Log("Item collected: " + itemName);
    }

    void OnMouseEnter() {
        renderer.material.color = new_color;
    }

    void OnMouseExit() {
        renderer.material.color = original_color;
    }
}

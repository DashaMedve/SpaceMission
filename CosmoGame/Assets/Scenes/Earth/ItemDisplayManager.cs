using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemDisplayManager : MonoBehaviour {
    public GameObject panel;
    public GameObject spacecraft;
    public Text itemListText;
    private bool isPannelVisible = false;
    private bool isSpacecraftlVisible = false;

    private List<string> itemsToFind = new List<string>();

    // Start is called before the first frame update
    void Start() {
        itemsToFind.Add("Core");
        itemsToFind.Add("Engine");
        itemsToFind.Add("Plasma");
        itemsToFind.Add("Thruster");
        itemsToFind.Add("Weapon_1");
        itemsToFind.Add("Weapon_2");
        itemsToFind.Add("Wing_1");
        itemsToFind.Add("Wing_2");
        itemsToFind.Add("Wing_3");
        itemsToFind.Add("Wing_4");
        panel.SetActive(isPannelVisible);
        spacecraft.SetActive(isSpacecraftlVisible);
        UpdateItemList();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            TogglePannel();
        }
    }

    public void TogglePannel() {
        isPannelVisible = !isPannelVisible;
        panel.SetActive(isPannelVisible);
    }

    private void UpdateItemList() {
        itemListText.text = "Items to find:\n";
        foreach (string item in itemsToFind) {
            itemListText.text += "- " + item + "\n";
        }
    }

    private void ThisIsWin() {
        itemListText.text = "Great!\n";
        itemListText.text += "Now you need\n";
        itemListText.text += "to find the\n";
        itemListText.text += "spacecraft that\n";
        itemListText.text += "you've already\n";
        itemListText.text += "created!";
    }

    public void ItemCollected(string itemName) {
        itemsToFind.Remove(itemName);
        if (itemsToFind.Count != 0) {
            UpdateItemList();
        }
        else {
            isSpacecraftlVisible = true;
            spacecraft.SetActive(isSpacecraftlVisible);
            ThisIsWin();
        }
    }
}

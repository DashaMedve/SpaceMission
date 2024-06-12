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
        itemsToFind.Add("Ядро");
        itemsToFind.Add("Двигатель");
        itemsToFind.Add("Плазма");
        itemsToFind.Add("Маневровый двигатель");
        itemsToFind.Add("Оружие_1");
        itemsToFind.Add("Оружие_2");
        itemsToFind.Add("Крыло_1");
        itemsToFind.Add("Крыло_2");
        itemsToFind.Add("Крыло_3");
        itemsToFind.Add("Крыло_4");
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
        itemListText.text = "Найдите компоненты:\n";
        foreach (string item in itemsToFind) {
            itemListText.text += "- " + item + "\n";
        }
    }

    private void ThisIsWin() {
        itemListText.text = "Отлично!\n";
        itemListText.text += "Теперь вам нужно\n";
        itemListText.text += "найти только что\n";
        itemListText.text += "построенный вами\n";
        itemListText.text += "космический\n";
        itemListText.text += "корабль!";
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

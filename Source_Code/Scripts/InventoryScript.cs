using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InventoryScript : MonoBehaviour
{
    public Player player;
    public GameObject inventoryPanel;
    public bool isShowPanel;

    public GameObject items;
    public StateScript stateScript;
    public GameScript gameScript;
    public InputActionReference buttonX; // Utilise le bouton X (Primary Left) pour l'inventaire
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
//        inventoryPanel.SetActive(false);
        player = GameObject.Find("Game Manager").GetComponent<GameScript>().GetPlayer();
        gameScript = GameObject.Find("Game Manager").GetComponent<GameScript>();
        stateScript = GameObject.Find("State Manager").GetComponent<StateScript>();
      
    }

    // Update is called once per frame
    void Update()
    {
        bool xButtonPressed = (buttonX != null && buttonX.action.WasReleasedThisFrame());
        if (xButtonPressed && player.inventory.HasItem("Bag") && !gameScript.filter.postProActivated)
        {
         
            isShowPanel = !isShowPanel;

            if(isShowPanel) {
                stateScript.state = State.Inventory;
        Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            } else {
                stateScript.state = State.Play;
        Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if(isShowPanel) {

                int uiSlotIndex = 0;
                for (int i = 0; i < player.inventory.GetCount(); i++)
                {
                    Item currentItem = player.inventory.items[i];

                    if (currentItem.itemName == "Bag") continue; // Skip displaying the Bag item

                    Transform slot = items.transform.GetChild(uiSlotIndex);
                    Image itemImage = slot.GetComponentInChildren<Image>().GetComponentInChildren<Image>();
                    TextMeshProUGUI itemText = slot.GetComponentInChildren<TextMeshProUGUI>();

                    itemImage.sprite = currentItem.icon;
                    itemImage.enabled = true;
                    itemImage.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    itemText.text = currentItem.itemName;

                    uiSlotIndex++; // Only increase slot index if item was shown
                }
            }
            inventoryPanel.SetActive(isShowPanel);
        }
    }
}

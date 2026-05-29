
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Audio;
public class GameScript : MonoBehaviour
{

    public GameObject pauseMenu;
    public bool isShowOptions;
    public GameObject optionsMenu;
    public Player player;
    public StateScript stateScript;
    public bool isShowPause;

    public GameObject playerObject;
    public Image holdPoint;
    public ItemsList itemsList;

    public GameObject chemistryCollider;
    public GameObject playgroundCollider;
    public GameObject resultColor;
    public InventoryScript inventoryScript;
    public GameObject colorsMenu;

    public AudioClip goodSound;
    public AudioClip wrongSound;

    public bool isAllowedToAnswerChemistry;


    public bool hasFinishedChemistry;
    public bool hasFinishedPlayground;

    public TimerScript timerScript;

    public GameObject mainCamera;

    public DaltonismFilter filter;

    public string currentRoom;
    public bool startedStorm = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
 void Awake()
{
    player = new Player();
    itemsList = GameObject.Find("Items Manager").GetComponent<ItemsList>();
       
}
    void Start()
    {
        stateScript = GameObject.Find("State Manager").GetComponent<StateScript>();
        pauseMenu = GameObject.Find("Pause Menu");
        optionsMenu = GameObject.Find("Option Menu");
        inventoryScript = GameObject.Find("Inventory Manager").GetComponent<InventoryScript>();
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        ChangeCurrentItem(itemsList.items["Aucun"]);
        //player.inventory.AddItem(itemsList.items["Bag"]);
        //player.inventory.AddItem(itemsList.items["Book"]);
        timerScript = GameObject.Find("Timer Manager").GetComponent<TimerScript>();
        mainCamera = GameObject.Find("FollowCam");
        filter = GameObject.Find("DaltonismManager").GetComponent<DaltonismFilter>();


    }


    public void ChangeSelectedColor(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        GameObject clickedObject = pointerData.pointerPress;
        Debug.Log("clique sur couleur");
        if(clickedObject.name != "rightImage")
        {
          resultColor.GetComponent<AudioSource>().PlayOneShot(wrongSound);
          

        } else
        {
             resultColor.GetComponent<AudioSource>().PlayOneShot(goodSound);
            hasFinishedChemistry = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        colorsMenu.SetActive(false);
        isAllowedToAnswerChemistry = false;
        stateScript.state = State.Play;

    }
    // Update is called once per frame
    void Update()
    {
 
     if(Input.GetKeyUp(KeyCode.Escape)) {
          if(isShowOptions) {
                CloseOptionsMenu();
            } else {
        if(isShowPause) {
            ClosePauseMenu();
          
        } else {
            if(stateScript.state == State.Play) {
            ShowPauseMenu();
            }
        }
            }
        
     }
    }

      public void ChangeCurrentItem(Item item) {
        player.currentItem = item;
//         holdPoint.sprite = item.icon;
        stateScript.state = State.Play;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
//        inventoryScript.inventoryPanel.SetActive(false);

    }
    public void ChangeCurrentItemOnButtonClick(BaseEventData data) {
        Debug.Log("passage");
         PointerEventData pointerData = data as PointerEventData;
        GameObject clickedObject = pointerData.pointerPress;
     
        TextMeshProUGUI itemText = clickedObject.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        string itemName = itemText.text;
    
        if(itemName.Length == 0) {
          itemName = "Aucun";
        }
    
      Item item = itemsList.items[itemName];
        ChangeCurrentItem(item);
        
    }
    public Player GetPlayer() {
        return player;
    }
    public void ShowPauseMenu() {
        isShowPause = true;
        stateScript.state = State.Pause;
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }
    public void ClosePauseMenu() {
        isShowPause = false;
        stateScript.state = State.Play;
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
        pauseMenu.SetActive(false);
    }


    public void ShowOptionsMenu() {
        isShowPause = false;
        isShowOptions = true;
        stateScript.state = State.Options;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void CloseOptionsMenu() {
        isShowOptions = false;
        isShowPause = true;
        stateScript.state = State.Pause;
            optionsMenu.SetActive(false);
            pauseMenu.SetActive(true);

    }
}




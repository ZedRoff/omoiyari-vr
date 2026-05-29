using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemUseScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public StateScript stateScript;
    public GameScript gameScript;
    public GameObject pageDaltonism;
    public RawImage page1;
    public RawImage page2;
    private int pageCount = 2;
    private int bookIndex = 0;
    void Start()
    {
        stateScript = GameObject.Find("State Manager").GetComponent<StateScript>();
        gameScript = GameObject.Find("Game Manager").GetComponent<GameScript>();
        page1.gameObject.SetActive(false);
        page2.gameObject.SetActive(false);
        pageDaltonism.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {

            Debug.Log("e");
            if (stateScript.state == State.InBook)
            {
                stateScript.state = State.Play;
                page1.gameObject.SetActive(false);
                page2.gameObject.SetActive(false);
                bookIndex = 0;
                return;
            }
           
            if(gameScript.player.currentItem.itemName == "BookChimie")
            {
                page1.gameObject.SetActive(true);
                stateScript.state = State.InBook;
                bookIndex = 1;
            }
            if (gameScript.player.currentItem.itemName == "BookDaltonism" && stateScript.state == State.Play)
            {
               
                pageDaltonism.SetActive(true);
                stateScript.state = State.InDaltonismBook;
                return;
            }
            if (gameScript.player.currentItem.itemName == "BookDaltonism" && stateScript.state == State.InDaltonismBook)
            {
                stateScript.state = State.Play;
                pageDaltonism.SetActive(false);
                return;
            }

        }
        else if(Input.GetKeyUp(KeyCode.RightArrow) && stateScript.state == State.InBook)
        {
            // 0 : page 1, 1 : page 2
            if(bookIndex == 0)
            {
                page1.gameObject.SetActive(true);
                page2.gameObject.SetActive(false);
                bookIndex = 1;
            } else 
            {

                page1.gameObject.SetActive(false);
                page2.gameObject.SetActive(true);
                bookIndex = 0;
            }
        } else if(Input.GetKeyUp(KeyCode.LeftArrow) && stateScript.state == State.InBook)
        {
            // 0 : page 1, 1 : page 2
            if (bookIndex == 0)
            {
                page1.gameObject.SetActive(true);
                page2.gameObject.SetActive(false);
                bookIndex = 1;
            }
            else
            {

                page1.gameObject.SetActive(false);
                page2.gameObject.SetActive(true);
                bookIndex = 0;
            }
        }
    }
}

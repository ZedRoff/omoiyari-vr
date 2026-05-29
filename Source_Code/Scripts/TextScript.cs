using UnityEngine;
using TMPro;
using System.Collections;

public class TextScript : MonoBehaviour
{
    public TextMeshProUGUI bulle;
    public GameObject boxBulle;
    public GameObject buttonX;
    public StateScript stateScript;

    private bool isTyping = false;
    private int dialogIndex = 0;

    string[][] texts = null;

    void Start()
    {
        stateScript = GameObject.Find("State Manager").GetComponent<StateScript>();
       boxBulle.SetActive(false);
       buttonX.SetActive(false);
      
    }
    public void StartDialog(string[][] pTexts)
    {
        texts = pTexts;
    }

    public bool IsInDialog()
    {

        return texts != null;
    }
    public bool IsTyping()
    {
        return isTyping;
    }
    IEnumerator TypeLine(string speaker, string line)
    {

        boxBulle.SetActive(true);
        buttonX.SetActive(false);
        isTyping = true;
        string fullText = $"<u>{speaker}</u> : ";
        bulle.text = fullText;

        foreach (char c in line)
        {
            fullText += c;
            bulle.text = fullText;
            yield return new WaitForSeconds(0.02f);
        }

        isTyping = false;
       buttonX.SetActive(true);
    }
    void CloseBulle()
    {
        if (stateScript.state == State.FinalDialog)
        {
            stateScript.state = State.Quiz;
        }
       
        else { 
            stateScript.state = State.Play;
        }
        StopAllCoroutines();
        isTyping = false;
        bulle.text = "";
        boxBulle.SetActive(false);
        buttonX.SetActive(false);
        dialogIndex = 0;
        texts = null;
    }

    // Update is called once per frame
    void Update()
    {
       if(dialogIndex == 0 && !isTyping && texts != null && texts.Length != 0) 
       { // base case, when the first dialog appears
            StartCoroutine(TypeLine(texts[0][0], texts[0][1]));
                dialogIndex++;
       }
        else if (Input.GetKey(KeyCode.X) && !isTyping && texts != null && texts.Length != 0)
        { // case if the first dialog is done and user has to press X to go to the others
            if(dialogIndex < texts.Length)
            {
                StartCoroutine(TypeLine(texts[dialogIndex][0], texts[dialogIndex][1]));
                dialogIndex++;

            } else
            { // case if the last dialog is done, have to reset all
                CloseBulle();
            }
          
        }  
  
    }
}

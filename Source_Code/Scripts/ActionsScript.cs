using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionsScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

// string: name, bool: state
    public Dictionary<string, bool> tasks = new Dictionary<string, bool>();

      public GameObject part;
      public Transform actionsParent;
      public TextMeshProUGUI checkText;
    void Start()
    {
         checkText = GameObject.Find("CheckText").GetComponent<TextMeshProUGUI>();
        if (tasks.Count == 0) checkText.text = "0/0";
    }

    public void AddTask(string name, bool state) {
        tasks.Add(name, state);
        GameObject go = Instantiate(part, actionsParent);
        go.transform.localScale = Vector3.one;

        TextMeshProUGUI label = go.GetComponentInChildren<TextMeshProUGUI>();
        Button button = go.GetComponentInChildren<Button>();
        
        button.GetComponent<Image>().color = state ? new Color(0,1,0) : new Color(1,0,0);
        label.text = name;

        UpdateProgress();
    }
    void UpdateProgress() {
int doneTasks = 0;
    foreach (var task in tasks)
    {
        if (task.Value) doneTasks++;
    } 
    
    checkText.text = $"{doneTasks}/{tasks.Count}";
    }
    public void RemoveTask(string name) {
        tasks.Remove(name);
          foreach (Transform child in actionsParent) {
            if(child.GetComponentInChildren<TextMeshProUGUI>().text == name) Destroy(child.gameObject);
          if(tasks.Count == 0) checkText.text = "0/0";
        }
    }
    public void FinishTask(string name) {
        if(!tasks.ContainsKey(name)) return;
        tasks[name] = true;

         foreach (Transform child in actionsParent) {
        TextMeshProUGUI label = child.GetComponentInChildren<TextMeshProUGUI>();
        if (label != null && label.text == name) {
            label.fontStyle = FontStyles.Strikethrough;

            Button button = child.GetComponentInChildren<Button>();
            if (button != null) {
                button.GetComponent<Image>().color = new Color(0, 1, 0); 
            }

            break;
        }
    }

    UpdateProgress();
    }
    public void FlushTasks() {
        tasks.Clear();
          foreach (Transform child in actionsParent) {
        Destroy(child.gameObject);  
    }
    UpdateProgress();
    }
 
}

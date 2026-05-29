using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameScript gameScript;
    void Start()
    {
        gameScript = GameObject.Find("Game Manager").GetComponent<GameScript>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(gameScript.currentRoom == "final")
        {
            Transform playerTransform = gameScript.playerObject.transform;
          

        }
    }
}

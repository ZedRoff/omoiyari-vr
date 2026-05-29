using FPSBasics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
public class CollisionScript : MonoBehaviour
{
    float playerBaseHeight = 2f;

  
 

    public CollisionManager collisionManager;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collisionManager = GameObject.Find("Collision Manager").GetComponent<CollisionManager>();

    }   
    private void OnTriggerStay(Collider other)
    {
        if (gameObject.name == "replyCollider" || gameObject.name == "DeuteranopieCollider1" || gameObject.name == "DeuteranopieCollider2" || gameObject.name == "DeuteranopieCollider2 (1)" || gameObject.name == "DeuteranopieCollider1 (1)" || gameObject.name == "TritanopieCollider1" || gameObject.name == "TritanopieCollider2" || gameObject.name == "TritanopieCollider1 (1)" || gameObject.name == "TritanopieCollider2 (1)")
        {

            collisionManager.gameScript.filter.Activate(1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("draggable"))
        {

            Debug.Log(other.name);
        }
        
        if (gameObject.name == "obj1")
        {
           
            CharacterController cc = collisionManager.player.GetComponent<CharacterController>();

            cc.enabled = false;
            collisionManager.player.transform.position = collisionManager.tp1.transform.position;
            cc.enabled = true;

            collisionManager.keyStrokeManager.step = -1;
            collisionManager.keyStrokeManager.internalStep = 0;
            collisionManager.obj1.GetComponent<BoxCollider>().enabled = true;
            collisionManager.obj1.GetComponent<BoxCollider>().isTrigger = false;

        }
        if (gameObject.name == "obj2")
        {
            CharacterController cc = collisionManager.player.GetComponent<CharacterController>();

            cc.enabled = false;
            collisionManager.player.transform.position = collisionManager.tp2.transform.position;
            cc.enabled = true;

            collisionManager.keyStrokeManager.step = -1;
            collisionManager.keyStrokeManager.internalStep = 0;

            collisionManager.obj2.GetComponent<BoxCollider>().enabled = true;
            collisionManager.obj2.GetComponent<BoxCollider>().isTrigger = false;
        }
        if (gameObject.name == "obj3")
        {
            CharacterController cc = collisionManager.player.GetComponent<CharacterController>();

            cc.enabled = false;
            collisionManager.player.transform.position = collisionManager.tp3.transform.position;
            cc.enabled = true;

            collisionManager.keyStrokeManager.step = -1;
            collisionManager.keyStrokeManager.internalStep = 0;

            collisionManager.obj3.GetComponent<BoxCollider>().enabled = true;
            collisionManager.obj3.GetComponent<BoxCollider>().isTrigger = false;
        }
        if (gameObject.name == "WaterSurface")
        {   
            CharacterController cc = collisionManager.player.GetComponent<CharacterController>();

            cc.enabled = false;
            collisionManager.player.transform.position = collisionManager.tp4.transform.position;
            cc.enabled = true;


            collisionManager.obj3.GetComponent<BoxCollider>().enabled = true;
            collisionManager.obj3.GetComponent<BoxCollider>().isTrigger = false;
        }
        if (other.CompareTag("Player")) {

            if(gameObject.name == "StormEndCollider")
            {
                collisionManager.stateScript.state = State.AutismEnd;
              //  collisionManager.sun.enabled = true;
            collisionManager.mainSun.SetActive(true);
            collisionManager.dirLight.enabled = true;
             //   dirLight.enabled = true;
                collisionManager.gameScript.startedStorm = false;
                collisionManager.map.SetActive(true);
                collisionManager.stormManager.enabled = false;
                collisionManager.jauge.SetActive(false);
                Destroy(gameObject);
            }
            if(gameObject.name == "StartCollider") {
                collisionManager.stateScript.state = State.StartDialog;
                collisionManager.actionsScript.AddTask("Trouver votre sac", false);
                collisionManager.actionsScript.AddTask("Finir l'épreuve 2", false);
                collisionManager.actionsScript.AddTask("Sortir du Mont Fuji", false);
                Destroy(gameObject);
            }
            if(gameObject.name == "EndTask1MtFuji")
            {
                collisionManager.actionsScript.FinishTask("Finir l'épreuve 2");
            }
            if (gameObject.name == "EndTask2MtFuji")
            {
                collisionManager.actionsScript.FinishTask("Sortir du Mont Fuji");
                collisionManager.rainAudio.Stop();
                collisionManager.rainParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            if (gameObject.name == "Quest1Collider") {
             
                    collisionManager.actionsScript.AddTask("Trouver votre sac", false);
                    collisionManager.actionsScript.AddTask("Ouvrir la porte", false);
                collisionManager.actionsScript.AddTask("Atteindre la salle principale", false);
                Destroy(gameObject);
            }  
            if(gameObject.name == "FlushCollider") {
                    collisionManager.actionsScript.FlushTasks();
                      Destroy(gameObject);
            }

           


        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "ScaleCollider")
        {
            collisionManager.gameScript.stateScript.state = State.CantEscape;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(gameObject.name == "SliperyEnter")
        {
            collisionManager.mv.GetComponent<UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets.DynamicMoveProvider>().moveSpeed = 5f;
            collisionManager.mv.GetComponent<DynamicMoveProvider>().slippery = true;
        }
        if (gameObject.name == "SliperyExit")
        {
            collisionManager.mv.GetComponent<UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets.DynamicMoveProvider>().moveSpeed = 15f;
            collisionManager.mv.GetComponent<DynamicMoveProvider>().slippery = false;
            collisionManager.player.GetComponent<FootSteps>().enabled = true;
            collisionManager.player.GetComponent<WheelchairController>().enabled = false;
            // collisionManager.player.GetComponent<FPSWalkerEnhanced>().jumpSpeed = 8f;

            // On trouve man_sitting AVANT de désactiver la scène
            GameObject manSitting = GameObject.Find("man_sitting");

            // Désactive la scène
            GameObject.Find("scene").SetActive(false);

            if (manSitting != null)
            {
                // 1. SUPPRIMER TOUS LES ENFANTS DE MAN_SITTING
                // On boucle à l'envers pour éviter les bugs d'indexation pendant la destruction
                for (int i = manSitting.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(manSitting.transform.GetChild(i).gameObject);
                }

                // 2. TRANSFORMATION EN CAPSULE (On change son apparence actuelle)
                MeshFilter meshFilter = manSitting.GetComponent<MeshFilter>();
                if (meshFilter == null) meshFilter = manSitting.AddComponent<MeshFilter>();

                GameObject tempCapsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                meshFilter.sharedMesh = tempCapsule.GetComponent<MeshFilter>().sharedMesh;

                MeshRenderer meshRenderer = manSitting.GetComponent<MeshRenderer>();
                if (meshRenderer == null) meshRenderer = manSitting.AddComponent<MeshRenderer>();
                meshRenderer.sharedMaterial = tempCapsule.GetComponent<MeshRenderer>().sharedMaterial;

                Destroy(tempCapsule);

                // Ajustement du collider
                if (manSitting.TryGetComponent<Collider>(out Collider oldCollider))
                {
                    Destroy(oldCollider);
                }
                manSitting.AddComponent<CapsuleCollider>();

                Debug.Log("man_sitting a été vidé de ses enfants et transformé en Capsule !");
            }
            else
            {
                Debug.LogError("Impossible de trouver 'man_sitting'.");
            }

            // collisionManager.player.GetComponent<FPSWalkerEnhanced>().walkSpeed = 25f;
        }
        if (gameObject.name == "ScaleCollider")
        {
            collisionManager.actionsScript.FinishTask("Atteindre la salle principale");
                collisionManager.gameScript.playerObject.transform.localScale = Vector3.one;
                collisionManager.gameScript.playerObject.transform.position += new Vector3(0, 0.5f * playerBaseHeight * 0.5f, 0);
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
            collisionManager.actionsScript.FlushTasks();
            collisionManager.actionsScript.AddTask("Trouver le manuel de chimie", false);
            collisionManager.actionsScript.AddTask("Trouver le manuel de daltonisme", false);
            collisionManager.actionsScript.AddTask("Trouver la clé", false);
            collisionManager.actionsScript.AddTask("Atteindre le playground", false);
        }

        
        if (gameObject.name == "FinalRoomCollider")
        {
            collisionManager.stateScript.state = State.QuizRoomDialog;
            collisionManager.actionsScript.FlushTasks();
            collisionManager.actionsScript.AddTask("Parler à Satoshi", false);
            collisionManager.actionsScript.AddTask("Trouver l'épreuve", false);
            collisionManager.actionsScript.AddTask("Lire le manuel daltonisme", false);
            collisionManager.actionsScript.AddTask("Lire le manuel dyslexie", false);
            collisionManager.actionsScript.AddTask("Entrer le code", false);
        }
        
        if (gameObject.name == "ChemistryCollider" && !collisionManager.gameScript.hasFinishedChemistry)
        {
         //   collisionManager.stateScript.state = State.StartChemistryDialog;
            collisionManager.gameScript.timerScript.StartTimer(5 * 60); 
            collisionManager.actionsScript.FlushTasks();
            collisionManager.actionsScript.AddTask("Trouver les 5 flacons", false);
            collisionManager.actionsScript.AddTask("Verser dans le becher", false);
            collisionManager.actionsScript.AddTask("Noter la solution", false);
            Destroy(gameObject);
        }
        if(gameObject.name == "replyCollider")
        {

            collisionManager.gameScript.filter.DeActivateAll();
        }
        if(gameObject.name == "DeuteranopieCollider1" || gameObject.name == "DeuteranopieCollider2" || gameObject.name == "DeuteranopieCollider2 (1)"  || gameObject.name == "DeuteranopieCollider1 (1)" || gameObject.name == "TritanopieCollider1" || gameObject.name == "TritanopieCollider2" || gameObject.name == "TritanopieCollider1 (1)" || gameObject.name == "TritanopieCollider2 (1)")
        {
            Debug.Log("outside");
            if (collisionManager.gameScript.filter.postProActivated)
            {
                Debug.Log("out");
                collisionManager.gameScript.filter.DeActivateAll();
               // cloudMngr.SetActive(true);
            }
          
        }
        if(gameObject.name == "StormCollider" && !collisionManager.gameScript.startedStorm)
        {
         //   collisionManager.stateScript.state = State.AutismStart;
            collisionManager.cloudMngr.SetActive(false);
            //collisionManager.sun.enabled = false;
            collisionManager.mainSun.SetActive(false);
            collisionManager.dirLight.enabled = false;
            collisionManager.gameScript.startedStorm = true;
         //   collisionManager.map.SetActive(false);
            collisionManager.stormManager.enabled = true;
           // collisionManager.jauge.SetActive(true);
            collisionManager.actionsScript.AddTask("Trouver la clé", false);
            collisionManager.actionsScript.AddTask("Trouver la sortie", false);
             Destroy(gameObject);
        } else if(gameObject.name == "StormCollider" && collisionManager.gameScript.startedStorm)
        {
            collisionManager.gameScript.startedStorm = false;
            //collisionManager.map.SetActive(true);
            //collisionManager.jauge.SetActive(false);
        }
       
        if(gameObject.name == "PlayGroundCollider" && !collisionManager.gameScript.hasFinishedPlayground)
        {
         
            if (!collisionManager.gameScript.filter.playGroundStarted)
            {
                collisionManager.gameScript.filter.playGroundStarted = true;
                collisionManager.stateScript.state = State.StartPlayGroundDialog;
                collisionManager.gameScript.timerScript.StartTimer(10 * 60);
                collisionManager.actionsScript.FinishTask("Atteindre le playground");
                collisionManager.actionsScript.FlushTasks();
                collisionManager.actionsScript.AddTask("Trouver les 9 triangles", false);
                collisionManager.actionsScript.AddTask("Finir la fresque", false);
             
            } else
            {
                if (collisionManager.gameScript.filter.postProActivated)
                {
                    collisionManager.gameScript.filter.DeActivateAll();
                }
                
            }
    
        }
        
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}

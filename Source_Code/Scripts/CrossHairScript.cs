
using System.Collections.Generic;
using System.Linq;
using DoorScript;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors; // C'est souvent celui-ci qui manque
using UnityEngine.InputSystem; // Requis
public class CrossHairScript : MonoBehaviour
{
   
    private float rayDistance = 5.0f;
    public Camera camView;
    public Sprite upKeyImage;
    public Sprite rightKeyImage;
    public Sprite downKeyImage;
    public Sprite leftKeyImage;
    
    public Sprite EKeyImage;
    public Sprite RKeyImage;
    public Sprite TKeyImage;
    public Sprite GKeyImage;
    public Sprite VKeyImage;


    public Sprite BKeyImage;
    public Sprite AKeyImage;
    public Sprite MKeyImage;
    public Sprite defaultImage;
    public Image crossHair;
    public InventoryScript inventoryScript;
    public ItemsList itemsList;
    private float defaultSize = 3.0f;
    private float scaledSize = 12.0f;
    private float doubleScaledSize = 2 * 12.0f;
    public Image holdPoint;
    public GameScript gameScript;
    public ActionsScript actionsScript;

    public List<string> contenus = new List<string>();
    public Material bonneCouleur;
    public Material mauvaiseCouleur;
    public Material pasAssezCouleur;
    public Material videCouleur;
    public GameObject becher;
    public GameObject status;
    public TextMeshPro statusText;
    public List<string> ingredients;

    public GameObject colorsMenu;

    public Material yellowMaterial;
    public Material greenMaterial;
    public Material redMaterial;
    public Material blueMaterial;
    public Material violetMaterial;
    public Material blackMaterial;


    private Dictionary<string, Material> requiredMaterials = new Dictionary<string, Material>();
    private List<GameObject> coloredObjects = new List<GameObject>();
    public bool testBool = false;

    public GameObject puzzlePieces;

    public GameObject finalMenu;
    public GameObject bookDyslexie;
    public GameObject bookDaltonism;

    public GameObject teleport1;
    public GameObject teleport2;
    private GameObject player;

    public GameObject stair1;
    public GameObject stair2;

    public ParticleSystem fire1;
    public ParticleSystem fire2;
    public ParticleSystem fire3;
    public ParticleSystem fire4;

    public GameObject part1;

    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;


    public GameObject tp1;
    public GameObject tp2;
    public GameObject tp3;

    public KeyStrokeManager keyStrokeManager;

    public GameObject rideaux;

    public Light spotLight;

    private Vector3 rideauxPositionBase;
    private bool enCours = false;
    public NearFarInteractor rayInteractor;

    public NearFarInteractor rayRightInteractor;
    [Header("Main Gauche")]
    public InputActionReference selectLeft;     // Grip (Saisie)
    public InputActionReference triggerLeft;    // Index
    public InputActionReference buttonY; // Bind : <XRController>{LeftHand}/secondaryButton


    [Header("Main Droite")]
    public InputActionReference triggerRight;   // Index
    public InputActionReference selectRight;    // Grip (Saisie)
    public InputActionReference buttonA; // Bind : <XRController>{RightHand}/primaryButton
    public InputActionReference buttonB; // Bind : <XRController>{RightHand}/secondaryButton
    private Renderer m_LastHoveredRenderer;
    private Color m_OriginalColor;

    private bool m_IsDragging = false; // Permet de verrouiller l'objet sélectionné
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    public Transform pokeInteractor;

    public Transform pokeRightInteractor;
    public GameObject[] tangramPieces;
    public GameObject torch;
    public GameObject flashLight;
    private readonly HashSet<string> validNames = new HashSet<string> { "aOHN", "Cu2O", "NAHGO3", "Cu504", "NAH5O3" };
    public GameObject colliderTori;
    public GameObject joyconleft;
    public GameObject joyconRight;

    public GameObject rightController;


    private bool hasBecher = false;




    [Header("Configuration Agitation")]
    private int crossZeroCount = 0;   // Compteur de passages positif/négatif
    private bool isPrevPositive = false;
    private bool isFirstFrame = true;
    private float lastChangeTime = 0f;
    private float resetTimeout = 0.6f; // Temps max en secondes entre deux agitations

    private bool hasRamen = false;

    public GameObject code;

    public GameObject endMenu;
    void Start()
    {
        rideaux = GameObject.Find("rideaux");
        fire1.GetComponent<ParticleSystem>().Stop();
        fire2.GetComponent<ParticleSystem>().Stop();
        fire3.GetComponent<ParticleSystem>().Stop();
        fire4.GetComponent<ParticleSystem>().Stop();
        inventoryScript = GameObject.Find("Inventory Manager").GetComponent<InventoryScript>();
        itemsList = GameObject.Find("Items Manager").GetComponent<ItemsList>();
        gameScript = GameObject.Find("Game Manager").GetComponent<GameScript>();
        keyStrokeManager = GameObject.Find("KeyStroke Manager").GetComponent<KeyStrokeManager>();

        actionsScript = GameObject.Find("Actions Manager").GetComponent<ActionsScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        teleport1 = GameObject.Find("teleport1");
        teleport2 = GameObject.Find("teleport2");
        statusText = status.GetComponent<TextMeshPro>();
        statusText.text = $"Ajouter le mélange";
        colorsMenu.SetActive(false);

        ingredients.Add(itemsList.items["aOHN"].itemName);
        ingredients.Add(itemsList.items["Cu504"].itemName);
        ingredients.Add(itemsList.items["NAH5O3"].itemName);
        ingredients.Add(itemsList.items["NAHGO3"].itemName);
        ingredients.Add(itemsList.items["Cu2O"].itemName);
        finalMenu.SetActive(false);
        bookDyslexie.SetActive(false);
        bookDaltonism.SetActive(false);
        torch = GameObject.Find("Torch");
      //  flashLight = GameObject.Find("FlashLight");
        colliderTori = GameObject.Find("FenceDouble01");

    }
    private void ResetLastHoverColor()
    {
        if (m_LastHoveredRenderer != null)
        {

            // On remet la couleur d'avant
            m_LastHoveredRenderer.material.color = m_OriginalColor;
            m_LastHoveredRenderer = null;
        }
    }
    public bool AreAllColorsSet()
    {
        foreach (var item in requiredMaterials)
        {
            GameObject obj = GameObject.Find(item.Key);

            if (obj != null)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null && renderer.material != item.Value)
                {
                    return false;
                }
            }
            else
            {
                Debug.LogError($"Object {item.Key} not found!");
                return false;
            }
        }
        return true;
    }

    public void VerserDansBecher(string itemName)
    {
            contenus.Add(itemName);
    }
    IEnumerator CheckSolution()
    {
        actionsScript.FinishTask("Noter la solution");
        statusText.text = "En attente...";
        yield return new WaitForSeconds(2.0f);
        Renderer rend = becher.GetComponent<Renderer>();
        statusText.text = "Notez votre résultat au tableau";
        if (contenus.Count != 5)
        {
            rend.material = pasAssezCouleur;
        }
        else
        {
            if (CheckMix())
            {
                rend.material = bonneCouleur;
            }
            else
            {
                rend.material = mauvaiseCouleur;
            }
        }
        gameScript.isAllowedToAnswerChemistry = true;
    }

    void ResetMelange()
    {
        gameScript.isAllowedToAnswerChemistry = false;
        statusText.text = "Ajouter le mélange";
        Renderer rend = becher.GetComponent<Renderer>(); 
        rend.material = videCouleur;
        contenus.Clear();
    }
    // Update is called once per frame
    void Update()
    {

    if(keyStrokeManager.step == 0)
        {
            if(keyStrokeManager.internalStep == 0)
            {
                crossHair.sprite = rightKeyImage;
            } else if(keyStrokeManager.internalStep == 1)
            {
                crossHair.sprite = upKeyImage;
            }
            else if (keyStrokeManager.internalStep == 2)
            {
                crossHair.sprite = leftKeyImage;
            }
            
            crossHair.rectTransform.localScale = new Vector3(doubleScaledSize, doubleScaledSize, doubleScaledSize);

            return;
        }


        if (keyStrokeManager.step == 1)
        {
            if (keyStrokeManager.internalStep == 0)
            {
                crossHair.sprite = downKeyImage;
            }
            else if (keyStrokeManager.internalStep == 1)
            {
                crossHair.sprite = upKeyImage;
            }
            else if (keyStrokeManager.internalStep == 2)
            {
                crossHair.sprite = downKeyImage;
            }

            crossHair.rectTransform.localScale = new Vector3(doubleScaledSize, doubleScaledSize, doubleScaledSize);

            return;
        }


        if (keyStrokeManager.step == 2)
        {
            if (keyStrokeManager.internalStep == 0)
            {
                crossHair.sprite = leftKeyImage;
            }
            else if (keyStrokeManager.internalStep == 1)
            {
                crossHair.sprite = rightKeyImage;
            }
            else if (keyStrokeManager.internalStep == 2)
            {
                crossHair.sprite = upKeyImage;
            }

            crossHair.rectTransform.localScale = new Vector3(doubleScaledSize, doubleScaledSize, doubleScaledSize);

            return;
        }


        bool gripLeftClicked = selectLeft.action.WasReleasedThisFrame();

        if (gripLeftClicked)
        {
            // On récupère le composant Light
            Light lightComponent = flashLight.GetComponent<Light>();

            if (lightComponent != null)
            {
                // CORRECTION : On inverse le statut de la lumière elle-même (.enabled)
                lightComponent.enabled = !lightComponent.enabled;
            }
        }

        // 1. On récupère la direction du "haut" local du contrôleur dans le monde
        Vector3 controllerUp = rightController.transform.up;

        // 2. On compare cette direction avec le "haut" absolu du monde (0, 1, 0)
        float dotProduct = Vector3.Dot(controllerUp, Vector3.up);

        // 3. Si la valeur passe en dessous de 0, cela signifie que le contrôleur pointe vers le bas
        // On utilise -0.5f pour laisser une petite marge (environ 120 degrés d'inclinaison)
        if (dotProduct < -0.5f && hasBecher)
        {
            ResetMelange();

            // Tu peux mettre ton code ici (par exemple vider le Bécher si le joueur le retourne)
        }   
        bool aPressed = buttonA.action.WasReleasedThisFrame(); // Anciennement lié à une partie de abRight
        bool bPressed = buttonB.action.WasReleasedThisFrame(); // Anciennement lié à une partie de abRight

        if (rightController != null)
        {
            // 1. On récupère la rotation locale (comme dans ton Debug.Log)
            float rawX = rightController.transform.localRotation.eulerAngles.x;

            // 2. Convertir l'angle d'Unity (0 à 360) en format -180 à +180
            // Exemple : 355° devient -5° (inclinaison vers le bas)
            float verticalAngle = rawX > 180f ? rawX - 360f : rawX;

            // Sécurité : On réinitialise le compteur si le joueur arrête d'agiter
            if (crossZeroCount > 0 && Time.time - lastChangeTime > resetTimeout)
            {
                crossZeroCount = 0;
                isFirstFrame = true;
            }

            // On ignore les micro-mouvements proches de 0 pour éviter les faux positifs (zone morte de 2 degrés)
            if (Mathf.Abs(verticalAngle) > 2f)
            {
                bool isCurrentPositive = verticalAngle > 0f;

                if (!isFirstFrame)
                {
                    // 3. Détection du basculement : le signe actuel est différent du précédent
                    if (isCurrentPositive != isPrevPositive)
                    {
                        crossZeroCount++;
                        lastChangeTime = Time.time;

                        // 3 passages de positif à négatif (ou inversement) = agitation validée
                        if (crossZeroCount >= 3)
                        {
                            StartCoroutine(CheckSolution());

                            // Reset après activation
                            crossZeroCount = 0;
                            isFirstFrame = true;
                        }
                    }
                }

                // Sauvegarde de l'état pour la frame suivante
                isPrevPositive = isCurrentPositive;
                isFirstFrame = false;
            }
        }
        if (aPressed && code.activeInHierarchy)
        {
          
            // 1. On récupère le conteneur "ReplyPart"
            Transform g = code.transform.Find("ReplyPart");
           
            if (g != null)
            {
                // 2. On va chercher le composant TextMeshPro directement dans le sous-chemin de chaque code
                TMPro.TextMeshProUGUI t1 = g.Find("code1/Text (TMP)")?.GetComponent<TMPro.TextMeshProUGUI>();
                TMPro.TextMeshProUGUI t2 = g.Find("code2/Text (TMP)")?.GetComponent<TMPro.TextMeshProUGUI>();
                TMPro.TextMeshProUGUI t3 = g.Find("code3/Text (TMP)")?.GetComponent<TMPro.TextMeshProUGUI>();
                TMPro.TextMeshProUGUI t4 = g.Find("code4/Text (TMP)")?.GetComponent<TMPro.TextMeshProUGUI>();
               
                // 3. Sécurité : On vérifie qu'on a bien trouvé tous les textes pour éviter les erreurs (NullReference)
                if (t1 != null && t2 != null && t3 != null && t4 != null)
                {
                    // On assemble les 4 textes pour former le code complet (ex: "1234")
                    string codeEntre = t1.text + t2.text + t3.text + t4.text;

                    Debug.Log($"[VERIFICATION] Le joueur a entré le code : {codeEntre}");

                    // 4. Tu n'as plus qu'à faire ta condition de victoire !
                    if (codeEntre == "1234") // Remplace par ton vrai code secret
                    {
                        Debug.Log("<color=green>Bravo ! Code correct !</color>");
                        // Insère ici l'action de réussite (ouvrir la porte, arrêter l'alarme...)
                        endMenu.SetActive(true);
                    }
                }
                else
                {
                    Debug.LogError("Impossible de trouver l'un des 'Text (TMP)' dans l'arborescence de ReplyPart ! Vérifie bien l'orthographe exacte de tes objets.");
                }
            }
        }
        if (bPressed && code.gameObject.activeInHierarchy)
        {
            code.SetActive(false);
        }

        if (rayInteractor.hasHover)
        {

            var interactable = rayInteractor.interactablesHovered[0];
            GameObject hitObject = interactable.transform.gameObject;
            Collider hitCollider = hitObject.GetComponent<Collider>();
            // --- REMPLACEMENT POUR LA MAIN GAUCHE ---
            bool triggerLeftClicked = triggerLeft.action.WasReleasedThisFrame();

         
            // --- REMPLACEMENT POUR LA MAIN DROITE ---
            bool triggerRightClicked = triggerRight.action.WasReleasedThisFrame();
            bool gripRightClicked = selectRight.action.WasReleasedThisFrame();

           // --- MAIN DROITE ---
            // --- MAIN GAUCHE ---
            bool yPressed = buttonY.action.WasReleasedThisFrame(); // Anciennement lié à une partie de xyLeft

            // Devient TRUE tant que le bouton reste enfoncé, pas seulement à la première frame
            bool triggerLeftHolded = triggerLeft.action.IsPressed();

            if (hitCollider.name == "ResultColor")
            {
                if (triggerLeftClicked)
                {
                    colorsMenu.SetActive(true);
                    gameScript.stateScript.state = State.ColorMenu;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }

            // 1. SI ON EST EN TRAIN DE DRAGGER : On ignore le survol des autres objets, on reste verrouillé !
            if (triggerLeftHolded && m_IsDragging && m_LastHoveredRenderer != null)
            {

                Ray ray = new Ray(rayInteractor.transform.position, rayInteractor.transform.forward);
                float wallZ = m_LastHoveredRenderer.transform.position.z;
                Plane xyPlane = new Plane(Vector3.forward, new Vector3(0, 0, wallZ));

                if (xyPlane.Raycast(ray, out float enterDistance))
                {
                    Vector3 hitPointOnWall = ray.GetPoint(enterDistance);
                    Vector3 meshCenterOffset = m_LastHoveredRenderer.bounds.center - m_LastHoveredRenderer.transform.position;
                    Vector3 targetPosition = hitPointOnWall - meshCenterOffset;

                    m_LastHoveredRenderer.transform.position = new Vector3(targetPosition.x, targetPosition.y, wallZ);
                }
            }
            // 2. SI ON NE DRAGGE PAS : On gère la détection de survol classique (Hover) et l'initialisation du drag
            else
            {
                // Si on a relâché le bouton, on désactive le verrou de drag
                if (!triggerLeftHolded)
                {
                    m_IsDragging = false;
                }

                if (hitCollider != null && hitCollider.CompareTag("draggable"))
                {
                    if (hitCollider.TryGetComponent<Renderer>(out Renderer currentRenderer))
                    {
                        // HOVER EXIT (Si on passe directement d'un objet draggable à un autre)
                        if (m_LastHoveredRenderer != null && m_LastHoveredRenderer != currentRenderer && !m_IsDragging)
                        {
                            ResetLastHoverColor();
                        }

                        // HOVER ENTER (On survole un nouvel objet)
                        if (m_LastHoveredRenderer != currentRenderer && !m_IsDragging)
                        {
                            m_LastHoveredRenderer = currentRenderer;
                            m_OriginalColor = currentRenderer.material.color;

                            Color.RGBToHSV(m_OriginalColor, out float h, out float s, out float v);
                            v = Mathf.Clamp01(v + 0.3f);
                            Color lighterColor = Color.HSVToRGB(h, s, v);

                            currentRenderer.material.color = lighterColor;
                        }
                    }

                    // Si on vient d'appuyer sur le bouton alors qu'on survole cet objet, on active le verrou !
                    if (triggerLeftHolded && m_LastHoveredRenderer != null)
                    {
                        m_IsDragging = true;
                    }
                }
                else
                {
                    // Si le laser pointe dans le vide et qu'on ne dragge pas, on reset la couleur de survol
                    if (m_LastHoveredRenderer != null && !m_IsDragging)
                    {
                        ResetLastHoverColor();
                        m_LastHoveredRenderer = null; // On oublie l'objet puisqu'on ne le survole plus
                    }
                }
            }
            
            if (hitCollider.CompareTag("Collectible") && (gameScript.player.inventory.HasItem("Bag") || hitCollider.gameObject.name == "Bag" || true))
            {


                if(triggerLeftClicked) {
                    string itemName = hitCollider.gameObject.name;

                    if (validNames.Contains(hitCollider.name))
                    {

                       
                        VerserDansBecher(hitCollider.name);
                        var groupedContents = contenus
     .GroupBy(item => item)
     .Select(group => $"{group.Count()}{group.Key}");

                        statusText.text = string.Join(" + ", groupedContents);

                    }
                    if(itemName == "Ramen")
                    {
                        hasRamen = true;

                        Destroy(hitCollider.gameObject);
                    }
                    if (itemName == "Torch" || itemName == "trashbag" || itemName == "FlashLight" || itemName == "Becher")
                    {
                       
                        if(itemName == "trashbag")
                        {
                            Destroy(torch);
                            Destroy(colliderTori);
                        }
                       
                        hitCollider.transform.SetParent(pokeRightInteractor);
                       
                        // OPTIONNEL : Si tu veux que la torche se téléporte exactement sur le pokeInteractor
                        hitCollider.transform.localPosition = Vector3.zero;
                        if (itemName == "Becher")
                        {
                            hasBecher = true;
                            hitCollider.transform.Rotate(Vector3.left);
                        }
                        else if (itemName == "FlashLight")
                        {
                            hitCollider.transform.Rotate(Vector3.up);
                        }
                        else
                        {
                            hitCollider.transform.localRotation = Quaternion.identity;
                        }
                      
                        if(itemName == "FlashLight")
                        {

                            hitCollider.transform.localScale = new Vector3(.1f, .1f, .1f);
                        } else
                        {
                            hitCollider.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                        }
                        if (hitCollider.TryGetComponent<BoxCollider>(out BoxCollider boxCollider))
                        {
                            Destroy(boxCollider);
                        }
                        Debug.Log($"{hitCollider.name} est maintenant un enfant de pokeInteractor !");
                    }
                    if (itemName.Contains("Triangle"))
                    {
                        // 1. On sépare par les espaces. 
                        // Si itemName est "Triangle (1) (Small) (Red)", les index sont : [0]="Triangle", [1]="(1)"
                        string[] splitName = itemName.Split(' ');

                        if (splitName.Length > 1)
                        {
                            string targetNumberWithParentheses = splitName[0]; // Récupère "(N)"
                            Debug.Log("Recherche de la pièce avec l'identifiant : " + targetNumberWithParentheses);

                            // 2. On parcourt le tableau des pièces drag & droppables
                            for (int i = 0; i < tangramPieces.Length; i++)
                            {
                                if (tangramPieces[i] == null) continue;

                                // 3. Si le nom de la pièce du tableau contient le "(N)" (ex: "Triangle (1) ...")
                                if (tangramPieces[i].name.Contains(targetNumberWithParentheses))
                                {
                                    // 4. On active l'objet trouvé !
                                    tangramPieces[i].SetActive(true);
                                    Debug.Log($"Succès : {tangramPieces[i].name} a été activé !");
                                    break; // On a trouvé la bonne pièce, on arrête la recherche
                                }
                            }
                        }
                    }
                    // 1. Déclare la liste des noms valides (en haut de ta classe ou dans la méthode)

                    // 2. Ta condition devient :
                   
                    if (itemsList.items.ContainsKey(itemName)) {


                Item item = itemsList.items[itemName];
               /* inventoryScript.player.inventory.AddItem(item);
                        if(item.usable)
                        {
                            gameScript.ChangeCurrentItem(item);
                        }
               
                        if(item.itemName == "Clé")
                        {
                            Destroy(hitCollider.gameObject);
                            actionsScript.FinishTask("Trouver la clé");
                        }
                        if(item.itemName == "Ramen")
                        {
                            actionsScript.FinishTask("Trouver la clé");
                        }
                        if (item.itemName == "BookDaltonism")
                        {
                            actionsScript.FinishTask("Trouver le manuel");
                        }

                        if (item.itemName == "Bag")
                        {
                            actionsScript.FinishTask("Trouver votre sac");
                        }
                        if(item.itemName == "BookChimie")
                        {
                            actionsScript.FinishTask("Trouver le manuel de chimie");
                        }

                        if (item.itemName == "BookDaltonism")
                        {
                            actionsScript.FinishTask("Trouver le manuel de daltonisme");
                        }

                        if (hitCollider.gameObject.TryGetComponent<TangramPiece>(out var myComponent))
                        {
                            hitCollider.GetComponent<TangramPiece>().Collect();
                        }



                        hitCollider.gameObject.SetActive(false);*/
            }
                }
                crossHair.sprite = EKeyImage;
                crossHair.rectTransform.localScale = new Vector3(scaledSize, scaledSize, scaledSize);
              
               
            } else if(hitCollider.CompareTag("Interactable")) {
                if(triggerLeftClicked && hitCollider.name == "rideaux")
                {
                    StartCoroutine(OuvrirFermerRideaux());
                }

                if (triggerRightClicked && hitCollider.name.Contains("Triangle"))
                {
                    bool win = true;

                    // Define the correct piece names in order
                    string[] correctOrder = new string[]
                    {
        "(1) Triangle Green Small",
        "(2) Triangle Yellow Small",
        "(3) Triangle Red Medium",
        "(4) Triangle Purple Big",
        "(5) Triangle Blue Big",
        "(6) Triangle Red Small",
        "(7) Triangle Purple Small",
        "(8) Triangle Blue Small",
        "(9) Triangle Green Small"
                    };

                    for (int i = 0; i < puzzlePieces.transform.childCount; i++)
                    {
                        Transform slot = puzzlePieces.transform.GetChild(i);
                        if (slot.childCount == 0 || slot.GetChild(0).name != correctOrder[i])
                        {
                            win = false;
                            break;
                        }
                    }

                    if (win)
                    {
                        Debug.Log("Tangram puzzle solved!");
                        gameScript.hasFinishedPlayground = true;
                    }
                    else
                    {
                        Debug.Log("Tangram puzzle not solved yet.");
                    }
                }


                if (bPressed)
                {

                    string hitColName = hitCollider.gameObject.name;

                    if(hitColName.Contains("Triangle"))
                    {
                        Renderer renderer = hitCollider.GetComponent<Renderer>();
                  
                        gameScript.player.inventory.AddItem(itemsList.items[hitCollider.transform.GetChild(0).name]);
                        gameScript.ChangeCurrentItem(itemsList.items[hitCollider.transform.GetChild(0).name]);
                        hitCollider.transform.GetChild(0).name = "Holder";
                        renderer.material = blackMaterial;
                    }
                }
                if(triggerLeftClicked)
                {
                    string itName = gameScript.player.currentItem.itemName;
                    string hitColName = hitCollider.gameObject.name;
                    Renderer renderer = hitCollider.GetComponent<Renderer>();
                    if (hitCollider.name == "StoneLantern1")
                    {
                        stair1.SetActive(true);
                        CharacterController cc = player.GetComponent<CharacterController>();

                        cc.enabled = false;
                        player.transform.position = tp1.transform.position;
                        //player.transform.rotation = tp1.transform.rotation;
                        cc.enabled = true;

                        fire1.GetComponent<ParticleSystem>().Play();
                        obj1.GetComponent<BoxCollider>().enabled = false;
                        keyStrokeManager.step = 0;
                        StartCoroutine(ResetLantern1());
                    }

                    if (hitCollider.name == "StoneLantern2")
                    {
                        stair2.SetActive(true);
                        CharacterController cc = player.GetComponent<CharacterController>();

                        cc.enabled = false;
                        player.transform.position = tp2.transform.position;
                        player.transform.rotation = tp2.transform.rotation;
                        cc.enabled = true;
                        fire2.GetComponent<ParticleSystem>().Play();
                        obj2.GetComponent<BoxCollider>().enabled = false;
                        keyStrokeManager.step = 1;

                        StartCoroutine(ResetLantern2());
                    }

                    if (hitCollider.name == "StoneLantern3")
                    {
                        part1.SetActive(false); 
                        fire3.GetComponent<ParticleSystem>().Play();
                        CharacterController cc = player.GetComponent<CharacterController>();

                        cc.enabled = false;
                        player.transform.position = tp3.transform.position;
                        player.transform.rotation = tp3.transform.rotation;
                        cc.enabled = true;
                        keyStrokeManager.step = 2;
                        StartCoroutine(ResetLantern3());
                    }

                    if (hitCollider.name == "StoneLantern4")
                    {
                        obj4.SetActive(true);
                        fire4.GetComponent<ParticleSystem>().Play();

                        StartCoroutine(ResetLantern4());
                    }
                    if (hitCollider.name.Contains("Triangle") && hitCollider.transform.GetChild(0).name != "Holder") return;
                   
                    if (itName == "(1) Triangle Green Small")
                    {
                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();

                        renderer.material = greenMaterial;
                        
                    } else if (itName == "(2) Triangle Yellow Small")
                    {
                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();

                        renderer.material = yellowMaterial;
                        
                    }
                    else if (itName == "(3) Triangle Red Medium")
                    {
                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();
                        renderer.material = redMaterial;
                       
                    }
                    else if (itName == "(4) Triangle Purple Big")
                    {
                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();

                        renderer.material = violetMaterial;
                        
                    }
                    else if (itName == "(5) Triangle Blue Big")
                    {

                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();
                        renderer.material = blueMaterial;
                        
                    }
                    else if (itName == "(6) Triangle Red Small")
                    {
                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();

                        renderer.material = redMaterial;
                        
                    }
                    else if (itName == "(7) Triangle Purple Small")
                    {
                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();

                        renderer.material = violetMaterial;
                        
                    }
                    else if (itName == "(8) Triangle Blue Small")
                    {
                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();

                        renderer.material = blueMaterial;
                        
                    }
                    else if (itName == "(9) Triangle Green Small")
                    {
                        gameScript.player.inventory.RemoveItem(itName);
                        hitCollider.transform.GetChild(0).name = itName;
                        gameScript.ChangeCurrentItem(itemsList.items["Aucun"]);
                        gameScript.filter.SwitchMode();
                        renderer.material = greenMaterial;
                    }
                    
                }


               
                if(triggerRightClicked && hitCollider.name == "iron_gate")
                {
                    hitCollider.gameObject.transform.position += Vector3.right * 3;
                }
                if (triggerLeftClicked && hitCollider.name == "laptop" && gameScript.stateScript.state == State.Play)
                {
                    actionsScript.FinishTask("Trouver l'épreuve");
                    finalMenu.SetActive(true);
                  //  gameScript.stateScript.state = State.FinalTask;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    return;
                }

                if (yPressed && hitCollider.name == "laptop" && gameScript.stateScript.state == State.FinalTask)
                {
                    finalMenu.SetActive(false);
                    gameScript.stateScript.state = State.Play;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
                if(triggerLeftClicked && hitCollider.name == "PUERTA" && gameScript.stateScript.state == State.Play)
                {
                    hitCollider.transform.Translate(Vector3.left*1.5f);
                }

                if (triggerLeftClicked && hitCollider.name == "jap_man" && gameScript.stateScript.state == State.Play)
                {
                    actionsScript.FinishTask("Parler à Satoshi");
                    gameScript.stateScript.state = State.FinalDialog;
                    Cursor.lockState = CursorLockMode.Locked;

                    Cursor.visible = false;
                    return;
                }
                if (triggerLeftClicked && hitCollider.name == "BookDyslexie" && gameScript.stateScript.state == State.Play)
                {
                    actionsScript.FinishTask("Lire le manuel dyslexie");
                    gameScript.stateScript.state = State.DyslexieReading;
                    bookDyslexie.SetActive(true);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
                if (triggerLeftClicked && gameScript.stateScript.state == State.DyslexieReading)
                {
                    gameScript.stateScript.state = State.Play;
                    bookDyslexie.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }

                if (triggerLeftClicked && hitCollider.name == "BookDaltonisme" && gameScript.stateScript.state == State.Play)
                {
                    actionsScript.FinishTask("Lire le manuel daltonisme");
                    gameScript.stateScript.state = State.DaltonismReading;
                    bookDaltonism.SetActive(true);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
                if (triggerLeftClicked && gameScript.stateScript.state == State.DaltonismReading)
                {
                    gameScript.stateScript.state = State.Play;
                    bookDaltonism.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    return;
                }
               

                if(hitCollider.name == "Becher" && gameScript.isAllowedToAnswerChemistry && triggerRightClicked)
                {
                    gameScript.stateScript.state = State.StartBecherAlreadyDialog;
                    return;
                }

               

                if (triggerLeftClicked) {
                     if(hitCollider.name == "Becher")
                    {
                       // string playerCurrentItemName = gameScript.player.currentItem.itemName;
                        //if (!ingredients.Contains(playerCurrentItemName)) return;
                        VerserDansBecher(hitCollider.name);
                        var groupedContents = contenus
     .GroupBy(item => item)
     .Select(group => $"{group.Count()}{group.Key}");

                        statusText.text = string.Join(" + ", groupedContents);
                    }
                 
                     if(hitCollider.name.Contains("Door") && hitCollider.name != "Door_spec")
                    {
                        if((hitCollider.name == "Door_chem" && gameScript.hasFinishedChemistry) || (hitCollider.name != "Door_chem"))
                        {
                            hitCollider.gameObject.GetComponent<Door>().OpenDoor();
                        } 
                      

                    }
                    if(hitCollider.name == "Door_spec") {
                        if(hasRamen) {
                            actionsScript.FinishTask("Trouver la sortie");
                            hitCollider.gameObject.GetComponent<Door>().OpenDoor();
                            gameScript.player.inventory.RemoveItem(gameScript.player.currentItem.itemName);
                            //actionsScript.FinishTask("Ouvrir la porte");
                        }
                    }
                    if(hitCollider.name == "bus")
                    {
                        CharacterController cc = player.GetComponent<CharacterController>();

                        cc.enabled = false;
                        player.transform.position = teleport1.transform.position;
                        cc.enabled = true;
                    }

                    if (hitCollider.name == "bus (2)")
                    {
                        CharacterController cc = player.GetComponent<CharacterController>();

                        cc.enabled = false;
                        player.transform.position = teleport2.transform.position;
                        cc.enabled = true;
                    }
                    if (hitCollider.name == "Door2")
                    {
                        if (gameScript.player.currentItem.itemName == "Clé")
                        {
                            hitCollider.gameObject.GetComponent<Door>().OpenDoor();
                            gameScript.player.inventory.RemoveItem(gameScript.player.currentItem.itemName);
                        
                        }
                    }

                }
  crossHair.sprite = RKeyImage;
                crossHair.rectTransform.localScale = new Vector3(scaledSize, scaledSize, scaledSize);
              
            }
            else
            {
                crossHair.rectTransform.localScale = new Vector3(defaultSize, defaultSize, defaultSize);
                crossHair.sprite = defaultImage;
               
            }
        }
        else
        {
            crossHair.rectTransform.sizeDelta = new Vector3(defaultSize, defaultSize, defaultSize);
            crossHair.sprite = defaultImage;
        }
    }

    public bool CheckMix()
    {
        int aOHNCount = 0;
        int Cu504Count = 0;

        foreach (string itemName in contenus)
        {
            if (Cu504Count < 2 && itemName == "Cu504" && aOHNCount == 0)
            {
                Cu504Count++;
            }
            else if (Cu504Count == 2 && aOHNCount < 3 && itemName == "aOHN")
            {
                aOHNCount++;
            }
            else
            {
                return false;
            }
        }

        return Cu504Count == 2 && aOHNCount == 3;
    }
    IEnumerator OuvrirFermerRideaux()
    {
        enCours = true;

        // OUVERTURE
        rideaux.transform.Translate(Vector3.forward * 5);
        spotLight.enabled = false;

        // ATTENTE
        yield return new WaitForSeconds(3f);

        // RETOUR POSITION INITIALE
        rideaux.transform.Translate(Vector3.back * 5);
        spotLight.enabled = true;

        enCours = false;
    }
    IEnumerator ResetLantern1()
    {
        yield return new WaitForSeconds(5f);
        obj1.GetComponent<BoxCollider>().enabled = true;
        obj1.GetComponent<BoxCollider>().isTrigger = true;
        fire1.GetComponent<ParticleSystem>().Stop();
        stair1.SetActive(false);
    }

    IEnumerator ResetLantern2()
    {
        yield return new WaitForSeconds(5f);
        obj2.GetComponent<BoxCollider>().enabled = true;
        obj2.GetComponent<BoxCollider>().isTrigger = true;

        fire2.GetComponent<ParticleSystem>().Stop();
        stair2.SetActive(false);
    }

    IEnumerator ResetLantern3()
    {
        yield return new WaitForSeconds(5f);
        obj3.GetComponent<BoxCollider>().enabled = true;
        obj3.GetComponent<BoxCollider>().isTrigger = true;
        fire3.GetComponent<ParticleSystem>().Stop();
        part1.SetActive(true);
       
    }
    IEnumerator ResetLantern4()
    {
        yield return new WaitForSeconds(15f);
    
        fire4.GetComponent<ParticleSystem>().Stop();
        obj4.SetActive(false);

    }
}

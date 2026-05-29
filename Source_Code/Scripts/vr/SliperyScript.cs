using UnityEngine;
using UnityEngine.InputSystem; // Requis pour le nouvel Input System de Unity

[RequireComponent(typeof(CharacterController))]
public class SliperyScript : MonoBehaviour
{
    [Header("État du mode")]
    [Tooltip("Active ou désactive l'effet de glissade globale.")]
    public bool slippery = false;

    [Header("Vitesse de Base (Mode Normal)")]
    [Tooltip("Vitesse de déplacement quand le mode glissade est désactivé.")]
    public float baseSpeed = 4f; // <--- LA VARIABLE QUE TU CHERCHES

    [Header("Configuration VR & Inputs")]
    [Tooltip("Glisser dans la direction du regard. Glissez-y la Main Camera du XR Rig.")]
    public Transform vrCamera;
    [Tooltip("L'action Input System correspondant au joystick de déplacement (ex: LeftHand/Move).")]
    public InputActionProperty moveAction;

    [Header("Paramètres de Glisse")]
    public float slipStrength = 15f;    // Force accumulée par l'input du joystick
    public float slipDamping = 0.98f;   // Friction (0.95 = s'arrête vite, 0.99 = glisse indéfiniment)
    public float gravity = 20f;         // Gravité appliquée pendant la glisse

    private CharacterController controller;
    private Vector3 slipVelocity = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Si la caméra n'est pas assignée, on cherche la caméra principale
        if (vrCamera == null && Camera.main != null)
        {
            vrCamera = Camera.main.transform;
        }
    }

    void FixedUpdate()
    {
        // 1. Récupérer les inputs du joystick VR (Vector2)
        Vector2 inputVector = moveAction.action.ReadValue<Vector2>();
        float inputX = inputVector.x;
        float inputY = inputVector.y;

        // 2. Calculer la direction par rapport à l'orientation de la caméra VR (Axe Y mis à plat)
        Vector3 forward = vrCamera.forward;
        Vector3 right = vrCamera.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 inputDir = (forward * inputY) + (right * inputX);

        if (slippery)
        {
            // --- MODE GLISSADE ---
            // 3. Ajouter l'input à la vitesse de glisse (Physique accumulée)
            slipVelocity += inputDir * slipStrength * Time.deltaTime;

            // 4. Appliquer la gravité
            if (controller.isGrounded && slipVelocity.y < 0)
            {
                slipVelocity.y = -2f;
            }
            else
            {
                slipVelocity.y -= gravity * Time.deltaTime;
            }

            // 5. Déplacer le CharacterController
            controller.Move(slipVelocity * Time.deltaTime);

            // 6. Appliquer le damping (friction)
            slipVelocity.x *= slipDamping;
            slipVelocity.z *= slipDamping;
        }
        else
        {
            // --- MODE NORMAL (Ajouté pour toi) ---
            // Réinitialiser la vitesse de glisse interne
            if (slipVelocity.x != 0 || slipVelocity.z != 0)
            {
                slipVelocity.x = 0;
                slipVelocity.z = 0;
            }

            // Calcul du mouvement direct (sans accumulation/glissade)
            Vector3 normalMove = inputDir * baseSpeed;

            // Application de la gravité de base pour ne pas s'envoler
            if (controller.isGrounded)
            {
                slipVelocity.y = -2f;
            }
            else
            {
                slipVelocity.y -= gravity * Time.deltaTime;
            }
            normalMove.y = slipVelocity.y;

            // Déplacement classique
            controller.Move(normalMove * Time.deltaTime);
        }
    }
}
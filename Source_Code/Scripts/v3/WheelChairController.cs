using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))] // Sécurité : force la présence de l'AudioSource
public class WheelchairController : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    [Header("Réglages Physiques")]
    public float torqueMultiplier = 2.0f;  // Puissance de la poussée
    public float rollingFriction = 0.98f; // Ralentissement naturel (0.95 - 0.99)
    public float turnForce = 1.5f;        // Sensibilité du pivotement

    [Header("Réglages Audio Roulement")]
    [Tooltip("Vitesse à partir de laquelle le volume audio atteint son maximum.")]
    public float maxAudioSpeed = 5.0f;
    [Tooltip("Vitesse minimale en dessous de laquelle le son se coupe.")]
    public float minAudioThreshold = 0.05f;
    [Tooltip("Vitesse de transition pour le fondu du volume (smooth).")]
    public float audioFadeSpeed = 5.0f;

    private float lastLeftRot;
    private float lastRightRot;

    private Vector3 currentMoveVelocity;
    private float currentTurnVelocity;
    private CharacterController controller;
    public AudioSource audioSource;

    [Header("Configuration VR Inputs")]
    public InputActionReference selectLeft;     // Grip (Saisie) gauche
    public InputActionReference selectRight;    // Grip (Saisie) droit

    void Start()
    {
        controller = GetComponent<CharacterController>();
    

        // Configuration automatique de l'AudioSource pour un rendu propre
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0f;

        lastLeftRot = leftHand.localEulerAngles.x;
        lastRightRot = rightHand.localEulerAngles.x;
    }

    void Update()
    {
        // 1. On vérifie en continu si les grips sont maintenus enfoncés
        bool isLeftGripPressed = selectLeft != null && selectLeft.action.IsPressed();
        bool isRightGripPressed = selectRight != null && selectRight.action.IsPressed();

        float deltaLeft = 0f;
        float deltaRight = 0f;

        // 2. Main gauche : On ne calcule le delta que si le grip gauche est pressé
        if (isLeftGripPressed)
        {
            deltaLeft = GetRotationDelta(leftHand.localEulerAngles.x, ref lastLeftRot);
        }
        else
        {
            lastLeftRot = leftHand.localEulerAngles.x;
        }

        // 3. Main droite : On ne calcule le delta que si le grip droit est pressé
        if (isRightGripPressed)
        {
            deltaRight = GetRotationDelta(rightHand.localEulerAngles.x, ref lastRightRot);
        }
        else
        {
            lastRightRot = rightHand.localEulerAngles.x;
        }

        // --- Calculs physiques ---
        float forwardThrust = (deltaLeft + deltaRight) * torqueMultiplier;
        float rotationThrust = (deltaRight - deltaLeft) * turnForce;

        currentMoveVelocity += transform.forward * forwardThrust * Time.deltaTime;
        currentMoveVelocity *= rollingFriction;

        currentTurnVelocity += rotationThrust * Time.deltaTime;
        currentTurnVelocity *= rollingFriction;

        controller.SimpleMove(currentMoveVelocity);
        transform.Rotate(0, currentTurnVelocity, 0);

        // --- GESTION DU SON NATUREL ---
        HandleRollingAudio();
    }

    void HandleRollingAudio()
    {
        // On calcule la vitesse de déplacement réelle à plat (magnitude du CharacterController)
        // On y ajoute une fraction de la vitesse de rotation pour que tourner sur place fasse aussi un peu de bruit
        float currentSpeed = controller.velocity.magnitude + (Mathf.Abs(currentTurnVelocity) * 0.2f);

        if (currentSpeed > minAudioThreshold)
        {
            // Si le son ne joue pas encore, on le lance
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // Normalisation de la vitesse entre 0 et 1 pour adapter le volume
            float targetVolume = Mathf.Clamp01(currentSpeed / maxAudioSpeed);
            // Lerp pour éviter les sauts brusques de volume
            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * audioFadeSpeed);

            // Modification dynamique du Pitch (Tonalité) : de 0.85 (lent) à 1.25 (rapide)
            float targetPitch = Mathf.Lerp(0.85f, 1.25f, targetVolume);
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, Time.deltaTime * audioFadeSpeed);
        }
        else
        {
            // Si on avance plus, on baisse progressivement le son vers 0 avant de couper
            if (audioSource.isPlaying)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * audioFadeSpeed);

                if (audioSource.volume <= 0.01f)
                {
                    audioSource.Stop();
                }
            }
        }
    }

    float GetRotationDelta(float currentAngle, ref float lastAngle)
    {
        float delta = Mathf.DeltaAngle(lastAngle, currentAngle);
        lastAngle = currentAngle;

        return delta;
    }
}
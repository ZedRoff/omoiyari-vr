using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets; // Requis pour accéder à DynamicMoveProvider

public class FootSteps : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public float stepRate = 0.5f;   // Temps entre deux bruits de pas (ex: 0.5 secondes)

    [Header("Références VR")]
    public DynamicMoveProvider moveProvider;

    [Header("Gestion du Mode")]
    public StateScript stateScript;

    private float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Tente de trouver automatiquement le DynamicMoveProvider si non assigné dans l'inspecteur
        if (moveProvider == null)
        {
            moveProvider = Object.FindFirstObjectByType<DynamicMoveProvider>();
        }

        // Récupère ton gestionnaire d'état de jeu
        GameObject stateManager = GameObject.Find("State Manager");
        if (stateManager != null)
        {
            stateScript = stateManager.GetComponent<StateScript>();
        }
    }

    void Update()
    {
        // Sécurité : On vérifie que tout est bien assigné pour éviter les erreurs de compilation
        if (stateScript == null || moveProvider == null) return;

        // 1. On vérifie si le jeu est en mode 'Play'
        if (stateScript.state == State.Play)
        {
            // 2. On détecte si le joueur bouge en lisant la valeur des joysticks (gauche ou droite)
            Vector2 leftInput = moveProvider.leftHandMoveInput.ReadValue();
            Vector2 rightInput = moveProvider.rightHandMoveInput.ReadValue();

            // S'il y a de l'input sur l'un des deux joysticks, c'est que le joueur essaie de bouger
            bool isMoving = (leftInput.sqrMagnitude > 0.01f || rightInput.sqrMagnitude > 0.01f);

            if (isMoving)
            {
                // Gestion du rythme des pas
                timer += Time.deltaTime;
                if (timer >= stepRate)
                {
                    PlayFootSteps();
                    timer = 0f;
                }
            }
            else
            {
                // Si le joueur s'arrête, on réinitialise le timer pour que le pas se déclenche direct à la prochaine marche
                timer = stepRate;
            }
        }
    }

    public void PlayFootSteps()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            // Optionnel : Ajoute un petit pitch aléatoire pour que le bruit de pas ne soit pas trop répétitif
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.Play();
        }
    }
}
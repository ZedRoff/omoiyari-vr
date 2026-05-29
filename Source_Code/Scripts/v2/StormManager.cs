using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class StormManager : MonoBehaviour
{
    public static StormManager Instance;

    private float stormInterval = 7f;
    public float warningTime = 3f;

    public event Action OnStormWarning;
    public event Action OnStormHit;

    public AudioSource audio;
    public Light lightningLight;
    public Image jauge;

    public GameScript gameScript;
    public AudioSource audioHeartBeat;

    [Header("Configuration Rythme Cardiaque")]
    [Range(0f, 1f)] private float currentStress = 0f; // 0 = Calme, 1 = Panique totale
    public float stressBuildUpSpeed = 0.5f;           // Vitesse à laquelle le cœur s'emballe
    public float stressCalmDownSpeed = 0.15f;         // Vitesse à laquelle le cœur se calme (plus lent, plus réaliste)

    private UnityEngine.XR.InputDevice leftDevice;
    private UnityEngine.XR.InputDevice rightDevice;

    private Coroutine hapticCoroutine;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        gameScript = GameObject.Find("Game Manager").GetComponent<GameScript>();
        InvokeRepeating(nameof(StartStormCycle), stormInterval, stormInterval);

        // Configuration initiale du battement de coeur pour qu'il tourne en boucle infinie
        if (audioHeartBeat != null)
        {
            audioHeartBeat.loop = true;
            audioHeartBeat.volume = 0f;
            audioHeartBeat.pitch = 1f;
            audioHeartBeat.Play();
        }
    }

    void Update()
    {
        HandleHeartbeatStress();
    }

    // --- NOUVELLE LOGIQUE CONSTANTE DU BATTEMENT DE COEUR ---
    void HandleHeartbeatStress()
    {
        if (audioHeartBeat == null || gameScript == null) return;

        float targetStress = 0f;

        // 1. Détermination du niveau de stress précis selon la situation
        if (gameScript.startedStorm)
        {
            if (PlayerState.Instance.isInShelter)
            {
                // ÉTAT 3 : La tempête fait rage mais le joueur est à l'abri -> Le cœur se calme presque totalement
                targetStress = 0.15f;
            }
            else
            {
                // ÉTAT 2 : La tempête est là et le joueur est exposé -> Panique et accélération maximale
                targetStress = 1.0f;
            }
        }
        else
        {
            // ÉTAT 1 : Pas de tempête (au début ou après) -> Calme plat, battement de base lent
            targetStress = 0f;
        }

        // 2. Transition fluide pour simuler l'inertie du cœur humain
        if (currentStress < targetStress)
        {
            // Le cœur s'emballe rapidement en cas de danger
            currentStress = Mathf.MoveTowards(currentStress, targetStress, stressBuildUpSpeed * Time.deltaTime);
        }
        else
        {
            // Le cœur met du temps à retrouver son rythme normal (très réaliste quand on court s'abriter)
            currentStress = Mathf.MoveTowards(currentStress, targetStress, stressCalmDownSpeed * Time.deltaTime);
        }

        // 3. Application des effets sur l'audio
        // Au lieu de couper le son à 0, on laisse le cœur battre TOUJOURS (même au calme), mais très discrètement
        if (!audioHeartBeat.isPlaying) audioHeartBeat.Play();

        // Volume : va de 0.15 (très discret au calme) à 1.0 (fond les ballons dans les oreilles)
        audioHeartBeat.volume = Mathf.Lerp(0.15f, 1.0f, currentStress);

        // Vitesse (Pitch) : va de 0.85 (battement très lent, relax) à 1.8 (tachycardie/panique)
        // Ajuste 0.85f selon le rythme de base de ton fichier audio pour que ce soit bien lent au départ
        audioHeartBeat.pitch = Mathf.Lerp(0.85f, 1.8f, currentStress);
    }

    void AssignXRDevices()
    {
        var leftDevices = new System.Collections.Generic.List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller, leftDevices);
        if (leftDevices.Count > 0) leftDevice = leftDevices[0];

        var rightDevices = new System.Collections.Generic.List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, rightDevices);
        if (rightDevices.Count > 0) rightDevice = rightDevices[0];
    }

    void StartStormCycle()
    {
        if (gameScript == null || !gameScript.startedStorm)
        {
            StopStormEntirely();
            return;
        }

        Debug.Log("[STORM] Alerte tempête lancée !");
        if (!PlayerState.Instance.isInShelter) audio.Play();
        OnStormWarning?.Invoke();

        Invoke(nameof(StormHit), warningTime);
    }

    void StormHit()
    {
        if (!gameScript.startedStorm) return;

        Debug.LogWarning("[STORM] L'éclair frappe !");
        OnStormHit?.Invoke();
        StartCoroutine(LightningFlash());

        if (!PlayerState.Instance.isInShelter)
        {
            if (hapticCoroutine != null) StopCoroutine(hapticCoroutine);
            AssignXRDevices();
            hapticCoroutine = StartCoroutine(DisorientationHaptics());
        }
    }

    public void StopStormEntirely()
    {
        CancelInvoke(nameof(StormHit));
        if (hapticCoroutine != null)
        {
            StopCoroutine(hapticCoroutine);
            hapticCoroutine = null;
        }
        lightningLight.enabled = false;
    }

    IEnumerator LightningFlash()
    {
        int flashes = UnityEngine.Random.Range(3, 4);
        for (int i = 0; i < flashes; i++)
        {
            if (!gameScript.startedStorm) break;
            lightningLight.enabled = true;
            lightningLight.intensity = UnityEngine.Random.Range(8f, 15f);
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.15f));
            lightningLight.enabled = false;
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.2f));
        }
    }

    IEnumerator DisorientationHaptics()
    {
        float duration = 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (!gameScript.startedStorm) yield break;

            float leftIntensity = UnityEngine.Random.Range(0.4f, 0.9f);
            float rightIntensity = UnityEngine.Random.Range(0.4f, 0.9f);
            float pulseDuration = UnityEngine.Random.Range(0.05f, 0.1f);

            if (leftDevice.isValid && leftDevice.TryGetHapticCapabilities(out var leftCaps) && leftCaps.supportsImpulse)
            {
                leftDevice.SendHapticImpulse(0, leftIntensity, pulseDuration);
            }

            if (rightDevice.isValid && rightDevice.TryGetHapticCapabilities(out var rightCaps) && rightCaps.supportsImpulse)
            {
                rightDevice.SendHapticImpulse(0, rightIntensity, pulseDuration);
            }

            float waitTime = UnityEngine.Random.Range(0.08f, 0.15f);
            yield return new WaitForSeconds(waitTime);
            elapsed += waitTime;
        }

        if (gameScript.startedStorm)
        {
            if (leftDevice.isValid) leftDevice.SendHapticImpulse(0, 0.3f, 0.1f);
            if (rightDevice.isValid) rightDevice.SendHapticImpulse(0, 0.3f, 0.1f);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public bool isInShelter;
    public float overloadLevel = 0f;
    public float maxOverload = 100f;

    public Image jauge;
    private float maxSize = 470f; // largeur max de la jauge
    private GameObject player;
    public GameObject tpStartStorm;

    void Awake()
    {
        Instance = this;
        tpStartStorm = GameObject.Find("tpStartStorm");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Start()
    {
        StormManager.Instance.OnStormHit += HandleStorm;

        // initialisation de la jauge
        UpdateJauge();
    }

    void HandleStorm()
    {
        if (isInShelter) return;

        overloadLevel += 25f;
        overloadLevel = Mathf.Clamp(overloadLevel, 0, maxOverload);

        UpdateJauge();
        ApplyOverloadEffects();
    }

    public void EnterShelter()
    {
        isInShelter = true;
        overloadLevel = Mathf.Max(overloadLevel - 40f, 0);

        UpdateJauge();
    }

    public void ExitShelter()
    {
        isInShelter = false;
    }

    void UpdateJauge()
    {
        // map overloadLevel (0-100) sur la largeur max
        float width = (overloadLevel / maxOverload) * maxSize;

        RectTransform rt = jauge.rectTransform;
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);

        UpdateJaugeColor();
    }

    void UpdateJaugeColor()
    {
        if (overloadLevel >= 80)
        {
            CharacterController cc = player.GetComponent<CharacterController>();

            cc.enabled = false;
            player.transform.position = tpStartStorm.transform.position;
            cc.enabled = true;
            overloadLevel = 0;
            UpdateJauge();
            jauge.color = Color.red;       // OVERLOAD MAX
        }
        else if (overloadLevel >= 50)
            jauge.color = new Color(1f, 0.5f, 0f); // orange
        else if (overloadLevel >= 25)
            jauge.color = Color.yellow;    // désorientation légère
        else
            jauge.color = Color.green;     // sain
    }

    void ApplyOverloadEffects()
    {
        if (overloadLevel >= 80)
            Debug.Log("OVERLOAD MAX");
        else if (overloadLevel >= 50)
            Debug.Log("Contrôles perturbés");
        else if (overloadLevel >= 25)
            Debug.Log("Désorientation légère");
    }
}

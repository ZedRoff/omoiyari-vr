using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public GameObject mv;
    public StateScript stateScript;
    public ActionsScript actionsScript;
    public GameScript gameScript;
    public GameObject cloudMngr;
    public GameObject map;




    public GameObject tp1;
    public GameObject tp2;
    public GameObject tp3;
    public GameObject tp4;

    public GameObject player;

    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    public GameObject obj4;

    public KeyStrokeManager keyStrokeManager;

    public Light dirLight;
    public StormManager stormManager;

    public GameObject mainSun;

    public GameObject jauge;

    public ParticleSystem rainParticles;
    public AudioSource rainAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

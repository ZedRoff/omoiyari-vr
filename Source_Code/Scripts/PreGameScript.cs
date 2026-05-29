using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreGameScript : MonoBehaviour
{
    public GameObject pegiLogo;
    public GameObject gameTitle;
    public GameObject gameJapTitle;
    public float waitDuration = 2.0f;
    public string nextScene = "MainMenu";
    void Start()
    {
        StartCoroutine(PlayIntro());
        
    }
    IEnumerator PlayIntro() 
    {
        gameJapTitle.SetActive(false);
        gameTitle.SetActive(false);
        pegiLogo.SetActive(false);
        yield return StartCoroutine(FadeIn(pegiLogo));
        yield return StartCoroutine(FadeOut(pegiLogo));

        yield return StartCoroutine(FadeIn(gameTitle));
        yield return StartCoroutine(FadeIn(gameJapTitle));

        // Load next scene
        SceneManager.LoadScene(nextScene);

    }
    IEnumerator FadeIn(GameObject obj) 
    {
        // On active l'objet
        obj.SetActive(true);
        // On veut pouvoir changer sa couleur
        Graphic graphic = obj.GetComponent<Graphic>();
        // On prend sa couleur à travers le component Graphic
        Color color = graphic.color;
        // On met "alpha" à 0, ce qui rend l'objet opaque
        color.a = 0.0f;
        // Alpha étant changé, on set la nouvelle couleur
        graphic.color = color;
        // Compteur du temps
        float time = 0.0f;
        while (time < waitDuration)
         {
            // On change l'alpha
            color.a = time / waitDuration;
            // On set la couleur
            graphic.color = color;
            // On incrémente le temps
            time += Time.deltaTime;
            yield return null;
         }
         // Tout s'est bien passé, on met le alpha à 1 (transparent)
         color.a = 1.0f;
         graphic.color = color;
    }
    IEnumerator FadeOut(GameObject obj)
    {
        Graphic graphic = obj.GetComponent<Graphic>();
        Color color = graphic.color;

        float time = 0.0f;
        while (time < waitDuration)
        {
            color.a = 1.0f - (time / waitDuration);
            graphic.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = 0f;
        graphic.color = color;
        obj.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

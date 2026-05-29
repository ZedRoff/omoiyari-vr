using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Sprite keySprite;
    public Sprite becherSprite;
    public Sprite defaultSprite;
    public Sprite bookChimieSprite;
    public Sprite bookDaltonismSprite;
    public Dictionary<string, Item> items;
    public Sprite purpleSprite;
    public Sprite blueSprite;
    public Sprite redSprite;
    public Sprite yellowSprite;
    public Sprite greenSprite;
    public Sprite ramenSprite;
    void Start()
    {
       
        items = new Dictionary<string, Item>();
        items.Add("Clé", new Item("Clé", "Une simple clé", keySprite, true)); // fait

        items.Add("Cu504", new Item("Cu504", "Un composé chimique ionique", becherSprite, true)); // fait
        items.Add("NAH5O3", new Item("NAH5O3", "Un sel issu de la neutralisation partielle de l'acide sulfurique", becherSprite, true)); // fait (bad)
        items.Add("NAHGO3", new Item("NAHGO3", "Un composé inorganique", becherSprite, true));
        items.Add("aOHN", new Item("aOHN", "Une solution transparente encore plus corrosive qu'à l'état pur en raison de son aspect mouillant, qui augmente l'action et le contact avec la peau.", becherSprite, true));
        items.Add("Cu2O", new Item("Cu2O", "Un composé de l'oxygène et du cuivre.", becherSprite, true)); // Cu2O

        items.Add("BookChimie", new Item("BookChimie", "Un livre de chimie", bookChimieSprite, true));
        items.Add("BookDaltonism", new Item("BookDaltonism", "Un livre qui donne les correspondances sur les couleurs", bookDaltonismSprite, true));
        items.Add("Bag", new Item("Bag", "Votre inventaire", defaultSprite, false));


        items.Add("(1) Triangle Green Small", new Item("(1) Triangle Green Small", "Triangle vert petit", greenSprite, true));
        items.Add("(2) Triangle Yellow Small", new Item("(2) Triangle Yellow Small", "Triangle jaune petit", yellowSprite, true));
        items.Add("(3) Triangle Red Medium", new Item("(3) Triangle Red Medium", "Triangle rouge moyen", redSprite, true));
        items.Add("(4) Triangle Purple Big", new Item("(4) Triangle Purple Big", "Triangle Violet grand", redSprite, true));
        items.Add("(5) Triangle Blue Big", new Item("(5) Triangle Blue Big", "Triangle bleu grand", blueSprite, true));
        items.Add("(6) Triangle Red Small", new Item("(6) Triangle Red Small", "Triangle rouge petit", redSprite, true));
        items.Add("(7) Triangle Purple Small", new Item("(7) Triangle Purple Small", "Triangle violet petit", purpleSprite, true));
        items.Add("(8) Triangle Blue Small", new Item("(8) Triangle Blue Small", "Triangle bleu petit", blueSprite, true));
        items.Add("(9) Triangle Green Small", new Item("(9) Triangle Green Small", "Triangle vert petit", greenSprite, true));

        items.Add("Ramen", new Item("Ramen", "Plat à base de ramen", ramenSprite, true));


        items.Add("Aucun", new Item("Aucun", "aucun item", defaultSprite, false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

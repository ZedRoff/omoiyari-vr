using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using FPSBasics;
using System.Linq;
public class StateScript : MonoBehaviour
{
    public State state {get; set;}
    public TextScript textScript;
    public List<int> digits;
    public GameObject digitsUI;
    public TextMeshProUGUI[] digitsUIArray;
    public GameObject quiz;
    public GameObject endGameMenu;

    public GameObject player;
    CharacterController characterController;
    FPSWalkerEnhanced walker;
    FootSteps footSteps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.Play;
         textScript = GameObject.Find("Text Manager").GetComponent<TextScript>();
        digits = new List<int>();
        digitsUIArray = new TextMeshProUGUI[digitsUI.transform.childCount];
        for (int i = 0; i < digitsUI.transform.childCount; i++)
        {
            digitsUIArray[i] = digitsUI.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
        }
        quiz.SetActive(false);
        endGameMenu.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");

        characterController = player.GetComponent<CharacterController>();
        walker = player.GetComponent<FPSWalkerEnhanced>();
        footSteps = player.GetComponent<FootSteps>();

    }

    public void FillDigits()
    {
        int count = Mathf.Min(digits.Count, digitsUIArray.Length);

        for (int i = 0; i < count; i++)
        {
            digitsUIArray[i].text = digits[i].ToString();
        }
        for (int i = count; i < digitsUIArray.Length; i++)
        {
            digitsUIArray[i].text = "";
        }


        if (digits.Count == 4)
        {
            if (digits[0] == 3 && digits[1] == 1 && digits[2] == 2 && digits[3] == 4)
            {
                state = State.End;
            }
        }
    }
    // Update is called once per frame

    void Update()
    {
        SetPlayerMovement(!IsDialogState(state));
        switch (state)
            


        {/*    */
            case State.AutismStart:
                string[][] a = new string[][]{
                new string[] { "Satoshi", "Ici, tu vas expérimenter certainement la pire sensation sur Terre"},
                new string[] { "Satoshi", "Vous faites beaucoup trop de bruit"},
                  new string[] { "Satoshi", "Vous vous moquez de notre casque, vous l'appelez 'casque de chantier'" },
                  new string[] { "Satoshi", "Tu vas sentir aujourd'hui ce que ça fait d'être mal à l'aise" }


            };
                textScript.StartDialog(a);
                break;
            case State.AutismEnd:
                string[][] b = new string[][]{
                new string[] { "Satoshi", "Tu vois comment cet orage t'as perturbé"},
                new string[] { "Satoshi", "Imagine toi maintenant dans une folle moqueuse"},
                new string[] { "Satoshi", "Comprends-tu maintenant..."},
                new string[] { "Satoshi", "Ce que cela fait ?"},

            };
                textScript.StartDialog(b);
                break;
            case State.StartDialog:
                string[][] startDialog = new string[][]{
                new string[] { "Satoshi", "Ah, te voilà enfin réveillé... Sagaeru. Ou devrais-je dire, 'volontaire numéro un' ?"},
                new string[] { "Satoshi", "Tu ne reconnais pas l'endroit ? Normal. C'est ton propre esprit qui l'a créé. Enfin... modifié. Par moi. Je m'appelle Ford Benson. Tu ne me connais pas, mais moi, je connais très bien ton équipe. Et leur petit projet révolutionnaire."},
                new string[] { "Satoshi", "Trop 'handicapé' pour coder, trop 'lent' pour innover... C'est ce qu'ils ont dit, en tout cas. Alors j'ai décidé de leur donner une leçon. Et toi, tu es mon... cobaye volontaire." },
                new string[] { "Satoshi", "Ton seul moyen de sortir ? Me retrouver. Mais pour ça, il va falloir passer par mes épreuves. Des épreuves qui te montreront ce que c'est, vivre avec un handicap. Et si tu réussis, peut-être que je te libérerai." },

                    new string[] { "Satoshi", "Tâche de ne pas tomber..." },
                    new string[] { "Satoshi", "Alors, prêt à jouer ?" }


            };
                textScript.StartDialog(startDialog);
            break;
            case State.StartBecherAlreadyDialog:
                string[][] texts2 = new string[][]{
                     new string[] { "Prof.", "Note le résultat au fond de la salle"},

                };
                textScript.StartDialog(texts2);
                break;


            case State.CantEscape:
                string[][] cantEscapeDialog = new string[][]{
                     new string[] { "Satoshi", "Ici, tu vas vivre ce que j'ai vécu..."},

                };
                textScript.StartDialog(cantEscapeDialog);
                break;
            case State.NotAllowedToAnswer:
                string[][] texts3 = new string[][]{
                     new string[] { "Prof.", "Fait d'abord l'expérience sur le becher"},

                };
                textScript.StartDialog(texts3);
                break;
            case State.StartChemistryDialog:
                string[][] entryDyslexie = new string[][]
           {
                 new string[] {"Satoshi", "Lire, pour certains, c’est comme respirer." },
                 new string[] {"Satoshi", "Pour d'autres, c’est un combat." },
                 new string[] {"Satoshi", "Chaque lettre danse, chaque mot se brouille." },
                 new string[] {"Satoshi", "Tu vas ressentir cette confusion, cet effort permanent. " },
                 new string[] {"Satoshi", "Ce n’est pas une question d’attention. C’est une réalité invisible pour la plupart." }
           };
                textScript.StartDialog(entryDyslexie);
                break;

            case State.StartPlayGroundDialog:
                string[][] entryDaltonism = new string[][]
            {
                 new string[] {"Satoshi", "Ce que tu vas vivre ici, ce n’est pas une simple illusion." },
                 new string[] {"Satoshi", "C’est ce que des millions de personnes voient chaque jour, sans que personne ne le sache." },
                 new string[] {"Satoshi", "Regarde autour de toi… Les couleurs ne sont plus les mêmes, n’est-ce pas ?" },
                 new string[] {"Satoshi", "Et pourtant, tout est toujours là. " },
                 new string[] {"Satoshi", "Tu vas devoir observer, distinguer, reconstituer... Mais cette fois, sans l’aide de la couleur." },
                 new string[] {"Satoshi", "Bonne chance." }
            };
                textScript.StartDialog(entryDaltonism);
                break;
            case State.FinalTask:
                string[][] laptopDialog = new string[][]{
                new string[] { "Satoshi", "Tu as croisé des chiffres. Des faits. Des vérités."},
                new string[] { "Satoshi", "Le code est là, dans tout ce que tu as appris." },
                new string[] { "Satoshi", "Si rien ne se passe ... "},
                new string[] { "Satoshi", "Tu as peut-être oublié de lire quelque chose…" }
            };
                textScript.StartDialog(laptopDialog);
                if (Input.anyKeyDown)
                {
                   
                    bool isNumber = false;
                    if(Input.GetKeyDown(KeyCode.Backspace))
                    {
                        if(digits.Count > 0)
                        {
                            digits.RemoveAt(digits.Count - 1);
                            FillDigits();
                        }
                        return;
                    }
                    int numberPressed = 0;
                
                    foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
                    {
                        Debug.Log("keydown");
                        if (Input.GetKeyDown(keyCode))
                        {
                        
                            if (keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9)
                            {
                                numberPressed = keyCode - KeyCode.Alpha0;
                                isNumber = true;
                            }

                            else if (keyCode >= KeyCode.Keypad0 && keyCode <= KeyCode.Keypad9)
                            {
                                numberPressed = keyCode - KeyCode.Keypad0;
                                isNumber = true;
                            }
                            

                            break; 
                        }
                    }
                    if(isNumber)
                    {
                       if(digits.Count < 4)
                        {
                            digits.Add(numberPressed);
                            FillDigits();
                        }
                       
                    }


                   
                }

                break;
            case State.QuizRoomDialog:
                string[][] finDialog = new string[][]{
                new string[] { "Satoshi", "Te voilà arrivé ici."},
                new string[] { "Satoshi", "Tu as traversé mes épreuves, vu ce que beaucoup vivent chaque jour sans qu’on leur tende la main. " },
                new string[] { "Satoshi", "Prends le temps d’observer cette pièce."},
                new string[] { "Satoshi", "Chaque objet raconte une histoire. Une réalité." },
                new string[] { "Satoshi", "Regarde-les. Comprends-les. Ce n’est pas qu’un jeu." }
            };
                textScript.StartDialog(finDialog);
                break;
            case State.FinalDialog:
                string[][] quizzDialog = new string[][]{
                new string[] { "Satoshi", "Alors, qu’en as-tu retenu ? Est-ce que tu vois les choses autrement maintenant ?"},
                new string[] { "Satoshi", "Tu n’as plus qu’un pas à faire… mais il te reste un dernier test." },
                new string[] { "Satoshi", "Prends le temps d’observer cette pièce."},
                new string[] { "Satoshi", "Chaque objet raconte une histoire. Une réalité." },
                new string[] { "Satoshi", "Regarde-les. Comprends-les. Ce n’est pas qu’un jeu." }
            };
                textScript.StartDialog(quizzDialog);
                break;
            case State.Quiz:
                quiz.SetActive(true);
                
                if (Input.GetKeyDown(KeyCode.T))
                {
                    state = State.Play;
                    quiz.SetActive(false);
                }
                break;
            case State.End:
                endGameMenu.SetActive(true);
                break;
           
        }
    }
    void SetPlayerMovement(bool canMove)
    {
        if (characterController != null)
            characterController.enabled = canMove;

        if (walker != null)
            walker.enabled = canMove;

    }
    bool IsDialogState(State s)
    {
        switch (s)
        {
            case State.StartDialog:
            case State.AutismStart:
            case State.AutismEnd:
            case State.StartChemistryDialog:
            case State.StartPlayGroundDialog:
            case State.QuizRoomDialog:
            case State.FinalDialog:
            case State.CantEscape:
            case State.NotAllowedToAnswer:
            case State.StartBecherAlreadyDialog:
            case State.FinalTask:
                return true;

            default:
                return false;
        }
    }

}

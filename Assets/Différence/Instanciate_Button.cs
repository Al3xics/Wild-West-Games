using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Instanciate_Button : MonoBehaviour
{
    [SerializeField] private List<string> ListColorNames;
    [SerializeField] private List<string> ListColorNamesFrench;
    [SerializeField] private List<Color> ListColors;
    [SerializeField] private List<Color> colorsWithoutColorToFind;
    [SerializeField] private GameObject panel;


    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject button_obj;
    [SerializeField] private TextMeshProUGUI Consigne;

    [SerializeField] private int numberOfObjects;
    Vector3 position;

    float rand_Consigne;
    int rand_Color_To_Find;
    string Color_To_Find;
    bool Color_Find_Is_Given = false;
    bool Need_To_Find_Color_Button = false;

    private bool iswinner_Button;

    public bool Iswinner_Button
    {
        get { return iswinner_Button; }
    }

    [SerializeField] private Button_Function_Difference BFD;

    int randColorButton;
    int randColorText;

    float x = 0;
    float y = 0;
    void Awake()
    {
        ListColorNames = new List<string>()
        {
            "Red",
            "Green",
            "Blue",
            "Yellow",
            "Orange",
            "Purple",
            "Cyan",
            "Magenta",
            "Brown",
            "Black"
        };
        ListColorNamesFrench = new List<string>()
        {
            "Rouge",
            "Vert",
            "Bleu",
            "Jaune",
            "Orange",
            "Violet",
            "Cyan",
            "Magenta",
            "Marron",
            "Noir"
        };
        ListColors = new List<Color>()
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            new Color(1.0f, 0.5f, 0.0f), // Orange
            new Color(0.5f, 0.0f, 0.5f), // Purple
            Color.cyan,
            new Color(1.0f, 0.0f, 1.0f), // Magenta
            new Color(0.6f, 0.3f, 0.0f), // Brown
            Color.black
        };
        List<Color> colorsWithoutColorToFind = new List<Color>();
        Color_Find_Is_Given = false;
        Need_To_Find_Color_Button = false;
        iswinner_Button = false;
    }

    void Update()
    {
        
    }

    public void Make_Game(int nbrButton)
    {
        rand_Color_To_Find = UnityEngine.Random.Range(0, ListColors.Count - 1);
        Color_To_Find = ListColorNames[rand_Color_To_Find];
        string Color_To_Find_French_Name = ListColorNamesFrench[rand_Color_To_Find];


        rand_Consigne = UnityEngine.Random.Range(0f, 1f);
        if (rand_Consigne <= 0.5f) {
            Need_To_Find_Color_Button = true;
            Consigne.SetText("Cliquer sur le button de couleur " + Color_To_Find_French_Name);
        } else {
            Need_To_Find_Color_Button = false;
            Consigne.SetText("Cliquer sur le texte de couleur " + Color_To_Find_French_Name);
        }

        for (int i =  0; i < ListColors.Count; i++)
        {
            if (i != rand_Color_To_Find)
            {
                colorsWithoutColorToFind.Add(ListColors[i]);
            }
        }

        for (int i = 0; i < nbrButton; i++)
        {
            button_obj = Instantiate(prefab);
            BFD = button_obj.GetComponent<Button_Function_Difference>();
            button_obj.transform.SetParent(parent.transform, false);

            if (Color_Find_Is_Given == false)
            {
                float rand = UnityEngine.Random.Range(0f, 1f);
                if (rand <= 0.2f || i + 1 >= nbrButton)
                {
                    int index = ListColorNames.IndexOf(Color_To_Find);
                    BFD.IsWinning_Button = true;
                    if (Need_To_Find_Color_Button)
                    {
                        button_obj.GetComponent<Image>().color = ListColors[index];
                        randColorText = UnityEngine.Random.Range(0, colorsWithoutColorToFind.Count);
                        button_obj.GetComponentInChildren<TextMeshProUGUI>().color = colorsWithoutColorToFind[randColorText];
                        button_obj.GetComponentInChildren<TextMeshProUGUI>().text = ListColorNamesFrench[randColorText];
                    }
                    else
                    {
                        button_obj.GetComponentInChildren<TextMeshProUGUI>().color = ListColors[index];
                        button_obj.GetComponentInChildren<TextMeshProUGUI>().text = ListColorNamesFrench[randColorText];
                        randColorButton = UnityEngine.Random.Range(0, colorsWithoutColorToFind.Count);
                        button_obj.GetComponent<Image>().color = colorsWithoutColorToFind[randColorButton];
                    }
                    Color_Find_Is_Given = true;
                }
                else
                {
                    BFD.IsWinning_Button = false;
                    RandomColorTextEtButton();

                    button_obj.GetComponent<Image>().color = colorsWithoutColorToFind[randColorButton];
                    button_obj.GetComponentInChildren<TextMeshProUGUI>().color = colorsWithoutColorToFind[randColorText];
                    button_obj.GetComponentInChildren<TextMeshProUGUI>().text = ListColorNamesFrench[randColorText];
                }
            }
            else
            {
                BFD.IsWinning_Button = false;
                RandomColorTextEtButton();

                button_obj.GetComponent<Image>().color = colorsWithoutColorToFind[randColorButton];
                button_obj.GetComponentInChildren<TextMeshProUGUI>().color = colorsWithoutColorToFind[randColorText];
                button_obj.GetComponentInChildren<TextMeshProUGUI>().text = ListColorNamesFrench[randColorText];
            }
        }
        
    }

     private void RandomColorTextEtButton()
     {
        randColorButton = UnityEngine.Random.Range(0, colorsWithoutColorToFind.Count);
        randColorText = UnityEngine.Random.Range(0, colorsWithoutColorToFind.Count);

        if (randColorButton == randColorText) 
        {
            RandomColorTextEtButton();
        }
     }
}

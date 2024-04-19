using Nova;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Instanciate_Button : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject button_obj;
    [SerializeField] private TextMeshPro Consigne;
    [SerializeField] private List<string> ListColorName;
    [SerializeField] private List<Color> ListColor;

    [SerializeField] private int numberOfObjects;
    Vector3 position;

    float rand_Consigne;
    int rand_Color_To_Find;
    string Color_To_FInd;
    bool Color_Find_Is_Given = false;
    bool Need_To_Find_Color_Button = false;

    float x = 0;
    float y = 0;
    void Start()
    {
        Color_Find_Is_Given = false;
        Need_To_Find_Color_Button = false;
    }

    void Update()
    {
        
    }

    public void Make_Game(int nbrButton)
    {
        rand_Color_To_Find = Random.Range(0, ListColor.Count);
        Color_To_FInd = ListColorName[rand_Color_To_Find];

        rand_Consigne = Random.Range(0f, 1f);
        if (rand_Consigne <= 0.5f) {
            Need_To_Find_Color_Button = true;
            Consigne.SetText("Cliquer sur le button de couleur " + Color_To_FInd);
        } else {
            Need_To_Find_Color_Button = false;
            Consigne.SetText("Cliquer sur le texte de couleur " + Color_To_FInd);
        }
/*        int rows = Mathf.CeilToInt(Mathf.Sqrt(nbrButton));
        int cols = Mathf.CeilToInt((float)nbrButton / rows);

        float spacingX = 3f;
        float spacingY = -1.5f;

        Vector3 initialPosition = new Vector3(-3f, 1.5f, 0f);*/

        for (int i = 0; i < nbrButton; i++)
        {
                    if (i == 0 || i == 3 || i == 6)
                        x = -3;
                    else if (i == 1 || i == 4 || i == 7 || i == 9)
                        x = 0;
                    else
                        x = 3;

                    if (i == 9)
                        y = -3;
                    else if (i <= 2)
                        y = 0f;
                    else if (i > 2 && i <= 5)
                        y = -1.5f;
                    else
                        y = 1.5f;
           /* if (i == 10)
            {
                position = new Vector3(0, -3);
            }
            else
            {
                int row = i / cols;
                int col = i % cols;
                position = initialPosition + new Vector3(col * spacingX, row * spacingY, 0f);
            }*/
            position = new Vector2(x, y);
            button_obj = Instantiate(prefab, position, Quaternion.identity);
            button_obj.transform.parent = parent.transform;
            button_obj.GetComponent<UIBlock2D>().Position = position;
            
            if (Color_Find_Is_Given == false)
            {
                float rand = Random.Range(0f, 1f);
                if (rand <= 0.2f || i + 1 >= nbrButton)
                {
/*                    if (Need_To_Find_Color_Button)
                    {
                        int index = ListColor.IndexOf(Color_To_FInd);
                        button_obj.GetComponent<UIBlock2D>().Border.Color = ListColor[Color_To_FInd];
                    }
                    else
                    {
                        button_obj.GetComponent<UIBlock2D>().Border.Color = ListColor[Color_To_FInd];
                    }*/
                }
            }
        }
        
    }

/*    public void Instanciate_Button_Less_4()
    {
        Consigne.SetText("");
    }
    public void Instanciate_Button_Less_other()
    {
        Consigne.SetText("");
    }*/
}

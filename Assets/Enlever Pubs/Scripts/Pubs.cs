using Nova;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pubs : MonoBehaviour
{
    public void DestroyPubs()
    {
        Destroy(gameObject);
    }

    // Je pourrait ajouter qu'on puisse déplacer l'objet (dans le cas on le bouton n'est pas accessible)
}

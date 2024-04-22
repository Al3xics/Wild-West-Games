using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWater : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Récupérer les coordonnées des limites de la caméra en utilisant ViewportToWorldPoint
        Vector3 minBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 maxBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Vérifier si un objet sort des limites de la caméra
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.transform.position.x < minBound.x || obj.transform.position.x > maxBound.x ||
                obj.transform.position.y < minBound.y || obj.transform.position.y > maxBound.y)
            {
                // Détruire l'objet
                Destroy(obj);
            }
        }
    }
}

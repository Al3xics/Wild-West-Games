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
        // R�cup�rer les coordonn�es des limites de la cam�ra en utilisant ViewportToWorldPoint
        Vector3 minBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 maxBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // V�rifier si un objet sort des limites de la cam�ra
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.transform.position.x < minBound.x || obj.transform.position.x > maxBound.x ||
                obj.transform.position.y < minBound.y || obj.transform.position.y > maxBound.y)
            {
                // D�truire l'objet
                Destroy(obj);
            }
        }
    }
}

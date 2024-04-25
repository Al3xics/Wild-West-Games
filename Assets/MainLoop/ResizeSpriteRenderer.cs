using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeSpriteRenderer : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;

        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Camera mainCamera = Camera.main;
        float screenWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;

        float scaleFactor = screenWidth / spriteSize.x;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }
}

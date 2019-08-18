using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void Start()
    {
		float height = Camera.main.orthographicSize * 2;
		float width = height * Screen.width/ Screen.height; // basically height * screen aspect ratio
 
		SpriteRenderer[] wallSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer spriteRenderer in wallSpriteRenderers)
		{
			float unitWidth = spriteRenderer.sprite.textureRect.width / spriteRenderer.sprite.pixelsPerUnit;
			float unitHeight = spriteRenderer.sprite.textureRect.height / spriteRenderer.sprite.pixelsPerUnit;
			spriteRenderer.transform.localScale = new Vector3(width / unitWidth, height / unitHeight);
		}
    }
}

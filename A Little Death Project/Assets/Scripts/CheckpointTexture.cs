using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTexture : MonoBehaviour
{
    public bool active;
    SpriteRenderer spriteRenderer;
    public Sprite[] skin;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        active = Checkpoints.active;

        if (!active)
        {
            if (spriteRenderer.sprite != skin[0])
                spriteRenderer.sprite = skin[0];
        }
        else
        {
            if (spriteRenderer.sprite != skin[1])
            {
                spriteRenderer.sprite = skin[1];
            }
        }
    }
}

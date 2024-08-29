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
            {
                spriteRenderer.sprite = skin[0];
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
            }
        }
        else
        {
            if (spriteRenderer.sprite != skin[1])
            {
                spriteRenderer.sprite = skin[1];
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(true);
            }
        }
    }
}

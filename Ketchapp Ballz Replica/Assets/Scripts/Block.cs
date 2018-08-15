using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private TextMeshPro textMeshPro;

    private int hitsRemaining = 5;

    private Color[] colors = new Color[5];

    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //textMeshPro = transform.GetChild(0).GetComponent<TextMeshPro>();
        ColorsSetup();
        UpdateBlockOnBeingHit();
    }

    private void ColorsSetup()
    {
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        colors[3] = Color.yellow;
        colors[4] = Color.magenta;
    }

    void UpdateBlockOnBeingHit()
    {
        textMeshPro.text = hitsRemaining.ToString();
        int randomNumber = UnityEngine.Random.Range(0, 5);
        spriteRenderer.color = Color.Lerp(colors[randomNumber], Color.white, hitsRemaining / 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hitsRemaining--;
        if (hitsRemaining > 0)
            UpdateBlockOnBeingHit();
        else
            Destroy(gameObject);
    }

    public void SetHits(int hits)
    {
        hitsRemaining = hits;
        UpdateBlockOnBeingHit();
    }
}

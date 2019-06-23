using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Sprite brokenSprite;
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockExplosionEffect;

    Level level;

    int timesHit = 0;

    // Start is called before the first frame update
    void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            SetBlockColor();
            level.CountBlocks();
        }
    }

    void SetBlockColor()
    {
        string[] blockColors = new string[] { "#9400D3", "#4B0082", "#0000FF",
        "#00FF00", "#FFFF00", "#FF7F00", "#FF0000" };

        if (ColorUtility.TryParseHtmlString(blockColors[UnityEngine.Random.Range(0, blockColors.Length)], out var color))
        {
            GetComponent<SpriteRenderer>().color = color;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        if (timesHit > 0)
        {
            DestroyBlock();
        }
        else
        {
            timesHit++;
            GetComponent<SpriteRenderer>().sprite = brokenSprite;
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        FindObjectOfType<GameSession>().AddToScore();
        level.BlockDestroyed();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        ExplodeBlock();
    }

    private void ExplodeBlock()
    {
        GameObject blockExplosion = Instantiate(blockExplosionEffect, transform.position, transform.rotation);

        var main = blockExplosion.GetComponent<ParticleSystem>().main;
        main.startColor = GetComponent<SpriteRenderer>().color;

        Destroy(blockExplosion, 3f);
    }
}

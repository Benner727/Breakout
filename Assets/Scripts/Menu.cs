using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float screenHeightInUnits = 12f;
    [SerializeField] GameObject blockExplosionEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateMouseEffect();
        }
    }

    public void CreateMouseEffect()
    {
        GameObject clickEffect = Instantiate(blockExplosionEffect, Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.rotation);

        string[] blockColors = new string[] { "#9400D3", "#4B0082", "#0000FF",
        "#00FF00", "#FFFF00", "#FF7F00", "#FF0000" };

        if (ColorUtility.TryParseHtmlString(blockColors[UnityEngine.Random.Range(0, blockColors.Length)], out var color))
        {
            var main = clickEffect.GetComponent<ParticleSystem>().main;
            main.startColor = color;
        }

        Destroy(clickEffect, 2f);
    }
}

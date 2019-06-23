using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 0.75f;
    [SerializeField] float maxX = 15.25f;
    [SerializeField] float screenWidthInUnits = 16f;

    new BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
        float mouseXPos = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        paddlePos.x = Mathf.Clamp(mouseXPos, minX, maxX);
        transform.position = paddlePos;
    }

    public float GetWidth()
    {
        return collider.bounds.size.x;
    }
}

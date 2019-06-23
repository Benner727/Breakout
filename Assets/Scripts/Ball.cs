using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle;
    [SerializeField] Vector2 pushVelocity = new Vector2(2f, 15f);
    [SerializeField] float speed = 20f;
    [SerializeField] AudioClip bounceSound;

    Vector2 paddleToBallVector;
    bool hasStarted = false;

    Rigidbody2D myRigidbody2D;
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle.transform.position;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();

        GetComponent<TrailRenderer>().enabled = false;

        myRigidbody2D.velocity = Vector2.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockToPaddle();
            Launch();
        }
    }

    private void Launch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidbody2D.velocity = pushVelocity;
            hasStarted = true;
            GetComponent<TrailRenderer>().enabled = true;
        }
    }

    private void LockToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            myAudioSource.PlayOneShot(bounceSound);

            if (collision.gameObject.name == "Paddle")
            {
                float x = hitFactor(transform.position, paddle.transform.position, paddle.GetWidth());
                Vector2 direction = new Vector2(x, 1).normalized;
                myRigidbody2D.velocity = direction * speed;
            }
        }
    }

    private float hitFactor(Vector2 ballPos, Vector2 paddlePos, float paddleWidth)
    {
        return (ballPos.x - paddlePos.x) / paddleWidth;
    }
}

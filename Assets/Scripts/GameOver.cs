using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        scoreText.text = "Score: " + FindObjectOfType<GameSession>().GetScore().ToString();
        FindObjectOfType<GameSession>().ResetGame();
    }
}

using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TMP_Text txtScorePlayer1;
    public TMP_Text txtScorePlayer2;
    public TMP_Text winner;

    public AudioSource audioWin;

    public Transform player1;
    public Transform player1GoalKeeper;
    private Vector2 player1OGPosition;

    public Transform player2;
    public Transform player2GoalKeeper;
    private Vector2 player2OGPosition;

    private Ball ball;

    private int player1Score;
    private int player2Score;

    private bool isWin;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public void Start()
    {
        player1OGPosition = player1.position;
        player2OGPosition = player2.position;

        ball = GameObject.Find("Ball").GetComponent<Ball>();
    }

    public void Player1Scored() 
    {
        player1Score++;
        txtScorePlayer1.text = player1Score.ToString();

        if (player1Score >= 5) 
        {
            // decir que ha ganado y reproducir sonido
            AnnounceWinner("Ganador: Jugador 1");
            isWin = true;
        }

        RestartPositions();
    }

    public void Player2Scored()
    {
        player2Score++;
        txtScorePlayer2.text = player2Score.ToString();

        if (player2Score >= 5)
        {
            // decir que ha ganado y reproducir sonido
            AnnounceWinner("Ganador: Jugador 2");
            isWin = true;
        }

        RestartPositions();
    }

    private void AnnounceWinner(string text)
    {
        winner.text = text;
        audioWin.Play();
    }

    private void RestartGame() 
    {
        player1Score = 0;
        player2Score = 0;
        txtScorePlayer1.text = player1Score.ToString();
        txtScorePlayer2.text = player2Score.ToString();
        winner.text = "";
    }

    private void RestartPositions() 
    {
        // Volver a colocar palas en posiciones
        player1.position = player1OGPosition;
        player1GoalKeeper.position = new Vector2(player1GoalKeeper.position.x, 0);

        player2.position = player2OGPosition;
        player2GoalKeeper.position = new Vector2(player2GoalKeeper.position.x, 0);

        ball.Reset();

        // Esperar un segundo antes de volver a jugar
        StartCoroutine(WaitToPlayAgain(3f));
    }

    private IEnumerator WaitToPlayAgain(float pauseTime) 
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;

        if (isWin)
        { 
            // Si isWin (es la última ronda y ya hay ganador)
            RestartGame();
            isWin = false;
        }
    }
}

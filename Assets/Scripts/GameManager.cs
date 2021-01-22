using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance = null;
    public GameObject[] pieces;
    public GameObject currentPiece;
    int pieceIdx;
    enum PieceState {
        none = 0, player = 1, AI = 2
    }
    public enum WinnerState {
        none = 0, player = 1, AI = 2, tie = 3
    }
    public int[,] board = new int[3, 3];

    public WinnerState currentWinner;

    public Text state;
    public Text playerScore;
    public Text AIScore;
    
    public bool isEnd;

    void Awake()
    {
        if (sharedInstance != null && sharedInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    void Start()
    {
        Init();
        SetRandomCurrentPiece();
    }

    void Init()
    {
        isEnd = false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = (int)PieceState.none;
            }
        }
    }

    void SetRandomCurrentPiece()
    {
        pieceIdx = Random.Range(0, 2);
        currentPiece = pieces[pieceIdx];
        
        if (pieceIdx == 0)
        {
            state.text = "Player Turn";
        }
        else
        {
            state.text = "AI Turn";
        }
    }

    public void ChangePiece()
    {
        currentPiece = pieces[(++pieceIdx) % 2];
        NextTurn();
    }

    public void NextTurn()
    {
        if (state.text == "Player Turn")
        {
            state.text = "AI Turn";
        }
        else
        {
            state.text = "Player Turn";
        }
    }

    public void GameEnd()
    {
        isEnd = true;
        UpdateState(currentWinner);
        UpdateScore(currentWinner);
    }

    public void CheckWinner()
    {
        int winner = (int)WinnerState.none;

        // Horizontal
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                currentWinner = (WinnerState)board[i, 0];
                return;
            }
        }

        // Vertical
        for (int i = 0; i < 3; i++)
        {
            if (board[0, i] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                currentWinner = (WinnerState)board[i, 0];
                return;
            }
        }

        // Diagonal
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            currentWinner = (WinnerState)board[1, 1];
            return;
        }
        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            currentWinner = (WinnerState)board[1, 1];
            return;
        }

        // Tie
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == (int)PieceState.none)
                {
                    currentWinner = (WinnerState)winner;
                    return;
                }
            }
        }
        
        currentWinner = WinnerState.tie;
    }

    void UpdateState(WinnerState winner)
    {
        if (winner == WinnerState.player)
        {
            state.text = "Player Win!";
        }
        else if (winner == WinnerState.AI)
        {
            state.text = "AI Win!";
        }
        else if (winner == WinnerState.tie)
        {
            state.text = "Tie!";
        }
        else
        {
            Debug.Log("WinnerSate is none!");
        }
    }

    void UpdateScore(WinnerState winner)
    {
        if (winner == WinnerState.player)
        {
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
        }
        else if (winner == WinnerState.AI)
        {
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }
}
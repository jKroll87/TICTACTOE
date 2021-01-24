using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [Header("Board")]
    [HideInInspector]
    public int[,] board = new int[3, 3];
    enum PieceState {
        none = 0, player = 1, AI = 2
    }
    enum WinnerState {
        none = 0, player = 1, AI = 2, tie = 3
    }
    
    
    public void BoardInit()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = (int)PieceState.none;
            }
        }
    }

    public int CheckWinner()
    {
        // Horizontal
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
            {
                return board[i, 0];
            }
        }

        // Vertical
        for (int i = 0; i < 3; i++)
        {
            if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
            {
                return board[0, i];
            }
        }

        // Diagonal
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
        {
            return board[1, 1];
        }
        if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
        {
            return board[1, 1];
        }

        // Tie
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == (int)PieceState.none)
                {
                    return board[i, j];
                }
            }
        }
        
        return (int)WinnerState.tie;
    }
}
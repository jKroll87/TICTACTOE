using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public int tileIndex;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SpawnPiece()
    {
        if (gameManager.isEnd == false)
        {
            Instantiate(gameManager.currentPiece, transform.position, Quaternion.identity);
            gameManager.board[tileIndex / 3, tileIndex % 3] = gameManager.pieceIdx + 1;
            gameManager.ChangePiece();
            gameManager.CheckWinner();
            gameManager.DebugBoard();

            if (gameManager.currentWinner != 0)
            {
                gameManager.GameEnd();
            }
        }
    }
}

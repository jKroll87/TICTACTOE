using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
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
            gameManager.ChangePiece();
            gameManager.CheckWinner();

            if (gameManager.currentWinner != 0)
            {
                gameManager.GameEnd();
            }
        }
        
    }
}

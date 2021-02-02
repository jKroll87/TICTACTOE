using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [Header("GameManager")]
    GameManager gameManager;
    BoardManager boardManager;
    UIManager UIManager;

    [Header("Piece")]
    public GameObject[] piecePrefabs = new GameObject[2];
    [HideInInspector]
    public int pieceIndex;
    enum PieceState {
        none = 0, player = 1, AI = 2
    }

    [Header("ObjectPooling")]
    
    static List<GameObject> OPiecePool;
    static List<GameObject> XPiecePool;
    int poolSize = 5;

    void Awake()
    {
        PieceObjectPooling();
    }

    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        boardManager = gameObject.GetComponent<BoardManager>();
        UIManager = gameManager.GetComponent<UIManager>();

        SetRandomCurrentPiece();
    }

    public string GetCurrentPieceName()
    {
        if (pieceIndex == 0)
        {
            return "player";
        }
        else if (pieceIndex == 1)
        {
            return "AI";
        }

        return "error";
    }

    public string GetCurrentWinnerName()
    {
        switch (gameManager.currentWinner)
        {
            case GameManager.WinnerState.none:
                return "none";
            case GameManager.WinnerState.player:
                return "player";
            case GameManager.WinnerState.AI:
                return "AI";
            case GameManager.WinnerState.tie:
                return "tie";
            default:
                return "error";
        }
    }

    void PieceObjectPooling()
    {
        if (OPiecePool == null)
        {
            OPiecePool = new List<GameObject>();
        }
        if (XPiecePool == null)
        {
            XPiecePool = new List<GameObject>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject OPieceObject = Instantiate(piecePrefabs[0]);
            GameObject XPieceObject = Instantiate(piecePrefabs[1]);

            OPieceObject.SetActive(false);
            OPiecePool.Add(OPieceObject);

            XPieceObject.SetActive(false);
            XPiecePool.Add(XPieceObject);
        }
    }

    void SetRandomCurrentPiece()
    {
        pieceIndex = Random.Range(0, 2);
    }

    public void SpawnPiece(GameObject tile)
    {
        TileController tileController = tile.GetComponent<TileController>();
        int index = tileController.tileIndex;

        if (boardManager.board[index / 3, index % 3] == 0)
        {
            if (pieceIndex == 0)
            {
                foreach(GameObject piece in OPiecePool)
                {
                    if (piece.activeSelf == false)
                    {
                        piece.SetActive(true);
                        piece.transform.position = tile.transform.position;

                        break;
                    }
                }
            }
            else if (pieceIndex == 1)
            {
                foreach(GameObject piece in XPiecePool)
                {
                    if (piece.activeSelf == false)
                    {
                        piece.SetActive(true);
                        piece.transform.position = tile.transform.position;

                        break;
                    }
                }
            }

            boardManager.board[index / 3, index % 3] = pieceIndex + 1;
            gameManager.SetCurrentWinner(boardManager.CheckWinner());

            if (gameManager.currentWinner == GameManager.WinnerState.none)
            {
                ChangePiece();
                UIManager.SetStateText(GetCurrentPieceName());
            }
            else
            {
                gameManager.Stop();
                UIManager.SetWinStateText(GetCurrentWinnerName());
                UIManager.UpdateScore(GetCurrentWinnerName());
            }
        }
    }

    void ChangePiece()
    {
        pieceIndex = (pieceIndex + 1) % 2;
    }

    void DisablePieces()
    {
        for (int i = 0; i < poolSize; i++)
        {
            OPiecePool[i].SetActive(false);
            XPiecePool[i].SetActive(false);
        }
    }
}
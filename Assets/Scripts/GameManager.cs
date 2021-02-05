using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Singleton")]
    public static GameManager sharedInstance = null;

    [Header("GameManager")]
    PieceManager pieceManager;
    BoardManager boardManager;
    UIManager UIManager;

    [Header("Winner")]
    public bool isStop;
    public enum WinnerState {
        none = 0, player = 1, AI = 2, tie = 3
    }
    public WinnerState currentWinner;

    void Awake()
    {
        Singleton();
    }

    void Start()
    {
        pieceManager = gameObject.GetComponent<PieceManager>();
        boardManager = gameObject.GetComponent<BoardManager>();
        UIManager = gameObject.GetComponent<UIManager>();

        Init();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void SetCurrentWinner(int value)
    {
        currentWinner = (WinnerState)value;
    }

    void Singleton()
    {
        if (sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }

    public void Init()
    {
        isStop = false;
        boardManager.BoardInit();
        UIManager.SetStateText(pieceManager.GetCurrentPieceName());
        pieceManager.DisablePieces();
    }

    public void Stop()
    {
        isStop = true;
    }
}
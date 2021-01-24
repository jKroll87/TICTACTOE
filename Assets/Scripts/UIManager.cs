using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    public Text stateText;
    public Text playerScoreText;
    public Text AIScoreText;
    public Image playerScoreBoardImage;
    public Image AIScoreBoardImage;
    public Sprite[] scoreBoardSprite = new Sprite[2];

    [Header("String")]
    string playerString = "player";
    string AIString = "AI";
    string tieString = "tie";

    public void SetStateText(string turn)
    {
        if (turn == playerString)
        {
            stateText.text = "Player Turn";
            playerScoreBoardImage.sprite = scoreBoardSprite[0];
            AIScoreBoardImage.sprite = scoreBoardSprite[1];
        }
        else if (turn == AIString)
        {
            stateText.text = "AI Turn";
            playerScoreBoardImage.sprite = scoreBoardSprite[1];
            AIScoreBoardImage.sprite = scoreBoardSprite[0];
        }
    }

    public void SetWinStateText(string winner)
    {
        if (winner == playerString)
        {
            stateText.text = "Player Win!";
        }
        else if (winner == AIString)
        {
            stateText.text = "AI Win!";
        }
        else if (winner == tieString)
        {
            stateText.text = "Tie!";
        }
    }

    public void UpdateScore(string winner)
    {
        if (winner == playerString)
        {
            playerScoreText.text = (int.Parse(playerScoreText.text) + 1).ToString();
        }
        else if (winner == AIString)
        {
            AIScoreText.text = (int.Parse(AIScoreText.text) + 1).ToString();
        }
    }
}

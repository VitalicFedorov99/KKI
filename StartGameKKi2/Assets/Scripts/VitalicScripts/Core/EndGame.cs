using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using CardGame.Core;


public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject _imageEndGame;
    [SerializeField] private Text _textEndGame;
    
    
    public void GameOver (Player player)
    {
        Time.timeScale = 0;
        if (player.PlayerType==PlayerType.First)
        {
            _textEndGame.text = "Победил второй игрок";
        }
        else
        {
            _textEndGame.text = "Победил первый  игрок";
        }
        _imageEndGame.SetActive(true);
    }

}

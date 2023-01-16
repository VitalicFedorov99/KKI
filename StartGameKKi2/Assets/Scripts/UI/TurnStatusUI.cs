using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class TurnStatusUI : MonoBehaviour
{
    [SerializeField] private Text _turnValueText;
    [SerializeField] private Text _playerText;

   

    public void OnUpdate(int turn)
    {
        _turnValueText.text = turn.ToString();
        _playerText.text = CardGame.Core.Game.CurrentPlayer.name;
    }
}

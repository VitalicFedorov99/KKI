using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class TurnStatusUI : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Text _turnValueText;
    [SerializeField] private Text _playerText;

    private void Awake()
    {
        _game.turnNumberChanged.AddListener(OnUpdate);
    }

    private void OnUpdate(int turn)
    {
        _turnValueText.text = turn.ToString();
        _playerText.text = Game.CurrentPlayer.name;
    }
}

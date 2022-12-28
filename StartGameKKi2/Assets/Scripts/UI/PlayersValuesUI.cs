using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayersValuesUI : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private Text _playerForceValueText;
        [SerializeField] private Text _playerHealthValueText;
        [SerializeField] private Text _enemyForceValueText;
        [SerializeField] private Text _enemyHealthValueText;

        private void Awake()
        {
            _game.playerChanged.AddListener(OnPlayerChange);
        }

        public void OnPlayerChange()
        {
            Game.CurrentPlayer.helthChanged.RemoveListener(OnEnemyHealthChange);
            Game.CurrentPlayer.forceChanged.RemoveListener(OnEnemyForceChange);
            Game.Enemy.helthChanged.RemoveListener(OnPlayerHealthChange);
            Game.Enemy.forceChanged.RemoveListener(OnPlayerForceChange);
            
            Game.CurrentPlayer.helthChanged.AddListener(OnPlayerHealthChange);
            Game.CurrentPlayer.forceChanged.AddListener(OnPlayerForceChange);
            Game.Enemy.helthChanged.AddListener(OnEnemyHealthChange);
            Game.Enemy.forceChanged.AddListener(OnEnemyForceChange);
        }

        public void OnPlayerHealthChange(int value)
        {
            _playerHealthValueText.text = value.ToString();
        }
        
        public void OnPlayerForceChange(int value)
        {
            _playerForceValueText.text = value.ToString();
        }
        
        public void OnEnemyHealthChange(int value)
        {
            _enemyHealthValueText.text = value.ToString();
        }
        
        public void OnEnemyForceChange(int value)
        {
            _enemyForceValueText.text = value.ToString();
        }
    }
}

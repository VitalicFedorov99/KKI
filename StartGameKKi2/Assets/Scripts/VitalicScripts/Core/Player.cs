using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.Events;

using CardGame.Cards.Model;
using CardGame.Cards.Base;
using CardGame.Field;

namespace CardGame.Core
{
    public class Player : MonoBehaviour
    {
        public Phase CurrentPhase { get; set; }

        public int CurrentHealth => _currentHealth;
        public int Force { get; set; }




        [SerializeField] private int _health = 15;
        [SerializeField] private int _maxCountCard = 5;


        private int _currentHealth = 1;
        private ActionsWithCards _actionsWithCards;
        private PlayerActions _playerActions;
        public UnityEvent moveEnded;
        public UnityEvent died;
        public UnityEvent<int> healthChanged =new UnityEvent<int>();
        public UnityEvent<int> forceChanged = new UnityEvent<int>();

        

        public void TakeDamage(int damage)// добавить твин ранения
        {
            _currentHealth -= damage;
            healthChanged.Invoke(CurrentHealth);

            if (_currentHealth <= 0)
                died?.Invoke();
        }

        public void SpendForce(int value)
        {
            Force -= value;
            forceChanged?.Invoke(Force);
            _actionsWithCards.DisableCards();
        }

        public void UpdateValues()
        {
            forceChanged?.Invoke(Force);
            healthChanged?.Invoke(_currentHealth);
        }

        public void AppForce(int movenumber) 
        {
            Force += movenumber / 2 + movenumber % 2;
            if (Force > 10)
                Force = 10;
        }


        public ActionsWithCards GetActionsWithCards() 
        {
            return _actionsWithCards;
        }
        public PlayerActions GetPlayerActions()
        {
            return _playerActions;
        }


        private void Awake()
        {
            _actionsWithCards = GetComponent<ActionsWithCards>();
            _playerActions = GetComponent<PlayerActions>();
            _currentHealth = _health;
        }
    }




    public enum Phase
    {
        Defend,
        Play,
        Attack
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using CardGame.Field;
using CardGame.Cards.Base;
namespace CardGame.Core
{
    public class Game : MonoBehaviour
    {

        [SerializeField] private Player _player1;
        [SerializeField] private Player _player2;
        [SerializeField] private UiLineRender _lineRender;
        [SerializeField] private UpdateForceHealthPanel _updateForceHealthPanel; 

        private GeneratorGameBoard _generatorGameBoard;
        [SerializeField] private ObserverRound _observerRound;
        [SerializeField] private TurnStatusUI _turnStatus;
       

        private static int _moveNumber = 0;
        public static int MoveNumber => _moveNumber;

        public static Player CurrentPlayer;
        public static Player Enemy;
        public static GeneratorGameBoard Generator;
        public static ObserverRound Observer;

        public UnityEvent moveEnded;
        //public UnityEvent<int> turnNumberChanged;
        public UnityEvent playerChanged;
        public UnityEvent<Player> gameOver;

        private void Awake()
        {
            Time.timeScale = 1;
            _generatorGameBoard = GetComponent<GeneratorGameBoard>();
            _observerRound = GetComponent<ObserverRound>();
            _observerRound.Setup();
            Observer = _observerRound;
            _generatorGameBoard.StartGame(_player1.GetActionsWithCards(), _player2.GetActionsWithCards());
            Generator = _generatorGameBoard;
            CurrentPlayer = _player1;
            Enemy = _player2;
            _moveNumber++;
            UpdateEventPlayer();
            FirstPhase();
            _turnStatus.OnUpdate(1);
        }

        public void NextPlayer() 
        {
            UpdateEventPlayer();
            FirstPhase();
            _observerRound.CallStartStep(CurrentPlayer.PlayerType);
        }

       
       



        public void FirstPhase() 
        {

            CurrentPlayer.GetPlayerActions().StartMove();
            CurrentPlayer.GetPlayerActions().GoToPlayCardPhase();
            CurrentPlayer.UpdateValues();
            Enemy.UpdateValues();
            CurrentPlayer.GetActionsWithCards().CardsCanDrag(true);
            CurrentPlayer.GetActionsWithCards().CardsCanAct();
        }

      

        public void FightPhase() 
        {
            CurrentPlayer.GetPlayerActions().GoToAttackPhase();
        }

        public void Fight() 
        {
            Enemy.GetPlayerActions().GoToFight();
        }


        public void EndAttackPhase() 
        {
            _moveNumber++;

            if (_moveNumber % 2 == 0)
            {
                CurrentPlayer = _player2;
                Enemy = _player1;
            }
            else
            {
                CurrentPlayer = _player1;
                Enemy = _player2;
            }

            UpdateEventPlayer();
            _generatorGameBoard.Generate(CurrentPlayer.GetActionsWithCards(), Enemy.GetActionsWithCards());
            CurrentPlayer.UpdateValues();
            Enemy.UpdateValues();
            CurrentPlayer.GetPlayerActions().GoToDefendPhase();
            _turnStatus.OnUpdate(CurrentPlayer.GetPlayerActions().GetStep()+1);
            //üòCurrentPlayer.GetActionsWithCards().
        }

        public void EndMove()
        {
            moveEnded?.Invoke();
        }

        private void Win(Player winner)
        {
            Debug.Log($"{winner.name} win!!!");
            gameOver?.Invoke(winner);
        }
        private void UpdateEventPlayer() 
        {
            CurrentPlayer.forceChanged.RemoveAllListeners();
            CurrentPlayer.healthChanged.RemoveAllListeners();
            Enemy.forceChanged.RemoveAllListeners();
            Enemy.healthChanged.RemoveAllListeners();

            CurrentPlayer.forceChanged.AddListener(_updateForceHealthPanel.UpdatePlayerForce);
            CurrentPlayer.healthChanged.AddListener(_updateForceHealthPanel.UpdatePlayerHealth);
            Enemy.forceChanged.AddListener(_updateForceHealthPanel.UpdateEnemyForce);
            Enemy.healthChanged.AddListener(_updateForceHealthPanel.UpdateEnemyHealth);
        }
    }
}

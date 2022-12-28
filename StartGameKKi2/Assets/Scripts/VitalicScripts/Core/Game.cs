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

        private static int _moveNumber = 0;
        public static int MoveNumber => _moveNumber;

        public static Player CurrentPlayer;
        public static Player Enemy;
        public static GeneratorGameBoard Generator; 


        public UnityEvent moveEnded;
        public UnityEvent<int> turnNumberChanged;
        public UnityEvent playerChanged;
        public UnityEvent<Player> gameOver;

        private void Awake()
        {

            _generatorGameBoard = GetComponent<GeneratorGameBoard>();
            /* _player1.moveEnded.AddListener(NextMove);
             _player2.moveEnded.AddListener(NextMove);
             _player1.died.AddListener(() => Win(_player2));
             _player2.died.AddListener(() => Win(_player1));*/
            _generatorGameBoard.StartGame(_player1.GetActionsWithCards(), _player2.GetActionsWithCards());
            Generator = _generatorGameBoard;
            CurrentPlayer = _player1;
            Enemy = _player2;
            _moveNumber++;
            UpdateEventPlayer();
            FirstPhase();
        }

        public void NextPlayer() 
        {
            _generatorGameBoard.Generate(CurrentPlayer.GetActionsWithCards(),Enemy.GetActionsWithCards());
            UpdateEventPlayer();
            FirstPhase();

        }

        public void NextMove()
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

            playerChanged?.Invoke();

            CurrentPlayer.GetPlayerActions().StartMove();

            CurrentPlayer.UpdateValues();
            Enemy.UpdateValues();
            _generatorGameBoard.Generate(CurrentPlayer.GetActionsWithCards(), Enemy.GetActionsWithCards());
            turnNumberChanged?.Invoke(_moveNumber / 2 + _moveNumber % 2);

            #region LOG
            if (_moveNumber <= 2)
                Debug.Log($"Привет, {CurrentPlayer.name}\n" +
                          "Добро пожаловать в ход №1 \n" +
                          "На первом ходу есть только фаза розыгрыша карт, поэтому фазу защиты пропускаем нажав кнопку Защита готова" +
                          "(выбери карту с руки и перетяни ее на поле в синюю зону, цена выбранной карты не может превышать Мощь)");

            if (_moveNumber > 2 && _moveNumber <= 4)
                Debug.Log($"{CurrentPlayer.name}: В следующий ход Вы сможете назначить ОДНУ карту на боевом поле для защиты от атаки соперника. Сейчас пропустите эту фазу (Он Вас не атаковал). Нажмите кнопку Защита готова");
            #endregion
        }



        public void FirstPhase() 
        {
            CurrentPlayer.AppForce(MoveNumber);
            CurrentPlayer.GetPlayerActions().GoToPlayCardPhase();
            CurrentPlayer.UpdateValues();
            Enemy.UpdateValues();
            CurrentPlayer.GetActionsWithCards().CardsCanDrag(true);
            CurrentPlayer.GetActionsWithCards().CardsCanAct();
        }

        public void Defend()
        {
           // Enemy.GetPlayerActions().Attack(CurrentPlayer);
            CurrentPlayer.GetPlayerActions().GoToPlayCardPhase();
            #region LOG
            if (_moveNumber > 2 && _moveNumber <= 4)
                Debug.Log($"{CurrentPlayer.name}: Разыграйте карты с руки если хотите. (этого можно не делать, если у Вас есть коварный план поднакопить мощи). Когда закончите нажмите кнопку К атаке");
            #endregion
        }


        public void Fight() 
        {
            Enemy.GetPlayerActions().GoToFight();
        }

       
        public void EndPlayCardPhase()
        {
           
            CurrentPlayer.GetPlayerActions().GoToAttackPhase();
            #region LOG
            if (_moveNumber <= 2)
                Debug.Log("На первом ходу Игроки не могут атаковать противника. Уступите стул сопернику. (Нажмите Закончить)");
            if (_moveNumber > 2 && _moveNumber <= 4)
                Debug.Log($"{CurrentPlayer.name}: Назначьте атакующего если хотите и нажмите и нажмите Завершить ход. После этого уступите место сопернику");
            #endregion
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
            //ьтCurrentPlayer.GetActionsWithCards().
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

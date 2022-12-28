using System;
using Field;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player _player1;
        [SerializeField] private Player _player2;

        private static int _moveNumber = 0;
        public static int MoveNumber => _moveNumber;

        public static Player CurrentPlayer;
        public static Player Enemy;

        public UnityEvent moveEnded;
        public UnityEvent<int> turnNumberChanged;
        public UnityEvent playerChanged;
        public UnityEvent<Player> gameOver;
        
        private void Awake()
        {
            _player1.moveEnded.AddListener(NextMove);
            _player2.moveEnded.AddListener(NextMove);
            _player1.died.AddListener(() => Win(_player2));
            _player2.died.AddListener(() => Win(_player1));
        }

        public void NextMove()
        {
            _moveNumber ++;

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

            CurrentPlayer.StartMove();
            
            CurrentPlayer.UpdateValues();
            Enemy.UpdateValues();
            
            GetComponent<GameBoardGenerator>().Generate(CurrentPlayer, Enemy);
            turnNumberChanged?.Invoke(_moveNumber / 2 + _moveNumber % 2);

            #region LOG
            if (_moveNumber <= 2)
                Debug.Log($"Привет, {CurrentPlayer.name}\n" +
                          "Добро пожаловать в ход №1 \n" +
                          "На первом ходу есть только фаза розыгрыша карт, поэтому фазу защиты пропускаем нажав кнопку Защита готова" +
                          "(выбери карту с руки и перетяни ее на поле в синюю зону, цена выбранной карты не может превышать Мощь)");
            
            if(_moveNumber > 2 && _moveNumber <= 4)
                Debug.Log($"{CurrentPlayer.name}: В следующий ход Вы сможете назначить ОДНУ карту на боевом поле для защиты от атаки соперника. Сейчас пропустите эту фазу (Он Вас не атаковал). Нажмите кнопку Защита готова");
            #endregion
        }

        public void Defend()
        {
            Enemy.Attack(CurrentPlayer);
            CurrentPlayer.GoToPlayCardPhase();
            #region LOG
            if(_moveNumber > 2 && _moveNumber <= 4)
                Debug.Log($"{CurrentPlayer.name}: Разыграйте карты с руки если хотите. (этого можно не делать, если у Вас есть коварный план поднакопить мощи). Когда закончите нажмите кнопку К атаке");
            #endregion
        }

        public void EndPlayCardPhase()
        {
            CurrentPlayer.GoToAttackPhase();
            #region LOG
            if(_moveNumber <= 2)
                Debug.Log("На первом ходу Игроки не могут атаковать противника. Уступите стул сопернику. (Нажмите Закончить)");
            if(_moveNumber > 2 && _moveNumber <= 4)
              Debug.Log($"{CurrentPlayer.name}: Назначьте атакующего если хотите и нажмите и нажмите Завершить ход. После этого уступите место сопернику");  
            #endregion
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
    }
}
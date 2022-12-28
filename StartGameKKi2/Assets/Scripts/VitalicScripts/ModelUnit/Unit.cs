using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cards.UnitEffects;
using JetBrains.Annotations;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using CardGame.Cards.Model;
using CardGame.Core;
namespace CardGame.Cards.Base
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private string _name;
        

        [CanBeNull][SerializeField] private UnitStartEffect _startEffect;
        private Status _status = Status.NonBoard;
        private int _startHealth;
        private Player _player;
        private Unit _enemyUnit = null;
        public Status Status => _status;
        public int Damage => _damage;
        public int Health => _health;

        public int Price => _price;

        public string Name => _name;

        public UnityEvent<Status> statusChanged;
        public UnityEvent<int> healthChanged = new UnityEvent<int>();
        public UnityEvent<CardUnit> cardClicked;
        
        public void Setup(ModelUnit model, Player player) 
        {
            _player = player;
            _price = model.Price;
            _damage = model.Damage;
            _health = model.Health;
            _name = model.name;
            _startHealth = _health;
            healthChanged.AddListener(GetComponent<UICard>().UpdateHealth);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            healthChanged?.Invoke(_health);
            if (_health <= 0)
            {
                Debug.Log($"{_name} ушла в отбой");
                //_player.GetComponent<ActionsWithCards>().MoveCardToDumpFromFoard(GetComponent<Card>());
                var card = Game.Generator.CardInDump(GetComponent<Card>(),_player);
                _player.GetActionsWithCards().AddCardInList(_player.GetActionsWithCards()._inDumpCards, card);
                _player.GetActionsWithCards().RemoveCardInList(_player.GetActionsWithCards()._inBoardCards, card);
                GetComponent<CardUnit>().Dead();
                GetComponent<CardUnit>().ResetCard();
                healthChanged?.Invoke(_health);
            }
        }

        public void SetStatus(Status status) 
        {
            _status = status;
        }

        public UnitStartEffect GetStartEffect() 
        {
            return _startEffect;
        }

        public Player GetPlayer() 
        {
            return _player;
        }

        public void Resp() 
        {
            _health = _startHealth;
        }

        public void SetEnemy(Unit enemy) 
        {
            _enemyUnit = enemy;
        }
        public Unit GetEnemy() 
        {
            return _enemyUnit;
        }
    }

    public enum Status
    {
        NonBoard,
        FirstTurn,
        NotCanAct,
        CanAct,
        Attacker,
        Defender
    }
}

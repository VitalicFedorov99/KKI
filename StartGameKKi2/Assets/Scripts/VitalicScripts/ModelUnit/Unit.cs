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
using CardGame.Cards.Effects;

namespace CardGame.Cards.Base
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private string _name;



        [CanBeNull] [SerializeField] private UnitStartEffect _startEffect;
        [SerializeField] private Status _status = Status.NonBoard;
        private int _startHealth;
        [SerializeField] private Player _player;
        [SerializeField] private Unit _enemyUnit = null;
        [SerializeField] private Player _enemyPlayer = null;
        public Status Status => _status;
        public int Damage => _damage;
        public int Health => _health;

        public int Price => _price;

        public string Name => _name;


        public bool IsBlocked;



        public UnityEvent<Status> statusChanged;
        public UnityEvent<int> healthChanged = new UnityEvent<int>();
        public UnityEvent<int> damageChanged = new UnityEvent<int>();
        public UnityEvent<CardUnit> cardClicked;


        public int CountCell;
        public CellEffect Cell;
        public bool IsEnemy;

        public void Setup(ModelUnit model, Player player)
        {
            _player = player;
            _price = model.Price;
            _damage = model.Damage;
            _health = model.Health;
            _name = model.name;
            _startHealth = _health;


            CountCell = model.CountCell;
            Cell = model.Cell;
            IsEnemy = model.IsEnemy;
            IsBlocked = model.IsBlocked;
            healthChanged.AddListener(GetComponent<UICard>().UpdateHealth);
            damageChanged.AddListener(GetComponent<UICard>().UpdateDamage);
        }

        public void TakeDamage(int damage)
        {
            if (gameObject.TryGetComponent(out InvicibleComponent invicible))
            {
                damage = 0;
            }
            _health -= damage;
            healthChanged?.Invoke(_health);
            if (_health <= 0)
            {
                Debug.Log($"{_name} ���� � �����");
                var card = Game.Generator.CardInDump(GetComponent<Card>(), _player);
                _player.GetActionsWithCards().AddCardInList(_player.GetActionsWithCards()._inDumpCards, card);
                _player.GetActionsWithCards().RemoveCardInList(_player.GetActionsWithCards()._inBoardCards, card);
                GetComponent<CardUnit>().Dead();
                GetComponent<CardUnit>().ResetCard();
                healthChanged?.Invoke(_health);
            }
        }


        public void SetCell(GameObject obj)
        {
            if (obj != gameObject && CountCell > 0)
            {
                CheckCell(obj);
            }
        }

        public void ChangeHealth(int health) 
        {
            _health += health;
            healthChanged?.Invoke(_health);
        }

        public void ChangeDamage(int damage) 
        {
            _damage += damage;
            damageChanged?.Invoke(_damage);
        }

        public void Healthing(int health)
        {

            _health += health;
            if (_health > _startHealth)
            {
                _health = _startHealth;

            }
            healthChanged?.Invoke(_health);
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

        public void SetEnemyPlayer(Player enemyPlayer)
        {
            _enemyPlayer = enemyPlayer;
        }

        public Player GetEnemyPlayer()
        {
            return _enemyPlayer;
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

        public void UpdateCountCell()
        {
            if (CountCell <= 0)
            {
                GetComponent<Card>().ActionStart?.Invoke();
            }
        }

        private void CheckCell(GameObject obj)
        {
            if (Cell == CellEffect.Card)
            {
                if (obj.TryGetComponent(out Card card))
                {
                    if (IsEnemy)
                    {
                        if (card.GetPlayer() != Game.CurrentPlayer)
                        {
                            ApplayEffect.instance.AddCell(obj);
                            // _objectCells.Add(obj);
                            CountCell--;
                            UpdateCountCell();
                        }
                    }
                    else
                    {
                        if (card.GetPlayer() == Game.CurrentPlayer)
                        {
                            Debug.LogError("55555555");
                            ApplayEffect.instance.AddCell(obj);
                            CountCell--;
                            UpdateCountCell();
                        }
                    }

                }
            }
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

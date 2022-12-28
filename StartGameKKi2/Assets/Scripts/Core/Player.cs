using System;
using System.Collections.Generic;
using System.Linq;
using Cards.Base;
using Field;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

namespace Core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Card> _allPlayerCards;
        [SerializeField] private int _health = 10;

        private Phase _phase;
        
        private int _force;
        private int _currentHealth = 1;
        
        private List<Card> _inDeckCards;
        private List<UnitCard> _inBoardCards;
        public List<Card> _inHandCards;
        private List<Card> _inDumpCards;

        public Phase CurrentPhase => _phase;
        public int CurrentHealth => _currentHealth;
        public int Force => _force;
        
        public List<UnitCard> InBoardCards => new List<UnitCard>(_inBoardCards);
        public List<Card> InHandCards => new List<Card>(_inHandCards);
        public Card LastInDumpCard => _inDumpCards.LastOrDefault();
        public int InDeckCardsCount => _inDeckCards.Count;
        
        public UnityEvent moveEnded;
        public UnityEvent died;
        public UnityEvent<int> helthChanged;
        public UnityEvent<int> forceChanged;

        private void Awake()
        {
            _currentHealth = _health;
            
            _inDeckCards = new List<Card>();
            _inHandCards = new List<Card>();
            _inBoardCards = new List<UnitCard>();
            _inDumpCards = new List<Card>();
            
            foreach (var cardPrefab in _allPlayerCards)
            {
                Card instantiateCard = Instantiate(cardPrefab, FindObjectOfType<Canvas>().transform);
                _inDeckCards.Add(instantiateCard);
                instantiateCard.gameObject.SetActive(false);
            }
            
            _inDeckCards.ForEach(card => card.InitOwner(this));
            _inDeckCards = GetRandomDeck(_inDeckCards);
        }

        public void StartMove()
        {
            AddCardsToHand();
            _inBoardCards.ForEach(card => card.SetCanAct());

            if (Game.MoveNumber > 20)
            {
                TakeDamage(1);
            }
            
            int addForceCount = Game.MoveNumber / 2 + Game.MoveNumber % 2;
            if (addForceCount > 6) 
                addForceCount = 6;
            _force += Game.MoveNumber / 2 + Game.MoveNumber % 2;
            if (_force > 10)
                _force = 10;
            
            Debug.Log($"Мощь {gameObject.name} выросла на {addForceCount}");

            GoToDefendPhase(Game.Enemy);
        }

        public void GoToDefendPhase(Player enemy)
        {
            _phase = Phase.Defend;
            
            _inHandCards.ForEach(card => card.IsCanDrag = false);
            _inBoardCards.ForEach(card => card.SetCanAct());

            #region LOG
            if (Game.Enemy.InBoardCards.FirstOrDefault(card => card.Status == Status.Attacker) && Game.MoveNumber <= 6)
            {
                Debug.Log($"{Game.Enemy.name} Вас атакует (Коварный негодяй!)");
                Debug.Log("Выставтье защитника на поле боя если хотите и нажмите Защита готова");
            }
            #endregion
        }
        
        public void GoToPlayCardPhase()
        {
            _phase = Phase.Play;
            _inBoardCards.ForEach(card =>
            {
                card.IsCanDrag = false;
                if(card.Status != Status.FirstTurn && card.Status != Status.Defender) 
                    card.SetCannotAct();
            });
            _inHandCards.ForEach(card => card.IsCanDrag = true);
            FindObjectOfType<GameBoardGenerator>().Generate(Game.CurrentPlayer, Game.Enemy);
            DisableCards();
        }

        public void GoToAttackPhase()
        {
            _phase = Phase.Attack;
            _inBoardCards.ForEach(card =>
            {
                card.IsCanDrag = true;
                if(card.Status != Status.FirstTurn && card.Status != Status.Defender) 
                    card.SetCanAct();
            });
            _inHandCards.ForEach(card => card.IsCanDrag = false);
        }

        public void Attack(Player enemy)
        {
            bool isAttacked = false;
            
            _inBoardCards.ForEach(card =>
            {
                if (card.Status == Status.Attacker)
                {
                    isAttacked = true;
                    UnitCard enemyDefender = enemy.InBoardCards.FirstOrDefault(enemyCard => enemyCard.Status == Status.Defender && enemyCard.Health > 0);
                    if (enemyDefender)
                    {
                        enemyDefender.TakeDamage(card.Damage);
                        card.TakeDamage(enemyDefender.Damage);
                        Debug.Log($"{card.Owner.name}:{card.Name} Нанесла урон {card.Damage} {enemyDefender.Owner.name}:{enemyDefender.Name}");
                        Debug.Log($"{enemyDefender.Owner.name}:{enemyDefender.Name} Нанесла урон {enemyDefender.Damage} {card.Owner.name}:{card.Name}");
                    }
                    else
                    {
                        enemy.TakeDamage(card.Damage);
                        Debug.Log($"{enemy.name} получил {card.Damage} урона");
                    }
                }
            });

            if (!isAttacked)
                Debug.Log("Всё спокойно");
            
            MoveCardsFromBoardToDump(_inBoardCards.Where(card => card.Health <= 0).ToArray());
            enemy.MoveCardsFromBoardToDump(enemy.InBoardCards.Where(card => card.Health <= 0).ToArray());
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            helthChanged.Invoke(CurrentHealth);
            
            if(_currentHealth <= 0)
                died?.Invoke();
            
            Debug.Log($"{name} получил урон {damage}");
        }

        public void MoveCardToDumpFromHand(Card card)
        {
            if (_inHandCards.Contains(card))
            {
                _inHandCards.Remove(card);
                _inDumpCards.Add(card);
                card.gameObject.SetActive(false);
            }
        }
        
        public void MoveCardToBoardFromHand(UnitCard card)
        {
            if (_inHandCards.Contains(card) && card.Price <= _force)
            { 
                card.OnGoInBoard(true);
                _inHandCards.Remove(card);
                _inBoardCards.Add(card);
                SpendForce(card.Price);
            }
            
            DisableCards();
            
            FindObjectOfType<GameBoardGenerator>().Generate(Game.CurrentPlayer, Game.Enemy);
        }

        public void MoveCardsFromBoardToDump(params UnitCard[] cards)
        {
            foreach (var card in cards)
            {
                if (_inBoardCards.Contains(card))
                {
                    _inBoardCards.Remove(card);
                    _inDumpCards.Add(card);
                    card.ResetCard();
                    card.gameObject.SetActive(false);
                }
            }
            FindObjectOfType<GameBoardGenerator>().Generate(Game.CurrentPlayer, Game.Enemy);
        }

        public void MoveCardsFromDeckToBoard(int count)
        {
            List<UnitCard> unitCards = _inDeckCards.OfType<UnitCard>().Take(count).ToList();
            unitCards.ForEach(card =>
            {
                _inDeckCards.Remove(card);
                _inBoardCards.Add(card);
                card.OnGoInBoard(false);
            });
            FindObjectOfType<GameBoardGenerator>().Generate(Game.CurrentPlayer, Game.Enemy);
        }

        public void SpendForce(int value)
        {
            _force -= value;
            forceChanged?.Invoke(_force);
            DisableCards();
        }

        public void UpdateValues()
        {
            forceChanged?.Invoke(_force);
            helthChanged?.Invoke(_currentHealth);
        }

        public void BlockControl()
        {
            _inHandCards.ForEach(card => card.IsCanDrag = false);
        }

        private List<Card> GetRandomDeck(List<Card> deck)
        {
            Random random = new Random();
            return deck.OrderBy(card => random.Next()).ToList();
        }
        
        
        private void MoveCardsCardFromDumpToDeck(params Card[] cards)
        {
            foreach (var card in cards)
            {
                _inDumpCards.Remove(card);
                _inDeckCards.Add(card);
            }

            _inDeckCards = GetRandomDeck(_inDeckCards);
        }

        public void RemoveCardFromBoard(UnitCard card)
        {
            _inBoardCards.Remove(card);
        }

        public void AddCardToBoard(UnitCard card)
        {
            _inBoardCards.Add(card);
        }

        private void AddCardsToHand()
        {
            int cardsToFullHand = 5 - _inHandCards.Count;
            if(_inDeckCards.Count < cardsToFullHand)
                MoveCardsCardFromDumpToDeck(new List<Card>(_inDumpCards).ToArray());
                
            _inDeckCards.Take(cardsToFullHand).ToList().ForEach(card =>
            {
                _inDeckCards.Remove(card);
                _inHandCards.Add(card);
            });
        }

        private void DisableCards()
        {
            _inHandCards.ForEach(card =>
            {
                if (card.Price > _force)
                    card.IsCanDrag = false;
            });
        }
    }

    public enum Phase
    {
        Defend,
        Play,
        Attack
    }
}
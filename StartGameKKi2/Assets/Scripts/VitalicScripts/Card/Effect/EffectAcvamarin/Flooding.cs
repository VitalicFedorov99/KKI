using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardGame.Cards.Base;
using CardGame.Core;
namespace CardGame.Cards.Effects
{
    public class Flooding : MonoBehaviour
    {
        [SerializeField] private int _countCard;
        [SerializeField] private List<Card> _cards; 
        public void Flood()
        {
            Debug.Log("возвращение карт");
            _cards = new List<Card>();
            var counter = _countCard;
            var count = Game.Enemy.GetActionsWithCards()._inBoardCards.Count;
            if (count < counter) 
            {
                counter = count;
            }
            for (int i = 0; i < counter; i++)
            {
                _cards.Add(Game.Enemy.GetActionsWithCards()._inBoardCards[i]);
            }
            for (int i = 0; i < counter; i++) 
            {
                Game.Enemy.GetActionsWithCards().RemoveCardInList(Game.Enemy.GetActionsWithCards()._inBoardCards, _cards[i]);
                Game.CurrentPlayer.GetActionsWithCards().AddCardInList(Game.Enemy.GetActionsWithCards()._inHandCards, _cards[i]);
                Game.CurrentPlayer.GetActionsWithCards().MoveCard(_cards[i], Game.Generator._enemyArea);
                _cards[i].ResetCard();
                _cards[i].CloseCard();

            }
            ApplayEffect.instance.EffectComplite();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
using CardGame.Core;
namespace CardGame.Cards.Effects
{
    public class EffectSos : MonoBehaviour
    {

        public int countCard;
        public CardClass classCard;
        public void Call()
        {
            
            var cards = Game.CurrentPlayer.GetActionsWithCards()._inDeckCards;
            int counter = 0;
            List<Card> chooseCard = new List<Card>();
            foreach(Card card in cards) 
            {
                if (card.GetClassCard() == classCard) 
                {
                    chooseCard.Add(card);
                    counter++;
                    if (counter >= countCard) 
                    {
                        break;
                    }
                }
            }

            foreach (Card card in chooseCard) {
                Game.CurrentPlayer.GetActionsWithCards().RemoveCardInList(Game.CurrentPlayer.GetActionsWithCards()._inDeckCards, card);
                Game.CurrentPlayer.GetActionsWithCards().AddCardInList(Game.CurrentPlayer.GetActionsWithCards()._inBoardCards, card);
                card.OpenCard();
                card.IsCanDrag=false;
                card.transform.parent = Game.Generator._playerBoard;
                card.transform.position = Game.Generator._playerBoard.position;
            }
            ApplayEffect.instance.EffectComplite();
        }
    }
}

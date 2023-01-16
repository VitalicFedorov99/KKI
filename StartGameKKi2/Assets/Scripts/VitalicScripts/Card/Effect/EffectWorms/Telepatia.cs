using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
using CardGame.Core;

using System.Linq;

namespace CardGame.Cards.Effects
{
    public class Telepatia : Effect
    {

        public int countCard;
        int count;
        List<Card> choosedCard;
        public void Call()
        {
            var areaChooseCard = Game.Generator.GetAreaChooseCard();
            Game.Generator.SwitchAreaChooseCard(true);

            ApplayEffect.instance.SetTelepatia(this.gameObject);
            List<Card> cards = Game.CurrentPlayer.GetActionsWithCards()._inDeckCards;
            choosedCard = new List<Card>();
            count = countCard;
            if (cards.Count < count)
            {
                count = cards.Count;
            }

            for (int i = 0; i < count; i++)
            {
                var card = cards.LastOrDefault();
                cards.Remove(card);
                choosedCard.Add(card);
                card.gameObject.AddComponent<TelepatiaComponent>();
                card.transform.parent = areaChooseCard;
                card.TogelIsCanDrag(true);
                card.OpenCard();
            }
            // ApplayEffect.instance.EffectComplite();
        }

        public void EndTelepatia() 
        {
            var cardsInHand = Game.CurrentPlayer.GetActionsWithCards()._inHandCards;
            var cardsInDeck = Game.CurrentPlayer.GetActionsWithCards()._inDeckCards;
            for (int i=0;i<count; i++) 
            {
                Destroy(choosedCard[i].GetComponent<TelepatiaComponent>());
                if (!cardsInHand.Contains(choosedCard[i])) 
                {
                    Game.CurrentPlayer.GetActionsWithCards().AddCardInList(cardsInDeck, choosedCard[i]);
                    choosedCard[i].transform.parent = Game.Generator._playerDeck;
                    choosedCard[i].transform.position = Game.Generator._playerDeck.position;
                    choosedCard[i].CloseCard();
                }
            }
            Game.Generator.SwitchAreaChooseCard(false);
        }

    }
}

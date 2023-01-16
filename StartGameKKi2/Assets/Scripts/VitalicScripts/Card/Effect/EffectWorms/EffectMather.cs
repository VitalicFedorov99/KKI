using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
using CardGame.Core;

namespace CardGame.Cards.Effects
{
    public class EffectMather : MonoBehaviour
    {

        public int HealthRobots;
        public int DamageWorms;
        public void Call()
        {
            var cards=Game.CurrentPlayer.GetActionsWithCards()._inBoardCards;
            foreach(Card card in cards) 
            {
                if (card.GetClassCard() == CardClass.Robot) 
                {
                    card.GetUnit().ChangeHealth(1);
                }
                if (card.GetClassCard() == CardClass.Worms)
                {
                    card.GetUnit().ChangeDamage(2);
                }
            }
            ApplayEffect.instance.EffectComplite();

        }
    }

}
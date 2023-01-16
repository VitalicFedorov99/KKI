using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardGame.Cards.Base;
using CardGame.Core;
namespace CardGame.Cards.Effects
{
    public class StealEffect : MonoBehaviour
    {

        [SerializeField] private CardClass _class;


        public void StealCard()
        {
            Debug.LogError("ворую");
            var cards = ApplayEffect.instance.GetCells();
            cards[0].GetComponent<Card>().SetPlayer(Game.CurrentPlayer);
            Game.Enemy.GetActionsWithCards().RemoveCardInList(Game.Enemy.GetActionsWithCards()._inBoardCards, cards[0].GetComponent<Card>());
            Game.CurrentPlayer.GetActionsWithCards().AddCardInList(Game.CurrentPlayer.GetActionsWithCards()._inBoardCards, cards[0].GetComponent<Card>());
            Game.CurrentPlayer.GetActionsWithCards().MoveCard(cards[0].GetComponent<Card>(), Game.Generator._playerBoard);
            ApplayEffect.instance.EffectComplite();
        }
    }
}

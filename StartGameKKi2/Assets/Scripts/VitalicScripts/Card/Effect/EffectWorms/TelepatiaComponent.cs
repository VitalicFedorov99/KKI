using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
using CardGame.Core;
namespace CardGame.Cards.Effects
{
    public class TelepatiaComponent : MonoBehaviour
    {
        public void Call()
        {
            Game.CurrentPlayer.GetActionsWithCards().AddCardInList(Game.CurrentPlayer.GetActionsWithCards()._inHandCards, GetComponent<Card>());
            transform.parent = Game.Generator._playerArea;
            ApplayEffect.instance.EndTelepatia();
            ApplayEffect.instance.EffectComplite();
            //Debug.LogError()
        }
    }
}

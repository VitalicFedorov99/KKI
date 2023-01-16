using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
using CardGame.Core;
namespace CardGame.Cards.Effects
{
    public class Invicible : MonoBehaviour
    {
        public int time;
        public void AddInvicible()
        {
            var cards = ApplayEffect.instance.GetCells();
            foreach (var card in cards)
            {
                card.AddComponent<InvicibleComponent>();
                card.GetComponent<InvicibleComponent>().Setup(time);
            }
            ApplayEffect.instance.EffectComplite();
        }
    }
}

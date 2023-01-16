using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
namespace CardGame.Cards.Effects
{
    public class CloudingMind : MonoBehaviour
    {
        public int time;
        public void AddCloudMind()
        {
            var cards = ApplayEffect.instance.GetCells();
            foreach (var card in cards)
            {
                card.AddComponent<CloudingMindComponent>();
                card.GetComponent<CloudingMindComponent>().Setup(time);
            }
            ApplayEffect.instance.EffectComplite();
        }
    }
}

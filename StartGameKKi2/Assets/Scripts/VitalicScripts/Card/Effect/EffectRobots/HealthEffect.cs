using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
namespace CardGame.Cards.Effects
{
    public class HealthEffect : MonoBehaviour
    {
        [SerializeField] private int _health;
        public void AddHealth()
        {
            Debug.LogError("лечение");
            var cards = ApplayEffect.instance.GetCells();
            cards[0].GetComponent<CardUnit>().GetUnit().Healthing(_health);
            ApplayEffect.instance.EffectComplite();
        }
    }
}

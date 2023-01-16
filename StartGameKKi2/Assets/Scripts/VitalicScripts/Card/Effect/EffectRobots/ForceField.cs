using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


using CardGame.Cards.Base;
namespace CardGame.Cards.Effects
{
    public class ForceField : Effect
    {

        [SerializeField] private int _health;
        




        public void AddHealth()
        {
            var cards = ApplayEffect.instance.GetCells();
            foreach (var card in cards)
            {
                card.GetComponent<CardUnit>().GetUnit().TakeDamage(-_health);
            }
            ApplayEffect.instance.EffectComplite();
        }


    }
}

using System;
using Cards.Base;
using Core;
using UnityEngine;

namespace Cards.UnitEffects
{
    public class AddHealthStartEffect : UnitStartEffect
    {
        [SerializeField] private int _healthValue;
        
        public override void MakeStartEffect()
        {
            if (Game.CurrentPlayer.InBoardCards.Count > 0)
            {
                //Game.CurrentPlayer.BlockControl();
                Game.CurrentPlayer.InBoardCards.ForEach(card => card.cardClicked.AddListener(AddHealth));
                Debug.Log("Кликните по юниту которому нужно восстановить здоровье");
            }
        }

        private void AddHealth(UnitCard card)
        {
            card.TakeDamage(-_healthValue);
            Game.CurrentPlayer.GoToPlayCardPhase();
            Game.CurrentPlayer.InBoardCards.ForEach(card1 => card1.cardClicked.RemoveListener(AddHealth));
        }
    }
}
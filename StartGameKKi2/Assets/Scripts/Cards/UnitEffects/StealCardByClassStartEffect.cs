using System.Collections.Generic;
using System.Linq;
using Cards.Base;
using Core;
using UnityEngine;

namespace Cards.UnitEffects
{
    public class StealCardByClassStartEffect : UnitStartEffect
    {
        [SerializeField] private CardClass _class;
        
        public override void MakeStartEffect()
        {
            List<UnitCard> classCards = Game.Enemy.InBoardCards.Where(card => card.Class == _class).ToList();

            if (classCards.Count > 0)
            {
                //Game.CurrentPlayer.BlockControl();
                classCards.ForEach(card => card.cardClicked.AddListener(StealCard));
                Debug.Log("Кликните по юниту над которым нужно установить контроль");
            }
        }

        private void StealCard(UnitCard unitCard)
        {
            Game.Enemy.RemoveCardFromBoard(unitCard);
            Game.CurrentPlayer.AddCardToBoard(unitCard);
            
            Game.CurrentPlayer.GoToPlayCardPhase();
            Game.CurrentPlayer.InBoardCards.ForEach(card => card.cardClicked.RemoveListener(StealCard));
        }
    }
}
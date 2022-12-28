using Cards.Base;
using UnityEngine;

namespace Cards.EffectCards
{
    public class AddHealthEffectCard : EffectUnitCard
    {
        [SerializeField] private int _addHealthCount = 3;
        
        public override void MakeEffect(UnitCard card)
        {
            if (card.Owner == Owner && !Owner.InHandCards.Contains(card))
            {
                base.MakeEffect(card);
                card.TakeDamage(-_addHealthCount);
            }
        }
    }
}
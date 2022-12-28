using Cards.Base;
using UnityEngine;

namespace Cards.EffectCards
{
    public class SosEffectPlayerCard : EffectPlayerCard
    {
        [SerializeField] private int _cardCount = 3;
        [SerializeField] private bool _isStartEffect;
        
        public override void MakeEffect()
        {
            base.MakeEffect();
            Owner.MoveCardsFromDeckToBoard(_cardCount);
        }
    }
}
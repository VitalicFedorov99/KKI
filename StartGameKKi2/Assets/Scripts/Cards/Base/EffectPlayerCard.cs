using UnityEngine;

namespace Cards.Base
{
    public class EffectPlayerCard : Card
    {
        
        public virtual void MakeEffect()
        {
            Owner.MoveCardToDumpFromHand(this);
            IsCanDrag = false;
            Owner.SpendForce(Price);
        }
    }
}
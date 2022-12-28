using UnityEngine;

namespace Cards.Base
{
    public class EffectUnitCard : Card
    {
        public virtual void MakeEffect(UnitCard card)
        {
            Owner.MoveCardToDumpFromHand(this);
            IsCanDrag = false;
            Owner.SpendForce(Price);
        }
    }
}
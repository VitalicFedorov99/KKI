using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.Cards.Model
{

    [CreateAssetMenu(fileName = "New Card Effect", menuName = "Cards/Effect")]
    public class ModelEffect : ModelCard
    {
        public override void UpdateType()
        {
            TypeCard = TypeCard.Effect;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards.Base;
namespace CardGame.Cards.Model
{
    [CreateAssetMenu(fileName ="New Card Unit",menuName ="Cards/Unit")]
    public class ModelUnit : ModelCard
    {
        public int Health;
        public int Damage;
        public override void UpdateType()
        {
            TypeCard = TypeCard.Effect;
        }
    }
}

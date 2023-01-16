using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cards.Base;
using UnityEngine.Events;

namespace CardGame.Cards.Model
{
    [CreateAssetMenu(fileName ="New Card Unit",menuName ="Cards/Unit")]
    public class ModelUnit : ModelCard
    {
        public int Health;
        public int Damage;

        public int CountCell;
        public CellEffect Cell;
        public bool IsEnemy;

        public bool IsBlocked;

 
        public override void UpdateType()
        {
            TypeCard = TypeCard.Unit;
        }
    }
}

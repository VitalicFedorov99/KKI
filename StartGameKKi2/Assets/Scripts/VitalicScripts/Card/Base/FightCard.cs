using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardGame.Cards.Base;
namespace CardGame.Cards
{
    public class FightCard : MonoBehaviour
    {
        [SerializeField] private Unit _enemy;
        private Unit _unit;

        public void Setup(Unit unit)
        {
            _unit = unit;
        }


        public void SetEnemy(Unit enemy) 
        {
            _enemy = enemy;
        }
    }
}

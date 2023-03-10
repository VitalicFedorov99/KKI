using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
namespace CardGame.Core
{
    public class ObserverCard : MonoBehaviour
    {
        public static ObserverCard _instance;
        [SerializeField] private Unit _unitClic;
        [SerializeField] private Unit _unitPointEnter;
        [SerializeField] private Card _currentCard;


        private void Awake()
        {
            _instance = this;
        }

        public void SetUnitClic(Unit unit) 
        {
            _unitClic = unit;
        }

        public Unit GetUnitClic() 
        {
            return _unitClic;
        }

        public void SetUnitPointEnter(Unit unit)
        {
            _unitPointEnter = unit;
        }

        public Unit GetUnitPointEnter()
        {
            return _unitPointEnter;
        }


        public void SetCurrentCard(Card card) 
        {
            _currentCard = card;
        }

        public Card GetCurrentCard() 
        {
            return _currentCard;
        }
    }
}

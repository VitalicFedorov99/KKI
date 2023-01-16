using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;
using CardGame.Cards.Base;
using CardGame.Core;

namespace CardGame.Cards.Effects
{
    public class Effect : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private int _countCell;
        [SerializeField] private CellEffect _cell;
        [SerializeField] private bool _isActive = false;
        [SerializeField] private bool _isEnemy;

        private Card _card;
        [SerializeField] private List<GameObject> _objectCells = new List<GameObject>();


        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isActive)
            {
                ApplayEffect.instance.Apply();
            }
        }

        public void Setup(int countCell, CellEffect cell, bool isEnemy)
        {
            _countCell = countCell;
            _cell = cell;
            _isActive = true;
            _isEnemy = isEnemy;
            _card = GetComponent<Card>();
            //UpdateCountCell();
        }



        

        public void SetCell(GameObject obj)
        {
            if (obj != gameObject && _countCell > 0)
            {
                CheckCell(obj);
            }
        }



        private void CheckCell(GameObject obj)
        {
            if (_cell == CellEffect.Card)
            {
                if (obj.TryGetComponent(out Card card))
                {
                    if (_isEnemy)
                    {
                        if (card.GetPlayer() != Game.CurrentPlayer)
                        {
                            ApplayEffect.instance.AddCell(obj);
                            // _objectCells.Add(obj);
                            _countCell--;
                            UpdateCountCell();
                        }
                    }
                    else
                    {
                        if (card.GetPlayer() == Game.CurrentPlayer)
                        {
                            ApplayEffect.instance.AddCell(obj);
                            _countCell--;
                            UpdateCountCell();
                        }
                    }

                }
            }
        }

        public void UpdateCountCell()
        {
            if (_countCell <= 0)
            {

                _card.ActionStart?.Invoke();
            }
        }


    }
}

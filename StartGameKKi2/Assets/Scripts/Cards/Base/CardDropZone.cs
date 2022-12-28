using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Cards.Base
{
    [RequireComponent(typeof(UnitCard))]
    public class CardDropZone : MonoBehaviour, IDropHandler
    {
        [SerializeField] private UnitCard _unitCard;

        private void OnValidate()
        {
            _unitCard = GetComponent<UnitCard>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out EffectUnitCard effectCard))
            {
                if(effectCard.IsCanDrag)
                    effectCard.MakeEffect(_unitCard);
            }
        }
    }
}
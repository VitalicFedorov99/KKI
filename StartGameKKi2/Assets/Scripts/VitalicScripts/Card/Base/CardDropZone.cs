using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace CardGame.Base
{
    public class CardDropZone : MonoBehaviour
    {
        //[SerializeField] private UnitCard _unitCard;

     

        public void OnDrop(PointerEventData eventData)
        {
           /* if (eventData.pointerDrag.TryGetComponent(out EffectUnitCard effectCard))
            {
                if (effectCard.IsCanDrag)
                    effectCard.MakeEffect(_unitCard);
            }*/
        }
    }
}

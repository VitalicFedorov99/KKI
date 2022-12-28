using Cards.Base;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Field
{
    public class PlayerBoardDropZone : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out UnitCard unitCard))
            {
                if (unitCard.IsCanDrag)
                {
                    unitCard.transform.SetParent(transform);
                    unitCard.Owner.MoveCardToBoardFromHand(unitCard);
                }
            }

            if (eventData.pointerDrag.TryGetComponent(out EffectPlayerCard effectPlayerCard))
            {
                if (effectPlayerCard.IsCanDrag)
                    effectPlayerCard.MakeEffect();
                
            }
        }
    }
}

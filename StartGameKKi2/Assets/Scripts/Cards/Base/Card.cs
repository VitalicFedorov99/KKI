using Core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards.Base
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private CardClass _class = CardClass.Non;
        [SerializeField] private bool _isLegendary;
        [SerializeField] private string _name;
        [SerializeField] [TextArea] private string _description;
        [SerializeField] public Sprite _sprite;
        [SerializeField] private int _price;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Player _owner;

        private Vector2 _startPosition;

        public bool IsCanDrag;
        public Player Owner => _owner;
        public int Price => _price;
        public string Name => _name;
        public string Description => _description;
        public CardClass Class => _class;

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void InitOwner(Player owner)
        {
            _owner = owner;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsCanDrag)
            {
                _canvasGroup.blocksRaycasts = false;
                _startPosition = transform.position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsCanDrag)
                transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsCanDrag)
            {
                _canvasGroup.blocksRaycasts = true;
                transform.position = _startPosition;
            }
        }
    }
}
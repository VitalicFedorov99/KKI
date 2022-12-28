using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using CardGame.Core;
using CardGame.Cards.Model;
using CardGame.Ui;
using CardGame.Cards.Effects;

namespace CardGame.Cards.Base
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private ModelCard _model;
        private CanvasGroup _canvasGroup;
        private UICard _uicard;
        private Vector2 _startPosition;
        public bool IsCanDrag;
        public bool IsPointerEnTer;
        private Player _owner;


        private UiLineRender _lineRenderer;

        private bool IsOpen = false;
        private Unit _unit = null;
        private CardUnit _cardUnit = null;
        private UpdaterPopap _popap;

        public UnityEvent ActionStart = new UnityEvent();

        public UnityEvent ActionDead = new UnityEvent();
        //Unit _unit = null;


        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _uicard = GetComponent<UICard>();
        }

        public void Setup(Player player, ModelCard model, UpdaterPopap updaterPopap)
        {
            _owner = player;
            _uicard.Setup(model);
            _model = model;
            _popap = updaterPopap;
            ActionStart = model?.ActionStart;
            ActionDead = model?.ActionDead;

            if (_model.TypeCard == TypeCard.Unit)
            {
                _unit = gameObject.AddComponent<Unit>();
                _cardUnit = gameObject.AddComponent<CardUnit>();
                // var modelUnit = model as ModelUnit
                _unit.Setup(_model as ModelUnit, player);
                _cardUnit.Setup(_unit);
            }
            else
            {

            }
        }

        public void SetupLineRenderer(UiLineRender lineRender)
        {
            _lineRenderer = lineRender;
        }


        public void CreatePricel()
        {
            _lineRenderer.Setup(GetComponent<RectTransform>());
        }

        public void DestroyPricel()
        {
            _lineRenderer.Off();
        }

        public void OpenCard()
        {
            _uicard.OpenCard(_model);
            IsOpen = true;
        }

        public void CloseCard()
        {
            _uicard.CloseCard();
            IsOpen = false;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsCanDrag)
            {
                TogelIsRaycast(false);
                _startPosition = transform.position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsCanDrag)
            {
                transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsCanDrag)
            {
                TogelIsRaycast(true);
                transform.position = _startPosition;
            }
        }

        public Unit GetUnit()
        {
            return _unit;
        }

        public CardUnit GetCardUnit()
        {
            return _cardUnit;
        }

        public Player GetPlayer()
        {
            return _owner;
        }
        public UICard GetUICard()
        {
            return _uicard;
        }

        public TypeCard GetTypeCard()
        {
            return _model.TypeCard;
        }

        public void TogelIsCanDrag(bool flag)
        {
            IsCanDrag = flag;
        }

        public void TogelIsRaycast(bool flag)
        {
            _canvasGroup.blocksRaycasts = flag;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {


            if (IsOpen)
            {
                _popap.UpdatePopap(_model);
                if (_model.TypeCard == TypeCard.Unit)
                {
                    if (_unit.Status == Status.Defender)
                    {
                        _uicard.ActivCard();
                        var enemy = _unit.GetEnemy();

                        if (enemy != null)
                        {
                            enemy.GetComponent<Card>().GetUICard().ActivCard();
                        }
                        ObserverCard._instance.SetUnitPointEnter(_unit);
                    }
                }
                
            }


            //transform.position = new Vector3(transform.position.x, transform.position.y + 50, transform.position.z);
            //IsPointerEnTer = true;
        }


        public void OnPointerExit(PointerEventData eventData)
        {

            _popap.OffPopap();
            var unit =ObserverCard._instance.GetUnitPointEnter();
            if (unit != null)
            {
                    unit.GetComponent<Card>().GetUICard().DeactivCard();
                    var enemy = unit.GetEnemy();

                    if (enemy != null)
                    {
                        enemy.GetComponent<Card>().GetUICard().DeactivCard();
                    }
                //ObserverCard._instance.SetUnitPointEnter(null);
            }
            //transform.position = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
            //IsPointerEnTer = false;

        }

        private void Update()
        {


            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _lineRenderer.Off();
            }

        }
            
    }
}

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
    public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler//, ICellEffect
    {
        [SerializeField] private ModelCard _model;
        private CanvasGroup _canvasGroup;
        private UICard _uicard;
        private Vector2 _startPosition;
        public bool IsCanDrag;
        public bool IsPointerEnTer;
       [SerializeField] private Player _owner;


        private UiLineRender _lineRenderer;

        [SerializeField] private bool IsOpen = false;
        private Unit _unit = null;
        private CardUnit _cardUnit = null;
        private Effect _effect = null;
        private UpdaterPopap _popap;

        public UnityEvent ActionStart = new UnityEvent();

        public UnityEvent ActionRegulary = new UnityEvent();

        public UnityEvent ActionFight = new UnityEvent(); 

        public UnityEvent ActionDead = new UnityEvent();

        //  public CellEffect Cell => CellEffect.Card;

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
            ActionRegulary = model.ActionRegulary;

            if (_model.TypeCard == TypeCard.Unit)
            {
                _unit = gameObject.AddComponent<Unit>();
                _cardUnit = gameObject.AddComponent<CardUnit>();
                _unit.Setup(_model as ModelUnit, player);
                _cardUnit.Setup(_unit);
             
            }
            else
            {
                _effect = gameObject.AddComponent<Effect>();

                var modelEffect = _model as ModelEffect;
                _effect.Setup(modelEffect.CountCell, modelEffect.Cell, modelEffect.IsEnemy);
            }
        }

        public void SetupLineRenderer(UiLineRender lineRender)
        {
            _lineRenderer = lineRender;
        }

        public int GetCost() 
        {
            return _model.Price;
        }

        public string GetName() 
        {
            return _model.Name;
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

        public Effect GetEffect()
        {
            return _effect;
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
        public void SetPlayer(Player player)
        {
            _owner = player;
        }
        public UICard GetUICard()
        {
            return _uicard;
        }

        public TypeCard GetTypeCard()
        {
            return _model.TypeCard;
        }

        public CardClass GetClassCard() 
        {
            return _model.Class;
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
        }

        public void ResetCard()
        {
            _cardUnit.ResetCard();
        }

        public void OnPointerExit(PointerEventData eventData)
        {

            _popap.OffPopap();
            var unit = ObserverCard._instance.GetUnitPointEnter();
            if (unit != null)
            {
                unit.GetComponent<Card>().GetUICard().DeactivCard();
                var enemy = unit.GetEnemy();

                if (enemy != null)
                {
                    enemy.GetComponent<Card>().GetUICard().DeactivCard();
                }
            }

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

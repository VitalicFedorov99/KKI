using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;


using UnityEngine.UI;
using UnityEngine.EventSystems;
using CardGame.Core;
using CardGame.Cards.Model;

namespace CardGame.Cards.Effects
{
    public class ApplayEffect : MonoBehaviour
    {

        static public ApplayEffect instance;
        [SerializeField] private Effect _effect;
        [SerializeField] private Card _card;
        [SerializeField] private bool _isActiv = false;
        [SerializeField] private Unit _unit;

        [SerializeField] private List<GameObject> _cells;

        [SerializeField] private GraphicRaycaster m_Raycaster;
        [SerializeField] private EventSystem m_EventSystem;
        [SerializeField] private GameObject _raycastObject = null;
        PointerEventData m_PointerEventData;



        [SerializeField] private GameObject _objTelepatia;
        private TypeCard _type;
      //  [SerializeField] private




        public bool IsActive() 
        {
            return _isActiv;
        }

        public void EffectComplite() 
        {
            _card.DestroyPricel();
            if (_type == TypeCard.Effect)
            {
                var card = Game.Generator.CardInDump(_card, Game.CurrentPlayer);
                Game.CurrentPlayer.GetActionsWithCards().AddCardInList(Game.CurrentPlayer.GetActionsWithCards()._inDumpCards, card);
                Game.CurrentPlayer.GetActionsWithCards().RemoveCardInList(Game.CurrentPlayer.GetActionsWithCards()._inHandCards, card);
            }
            StartCoroutine(CoroutineSwitchOff());
        }

        public void SetupEffect(Effect effect, Card card)
        {
            _effect = effect;
            _card = card;
            _isActiv = true;
            _type = TypeCard.Effect;
            effect.UpdateCountCell();
        }

        public void SetupUnit(Unit unit,Card card) 
        {
            _unit = unit;
            _card = card;
            _isActiv = true;
            _type = TypeCard.Unit;
        }
        public void Apply()
        {
            _card.CreatePricel();
        }

        public void ClearCells() 
        {
            _cells.Clear();
            _isActiv = false;
        }

        public void AddCell(GameObject obj) 
        {
            _cells.Add(obj);
        }

        public void SetTelepatia(GameObject obj) 
        {
            _objTelepatia = obj;
        }


        public List<GameObject> GetCells() 
        {
            return _cells;
        }
        private void Start()
        {
            if (instance == null) 
            {
                instance = this;
            }
        }


        private void CheckCountCell() 
        {
            //if(Game.CurrentPlayer.GetActionsWithCards()._inBoardCards <_effect.)
        }


        private void SwitchOff() 
        {
            _isActiv = false;
            _effect = null;
            _card = null;
            _unit = null;
            ClearCells();
        }

        public void EndTelepatia() 
        {
            _objTelepatia.GetComponent<Telepatia>().EndTelepatia();
        }
        
        IEnumerator CoroutineSwitchOff() 
        {
            yield return new WaitForSeconds(0.1f);
            SwitchOff();
        }

       private void Update()
        {
            //Check if the left Mouse button is clicked
            if (_isActiv)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    m_PointerEventData = new PointerEventData(m_EventSystem);
                    m_PointerEventData.position = Input.mousePosition;

                 
                    List<RaycastResult> results = new List<RaycastResult>();
                   
                    m_Raycaster.Raycast(m_PointerEventData, results);

                    foreach (RaycastResult result in results)
                    {
                        if (result.gameObject.TryGetComponent(out Card card))
                        {
                            if (_raycastObject != result.gameObject)
                            {
                                _raycastObject = result.gameObject;
                                if (_type == TypeCard.Effect)
                                {
                                    _effect.SetCell(_raycastObject);
                                }
                                else 
                                {
                                    _unit.SetCell(_raycastObject);
                                }
                            }
                            if(result.gameObject.TryGetComponent(out TelepatiaComponent telepat)) 
                            {
                                telepat.Call();
                            }
                        }
                    }
                }
            }
        }


    }


   
}

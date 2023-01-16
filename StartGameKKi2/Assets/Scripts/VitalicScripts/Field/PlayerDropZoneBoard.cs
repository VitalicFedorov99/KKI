using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CardGame.Cards.Base;
using CardGame.Cards.Model;
using CardGame.Core;
namespace CardGame.Field
{
    public class PlayerDropZoneBoard : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out Card unitCard))
            {

                if (unitCard.GetCost()<=Game.CurrentPlayer.Force) 
                {
                    Debug.Log("Разыгранна " + unitCard.GetName());
                    ObserverCard._instance.SetCurrentCard(unitCard);
                    Game.CurrentPlayer.SpendForce(unitCard.GetCost());
                    if (unitCard.GetTypeCard() == TypeCard.Unit)
                    {
                       
                        if (unitCard.IsCanDrag)
                        {
                            unitCard.transform.SetParent(transform);
                            unitCard.TogelIsCanDrag(false);
                            unitCard.TogelIsRaycast(true);
                            unitCard.GetComponent<CardUnit>().OnGoInBoard(true);
                            if (unitCard.ActionRegulary.GetPersistentEventCount() > 0)
                            {
                                Game.Observer.AddAction(unitCard.ActionRegulary, unitCard.GetPlayer().PlayerType);
                            }

                            Game.CurrentPlayer.GetActionsWithCards().AddCardInList(Game.CurrentPlayer.GetActionsWithCards()._inBoardCards, unitCard);
                            Game.CurrentPlayer.GetActionsWithCards().RemoveCardInList(Game.CurrentPlayer.GetActionsWithCards()._inHandCards, unitCard);
                        }
                    }
                    else if (unitCard.GetTypeCard() == TypeCard.Effect)
                    {
                        if (unitCard.IsCanDrag)
                        {
                            var card = Game.Generator.ApplyEffect(unitCard, Game.CurrentPlayer);
                        }
                    }
                }
                else 
                {
                    Debug.LogError("Не хватает энергии");
                }
            }

            
        }
    }
}

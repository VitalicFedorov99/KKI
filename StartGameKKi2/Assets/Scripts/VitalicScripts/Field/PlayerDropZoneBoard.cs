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
                if (unitCard.GetTypeCard() == TypeCard.Unit)
                {
                    if (unitCard.IsCanDrag)
                    {
                        unitCard.transform.SetParent(transform);
                        //unitCard.GetPlayer().MoveCardToBoardFromHand(unitCard);
                        unitCard.TogelIsCanDrag(false);
                        unitCard.TogelIsRaycast(true);
                        unitCard.ActionStart?.Invoke();
                        unitCard.GetComponent<CardUnit>().SetFirstTurn();
                        //unitCard.GetComponent<CardUnit>().SetCanAct();
                        Debug.Log(Game.CurrentPlayer.GetActionsWithCards()._inBoardCards.Count);
                        
                        Game.CurrentPlayer.GetActionsWithCards().AddCardInList(Game.CurrentPlayer.GetActionsWithCards()._inBoardCards, unitCard);
                        Game.CurrentPlayer.GetActionsWithCards().RemoveCardInList(Game.CurrentPlayer.GetActionsWithCards()._inHandCards, unitCard);
                    }
                }
                else if (unitCard.GetTypeCard() == TypeCard.Effect)
                {
                    if (unitCard.IsCanDrag)
                    {
                        unitCard.ActionStart?.Invoke();
                        var card= Game.Generator.CardInDump(unitCard, Game.CurrentPlayer);
                        //Game.CurrentPlayer.GetActionsWithCards().AddCardInList(Game.CurrentPlayer.GetActionsWithCards()._inDumpCards, card);
                        //Game.CurrentPlayer.GetActionsWithCards().RemoveCardInList(Game.CurrentPlayer.GetActionsWithCards()._inHandCards, card);
                        
                       
                        //unitCard.gameObject.SetActive(false);
                    }
                }
            }

            
        }
    }
}

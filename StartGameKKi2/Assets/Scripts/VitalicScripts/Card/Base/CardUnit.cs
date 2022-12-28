using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using CardGame.Core;


namespace CardGame.Cards.Base
{
    public class CardUnit : MonoBehaviour, IPointerClickHandler
    {

        private Unit _unit;


        public void Setup(Unit unit)
        {
            _unit = unit;
        }
        public void OnGoInBoard(bool isStart)
        {
            _unit.SetStatus(Status.FirstTurn);
            _unit.statusChanged?.Invoke(_unit.Status);
            if (isStart && _unit.GetStartEffect())
                _unit.GetStartEffect().MakeStartEffect();
            Debug.Log($"{_unit.GetPlayer().name}: {_unit.Name} вступает в бой");
        }

        public void ResetCard()
        {
            _unit.SetStatus(Status.NonBoard);
            _unit.Resp();
        }

        public void SetCanAct()
        {
            _unit.SetStatus(Status.CanAct);
            _unit.statusChanged?.Invoke(_unit.Status);
        }

        public void SetCannotAct()
        {
            _unit.SetStatus(Status.CanAct);
            _unit.statusChanged?.Invoke(_unit.Status);
        }

        public void SetFirstTurn() 
        {
            _unit.SetStatus(Status.FirstTurn);
        }

        public void Attack()
        {
            if (_unit.Status != Status.Attacker)
            {
                _unit.SetStatus(Status.Attacker);
                transform.Rotate(new Vector3(0, 0, 30));
            }
            else if (_unit.Status == Status.Attacker)
            {
                _unit.SetStatus(Status.CanAct);
                transform.Rotate(new Vector3(0, 0, -30));
            }
            
        }

        public void Dead() 
        {
            
            if (_unit.Status == Status.Attacker)
            {
                _unit.SetStatus(Status.CanAct);
                transform.Rotate(new Vector3(0, 0, -30));
            }
        }


        public void Defend()
        {
            if (!_unit.GetPlayer().GetActionsWithCards()._inDeckCards.FirstOrDefault(card => card.GetUnit().Status == Status.Defender)
                && Game.Enemy.GetActionsWithCards()._inBoardCards.FirstOrDefault(card => card.GetUnit().Status == Status.Attacker))
            {
                _unit.SetStatus(Status.Defender);
                _unit.statusChanged?.Invoke(_unit.Status);
            }
        }

        public void TakeDamage(int damage)
        {
            _unit.TakeDamage(damage);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Game.CurrentPlayer == GetComponent<Card>().GetPlayer())
            {
                if (Game.CurrentPlayer.CurrentPhase == Phase.Attack)
                {
                    if (_unit.Status == Status.CanAct || _unit.Status == Status.Attacker)
                    {
                        Attack();
                    }
                }
                if(Game.CurrentPlayer.CurrentPhase == Phase.Defend) 
                {
                    if(_unit.Status == Status.CanAct) 
                    {
                        GetComponent<Card>().CreatePricel();
                        ObserverCard._instance.SetUnitClic(_unit);
                    }
                    if(_unit.Status == Status.Defender) 
                    {
                        GetComponent<Card>().CreatePricel();
                        ObserverCard._instance.SetUnitClic(_unit);
                    }
                }
            }
            if(Game.CurrentPlayer!= GetComponent<Card>().GetPlayer()) 
            {
                if(Game.CurrentPlayer.CurrentPhase == Phase.Defend) 
                {
                    if(_unit.Status == Status.Attacker) 
                    {
                        Debug.LogError("Враг найден");
                        
                        var enemy = _unit.GetEnemy();
                        if (enemy != null)
                        {
                            var enemyEnemies = enemy.GetEnemy();
                            if (enemyEnemies != null)
                            {
                                enemyEnemies.SetEnemy(null);
                            }
                        }
                        _unit.SetEnemy(ObserverCard._instance.GetUnitClic());
                        ObserverCard._instance.GetUnitClic().SetEnemy(_unit);
                        ObserverCard._instance.GetUnitClic().SetStatus(Status.Defender);
                        GetComponent<Card>().DestroyPricel();

                    }
                    
                }
            }
        }

    
       
        public Unit GetUnit()
        {
            return _unit;
        }
    }
}
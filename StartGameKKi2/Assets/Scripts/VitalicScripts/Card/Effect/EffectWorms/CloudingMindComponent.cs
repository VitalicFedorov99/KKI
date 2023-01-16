using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.Events;

using CardGame.Core;
using CardGame.Cards.Base;
public class CloudingMindComponent : MonoBehaviour
{
    UnityEvent ev;
    Unit _unit;
    public PlayerType playerType;
    public int ActionTime;
    public void Setup(int time)
    {
        ActionTime = time;
        ev = new UnityEvent();
        ev.AddListener(Call);
        playerType = Game.Enemy.PlayerType;
        Game.Observer.AddAction(ev, playerType);

        _unit = gameObject.GetComponent<Card>().GetUnit();

        if (_unit.Status == Status.Attacker)
        {
            _unit.SetEnemyPlayer(Game.Enemy);
            foreach (Card card in Game.Enemy.GetActionsWithCards()._inBoardCards)
            {
                if (card.GetUnit() != _unit)
                {
                    _unit.SetEnemy(card.GetUnit());
                    Debug.LogError("Нашел врага" + card.name);
                    break;
                }
            }
        }
    }


    public void Call()
    {
        ActionTime--;
        if (ActionTime <= 0)
        {
            Game.Observer.RemoveAction(ev, gameObject.GetComponent<Card>().GetPlayer().PlayerType);
            gameObject.GetComponent<Unit>().SetEnemyPlayer(Game.Enemy);
            Debug.LogError("Удаляю");
            Destroy(this, 3f);
        }

    }
}

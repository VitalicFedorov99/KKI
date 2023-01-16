using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using CardGame.Core;
using CardGame.Cards.Base;


public class InvicibleComponent : MonoBehaviour
{
    public int ActionTime;
    UnityEvent ev;

    public void Setup(int time)
    {
        ActionTime = time;
        ev = new UnityEvent();
        ev.AddListener(Call);
        Game.Observer.AddAction(ev, gameObject.GetComponent<Card>().GetPlayer().PlayerType);
    }


    public void Call()
    {
        ActionTime--;
        if (ActionTime <= 0)
        {
            Game.Observer.RemoveAction(ev, gameObject.GetComponent<Card>().GetPlayer().PlayerType);
            Debug.LogError("”дал€ю");
            Destroy(this,3f);
        }

    }



}

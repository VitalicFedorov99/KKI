using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObserverRound : MonoBehaviour
{
    public List<UnityEvent> ActionStartStepFirstPlayer;
    public List<UnityEvent> ActionStartStepSecondPlayer;

    public List<UnityEvent> ActionDestroyFirstPlayer;
    public List<UnityEvent> ActionDestroySecondPlayer;



    public void Setup() 
    {
        ActionStartStepFirstPlayer = new List<UnityEvent>();
        ActionStartStepSecondPlayer = new List<UnityEvent>();
        ActionDestroyFirstPlayer = new List<UnityEvent>();
        ActionDestroySecondPlayer = new List<UnityEvent>();
    }




    public void AddAction(UnityEvent action, PlayerType playerType) 
    {
        if (playerType == PlayerType.First)
        { 
            ActionStartStepFirstPlayer.Add(action);
        }
        if (playerType == PlayerType.Second)
        {
            ActionStartStepSecondPlayer.Add(action);
        }
    }

    public void RemoveAction(UnityEvent action, PlayerType playerType) 
    {
        if (playerType == PlayerType.First)
        {
            ActionDestroyFirstPlayer.Add(action);
            //ActionStartStepFirstPlayer.Remove(action);
        }
        if (playerType == PlayerType.Second)
        {
            ActionDestroySecondPlayer.Add(action);
            //ActionStartStepSecondPlayer.Add(action);
        }
    }

    public void CallStartStep(PlayerType playerType ) 
    {
        if (playerType == PlayerType.First) 
        {
            foreach(UnityEvent ev in ActionStartStepFirstPlayer) 
            {
                ev?.Invoke();
            }
         
        }
        if( playerType == PlayerType.Second) 
        {
            foreach (UnityEvent ev in ActionStartStepSecondPlayer)
            {
                ev?.Invoke();
            }
        }

        foreach(var action in ActionDestroyFirstPlayer) 
        {
            ActionStartStepFirstPlayer.Remove(action);
        }
        foreach (var action in ActionDestroySecondPlayer)
        {
            ActionStartStepSecondPlayer.Remove(action);
        }

        ActionDestroyFirstPlayer.Clear();
        ActionDestroySecondPlayer.Clear();
    }

}


public enum PlayerType
{
    First,
    Second

}
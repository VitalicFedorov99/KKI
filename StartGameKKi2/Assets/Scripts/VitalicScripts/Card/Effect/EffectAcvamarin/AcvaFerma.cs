using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Core;
public class AcvaFerma : MonoBehaviour
{

    public void AddPower() 
    {
        Game.CurrentPlayer.AddForce(1);
    }
}

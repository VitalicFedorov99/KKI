using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateStateButton : MonoBehaviour
{
    [SerializeField] private GameObject _buttonEndPhase;
    [SerializeField] private GameObject _buttonEndFightPhase;
    [SerializeField] private GameObject _buttonEndDefPhase;


    public void EndPhase() 
    {
        _buttonEndPhase.SetActive(false);
        _buttonEndFightPhase.SetActive(true);
        Debug.Log("Состояние подготовки закончено");
    }

    public void EndFightPhase() 
    {
        _buttonEndFightPhase.SetActive(false);
        _buttonEndDefPhase.SetActive(true);
        Debug.Log("Состояние атаки закончено");
    }

    public void EndDefPhase() 
    {
        _buttonEndDefPhase.SetActive(false);
        _buttonEndPhase.SetActive(true);
        Debug.Log("Состояние защиты закончено");
    }


}

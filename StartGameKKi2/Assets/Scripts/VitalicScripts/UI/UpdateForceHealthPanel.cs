using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateForceHealthPanel : MonoBehaviour
{
    
    [SerializeField] private Text _playerForce;
    [SerializeField] private Text _playerHealth;

    [SerializeField] private Text _enemyForce;
    [SerializeField] private Text _enemyHealth;

    public void UpdateEnemyHealth(int health)
    {
       
        _enemyHealth.text = health.ToString();
    }

    public void UpdateEnemyForce(int force)
    {
        _enemyForce.text = force.ToString();
    }
    public void UpdatePlayerHealth(int health)
    {
        _playerHealth.text = health.ToString();
    }
    public void UpdatePlayerForce(int force)
    {
        Debug.Log("вызвался");
        _playerForce.text = force.ToString();
    }

}

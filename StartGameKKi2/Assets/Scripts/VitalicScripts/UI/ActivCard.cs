using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActivCard : MonoBehaviour
{
    [SerializeField] private List<Image> _imageActiv;
    [SerializeField] private Color _colorOn;
    [SerializeField] private Color _colorOff;

    public void On() 
    {
        foreach( Image img in _imageActiv) 
        {
            img.color = _colorOn;
        }
    }

    public void Off()
    {
        foreach (Image img in _imageActiv)
        {
            img.color = _colorOff;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;
public class UiLineRender : MonoBehaviour
{
    [SerializeField] private UILineConnector _uiLineConnector;
    [SerializeField] private GameObject _endLine;
    [SerializeField] private RectTransform _startLine;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private RectTransform GameObjectRectangle;
    [SerializeField] private Vector2 topRightLocalCoord;
    [SerializeField] private bool _isActiv;


    public void Setup(RectTransform startObj) 
    {
        _isActiv = true;
        _startLine = startObj;
        GameObjectRectangle.gameObject.SetActive(true);
        _uiLineConnector.enabled = true;
        _uiLineConnector.GetComponent<UILineRenderer>().enabled = true;
        
    }


    public void Off() 
    {
        _isActiv = false;
        GameObjectRectangle.gameObject.SetActive(false);
        _uiLineConnector.transforms[0] = _startLine;
        _uiLineConnector.transforms[1] = _startLine;
        _uiLineConnector.enabled = false;
        _uiLineConnector.GetComponent<UILineRenderer>().enabled = false;
    }

    private void Update()
    {

        if (_isActiv)
        {
            _uiLineConnector.transforms[0] = _startLine;
            _uiLineConnector.transforms[1] = GameObjectRectangle;
            Vector2 ScreenCoords = new Vector2(0, 0);
            ScreenCoords.x = Input.mousePosition.x - Screen.width / 2;
            ScreenCoords.y = Input.mousePosition.y - Screen.height / 2;
            GameObjectRectangle.anchoredPosition = new Vector2(ScreenCoords.x + 20, ScreenCoords.y + 20);
        }
        //_uiLineConnector.transform




      
    }

    




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour, IObserver
{
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] Subject _Player;
    public CursorMode cursorMode = CursorMode.Auto;

    private Vector2 cursorHospot;

    public void OnNotify(PlayerActions action)
    {
        if(action == PlayerActions.Morreu)
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }

    void Start()
    {
        cursorHospot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHospot, CursorMode.Auto);
    }

    void OnEnable()
    {
        _Player.addObserver(this);
    }

    void OnDisable()
    {
        _Player.removeObserver(this);
        
    }

}


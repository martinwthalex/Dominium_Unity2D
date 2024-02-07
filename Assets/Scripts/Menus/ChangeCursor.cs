using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D customCursor; // Asigna el cursor personalizado en el inspector.
    [SerializeField] private Vector2 cursorHotspot;

    void Start()
    {
        Cursor.SetCursor(null, cursorHotspot, CursorMode.Auto); // Establece el cursor predeterminado al iniciar.
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Verifica si se est� pulsando el bot�n izquierdo del rat�n.
        {
            Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto); // Cambia el cursor al personalizado mientras el bot�n est� pulsado.
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Restablece el cursor al predeterminado cuando se suelta el bot�n.
        }
    }
}

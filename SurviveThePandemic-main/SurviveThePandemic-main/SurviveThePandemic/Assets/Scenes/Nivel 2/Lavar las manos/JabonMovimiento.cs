using UnityEngine;

public class JabonMovimiento : MonoBehaviour
{
    void Update()
    {
        // Mover el objeto del jabón al seguir el cursor
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }
}

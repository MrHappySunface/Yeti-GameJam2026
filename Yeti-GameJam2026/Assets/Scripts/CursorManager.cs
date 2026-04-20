using UnityEngine;

public class CursorManager2D : MonoBehaviour
{
    public Texture2D hoverCursor;
    public Vector2 hotSpot = Vector2.zero;

    void Update()
    {
        // Shoot a ray from the camera to the mouse position in 2D space
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hit.collider != null)
        {
            Cursor.SetCursor(hoverCursor, hotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
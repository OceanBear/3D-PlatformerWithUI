using UnityEngine;

public class CursorManager : MonoBehaviour
{
    // Start with the cursor locked and hidden
    void Start()
    {
        LockCursor();
    }

    void Update()
    {
        // Toggle cursor state when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                UnlockCursor();
            else
                LockCursor();
        }
        // When the mouse is unlocked and the left mouse button is clicked
        // if the settings menu is not open, the mouse is locked
        if (Cursor.lockState != CursorLockMode.Locked && Input.GetMouseButtonDown(0) && !GameManager.Instance.IsSettingsMenuActive)
        {
            LockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

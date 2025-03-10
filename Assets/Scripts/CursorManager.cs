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

        // When cursor is unlocked, clicking inside the game window re-locks it
        if (Cursor.lockState != CursorLockMode.Locked && Input.GetMouseButtonDown(0))
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

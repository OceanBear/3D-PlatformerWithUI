using UnityEngine;
using UnityEngine.UI;  // If using built-in UI Text; if using TextMeshPro, import TMPro and change the type to TMP_Text

public class DashStatusUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Player player;         // Reference to the Player script
    [SerializeField] private Text dashStatusText;     // Reference to the UI Text component
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the dash status text based on whether dash is available
        if (player.IsDashAvailable)
        {
            dashStatusText.text = "Right click to dash";
        }
        else
        {
            dashStatusText.text = "Dash is cooling down";
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class EulaPopupHandler : MonoBehaviour
{
    public GameObject popupPanel;  // Assign the popup panel in Inspector
    public Button acceptButton;    // Assign the "Accept" button in Inspector
    public Button viewPolicyButton; // Assign the "View Policy" button in Inspector
    public string policyURL = "https://www.pingoos.gg/privacy-policy"; // Set your privacy policy link
    
    void Start()
    {
        // Check if EULA was accepted before
        if (PlayerPrefs.GetInt("eulaAccepted", 0) == 0)
        {
            popupPanel.SetActive(true);  // Show the popup if not accepted
        }
        else
        {
            popupPanel.SetActive(false); // Hide it if already accepted
        }

        // Assign button click events
        acceptButton.onClick.AddListener(AcceptEULA);
        viewPolicyButton.onClick.AddListener(ViewPolicy);
    }

    void AcceptEULA()
    {
        PlayerPrefs.SetInt("eulaAccepted", 1);  // Save acceptance
        PlayerPrefs.Save(); // Ensure data is saved
        popupPanel.SetActive(false); // Hide the popup
    }

    void ViewPolicy()
    {
        Application.OpenURL(policyURL); // Open policy link in browser
    }
}

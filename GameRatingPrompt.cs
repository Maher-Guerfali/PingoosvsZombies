using UnityEngine;
using UnityEngine.UI;

public class GameRatingPrompt : MonoBehaviour
{
    public GameObject ratingPopup; // Assign the rating UI popup in the Inspector
    public Button remindMeLaterButton; // Assign "Remind Me Later" button
    public Button rateNowButton; // Assign "Rate Now" button
    public string appStoreURL = "https://apps.apple.com/app/idYOUR_APP_ID"; // Replace with your actual app link

    void Start()
    {
        int starLiked = PlayerPrefs.GetInt("starLiked", 0); // Get stored value, default is 0
        starLiked++; // Increase counter on game start

        if (starLiked == 3)
        {
            ratingPopup.SetActive(true); // Show popup at 3rd launch
        }
        else if (starLiked >= 20)
        {
            ratingPopup.SetActive(false); // Never show again if 20+
        }

        PlayerPrefs.SetInt("starLiked", starLiked); // Save updated counter
        PlayerPrefs.Save(); // Ensure it's stored

        // Assign button actions
        remindMeLaterButton.onClick.AddListener(RemindMeLater);
        rateNowButton.onClick.AddListener(RateNow);
    }

    void RemindMeLater()
    {
        int starLiked = PlayerPrefs.GetInt("starLiked", 0);
        starLiked += 7; // Add 7 more tries before asking again
        PlayerPrefs.SetInt("starLiked", starLiked);
        PlayerPrefs.Save();
        ratingPopup.SetActive(false); // Hide popup
    }

    void RateNow()
    {
        Application.OpenURL(appStoreURL); // Open App Store link
        PlayerPrefs.SetInt("starLiked", 20); // Prevent future popups
        PlayerPrefs.Save();
        ratingPopup.SetActive(false); // Hide popup
    }
}

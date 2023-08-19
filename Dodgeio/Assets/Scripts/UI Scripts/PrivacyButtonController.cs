using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyButtonController : MonoBehaviour
{

	private const string ADS_PERSONALIZATION_CONSENT = "Ads";
	private const string ANALYTICS_CONSENT = "Analytics";

	public void ClickPrivacyButton()
    {
        SimpleGDPR.ShowDialog(new TermsOfServiceDialog().
                SetTermsOfServiceLink("").
                SetPrivacyPolicyLink("https://graphgames.io/Privacy/"));

    }



    public void ClickPrivacyButtonGDPR()
    {
        StartCoroutine(ShowGDPRConsentDialogAndWait());

    }


	private IEnumerator ShowGDPRConsentDialogAndWait()
	{
		// Show a consent dialog with two sections (and wait for the dialog to be closed):
		// - Ads Personalization: its value can be changed directly from the UI,
		//   result is stored in the ADS_PERSONALIZATION_CONSENT identifier
		// - Unity Analytics: its value can't be changed from the UI since Unity presents its own UI
		//   to toggle Analytics consent. Instead, a button is shown and when the button is clicked,
		//   UnityAnalyticsButtonClicked function is called to present Unity's own UI
		yield return SimpleGDPR.WaitForDialog(new GDPRConsentDialog().
			AddSectionWithToggle(ADS_PERSONALIZATION_CONSENT, "Ads Personalization", "When enabled, you'll see ads that are more relevant to you. Otherwise, you will still receive ads, but they will no longer be tailored toward you.", false).
			AddSectionWithToggle(ANALYTICS_CONSENT, "Analytics", "", false).
			//AddSectionWithButton(UnityAnalyticsButtonClicked, "Unity Analytics", "The collected data allows us to optimize the gameplay and update the game with new enjoyable content. You can see your collected data or change your settings from the dashboard.", "Open Analytics Dashboard").
			AddPrivacyPolicies("https://www.applovin.com/privacy/", "https://www.appsflyer.com/legal/services-privacy-policy/", "https://graphgames.io/Privacy/"));
			

		// Check if user has granted the Ads Personalization permission
		if (SimpleGDPR.GetConsentState(ADS_PERSONALIZATION_CONSENT) == SimpleGDPR.ConsentState.Yes)
		{
			// You can show personalized ads to the user
		}
		else
		{
			// Don't show personalized ads to the user
		}

		// Check if user has granted the Analytics permission
		if (SimpleGDPR.GetConsentState(ANALYTICS_CONSENT) == SimpleGDPR.ConsentState.Yes)
		{
			// Toggle analytics
		}
		else
		{
			// Don't Toggle analytics
		}
	}
}

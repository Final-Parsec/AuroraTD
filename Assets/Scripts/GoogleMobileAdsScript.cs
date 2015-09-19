using System;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System.Collections;

public class GoogleMobileAdsHandler : IInAppPurchaseHandler
{
    private readonly string[] validSkus = { "android.test.purchased" };

    //Will only be sent on a success.
    public void OnInAppPurchaseFinished(IInAppPurchaseResult result)
    {
        result.FinishPurchase();
        GoogleMobileAdsScript.OutputMessage = "Purchase Succeeded! Credit user here.";
    }

    //Check SKU against valid SKUs.
    public bool IsValidPurchase(string sku)
    {
        foreach(string validSku in validSkus) {
            if (sku == validSku) {
                return true;
            }
        }
        return false;
    }

    //Return the app's public key.
    public string AndroidPublicKey
    {
        //In a real app, return public key instead of null.
        get { return null; }
    }
}

// Example script showing how to invoke the Google Mobile Ads Unity plugin.
public class GoogleMobileAdsScript : MonoBehaviour
{

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private static string outputMessage = "";

	public bool disableAds = false;

    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

	void Awake()
	{
	}

    public void RequestBanner()
    {
		if (disableAds)
			return;

		if (bannerView != null)
			bannerView.Destroy ();

        #if UNITY_EDITOR
            string adUnitId = "unused";
        #elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-2481366852569675/8450597146";
        #elif UNITY_IPHONE
            string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);
        // Register for ad events.
        bannerView.AdLoaded += HandleAdLoaded;
        bannerView.AdFailedToLoad += HandleAdFailedToLoad;
        bannerView.AdOpened += HandleAdOpened;
        bannerView.AdClosing += HandleAdClosing;
        bannerView.AdClosed += HandleAdClosed;
        bannerView.AdLeftApplication += HandleAdLeftApplication;
        // Load a banner ad.
        bannerView.LoadAd(createAdRequest());
    }

    public void RequestInterstitial()
    {
		if (disableAds)
			return;

		if (interstitial != null)
			interstitial.Destroy ();

        #if UNITY_EDITOR
            string adUnitId = "unused";
        #elif UNITY_ANDROID
		string adUnitId = "ca-app-pub-2481366852569675/7189346744";
        #elif UNITY_IPHONE
            string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);
        // Register for ad events.
        interstitial.AdLoaded += HandleInterstitialLoaded;
        interstitial.AdFailedToLoad += HandleInterstitialFailedToLoad;
        interstitial.AdOpened += HandleInterstitialOpened;
        interstitial.AdClosing += HandleInterstitialClosing;
        interstitial.AdClosed += HandleInterstitialClosed;
        interstitial.AdLeftApplication += HandleInterstitialLeftApplication;
        GoogleMobileAdsHandler handler = new GoogleMobileAdsHandler();
        interstitial.SetInAppPurchaseHandler(handler);
        // Load an interstitial ad.
        interstitial.LoadAd(createAdRequest());
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest createAdRequest()
    {
        return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddKeyword("game")
                .SetGender(Gender.Male)
                .SetBirthday(new DateTime(1985, 1, 1))
                .TagForChildDirectedTreatment(false)
                .AddExtra("color_bg", "9B30FF")
                .Build();

    }

	public void ShowInterstitial()
    {
		if (disableAds)
			return;

        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

	public void ShowBanner()
	{
		if (disableAds)
			return;

		bannerView.Show ();
	}

	public void HideBanner()
	{
		if (disableAds)
			return;

		bannerView.Hide();
	}

	public void DestroyBanner()
	{
		if (disableAds)
			return;

		bannerView.Destroy();
	}

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
    }

    void HandleAdClosing(object sender, EventArgs args)
    {
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        print("HandleInterstitialOpened event received");
    }

    void HandleInterstitialClosing(object sender, EventArgs args)
    {
        print("HandleInterstitialClosing event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        print("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        print("HandleInterstitialLeftApplication event received");
    }

    #endregion
}

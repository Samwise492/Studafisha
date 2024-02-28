using System;
using Mycom.Target.Unity.Ads;
using UnityEngine;

public class MyTargetAd : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    private UInt32 androidSlotId = 1561649;
    private UInt32 iOSSlotId = 1561649;
  
    private InterstitialAd CreateInterstitialAd()
    {
        UInt32 slotId = 1561649;
#if UNITY_ANDROID
        slotId = androidSlotId;
#elif UNITY_IOS
        slotId = iOSSlotId;
#endif
        // Включение режима отладки
        // InterstitialAd.IsDebugMode = true;

        // Создаем экземпляр InterstitialAd
        return new InterstitialAd(slotId);
    }
    
    private void Start()
    {
        InitAd();
    }
    private void InitAd()
    {
        print("initialisation");
        // Создаем экземпляр InterstitialAd
        interstitialAd = CreateInterstitialAd();
        
        // Устанавливаем обработчики событий
        interstitialAd.AdLoadCompleted += OnLoadCompleted;
        interstitialAd.AdDismissed += OnAdDismissed;
        interstitialAd.AdDisplayed += OnAdDisplayed;
        interstitialAd.AdVideoCompleted += OnAdVideoCompleted;
        interstitialAd.AdClicked += OnAdClicked;
        interstitialAd.AdLoadFailed += OnAdLoadFailed;
        
        // Запускаем загрузку данных
        interstitialAd.Load();
    }
    
    private void OnLoadCompleted(System.Object sender, EventArgs e)
    {
        print("load completed");

        // на отдельной странице
        interstitialAd.Show();
    
        // или в диалоговом окне
        // _interstitialAd.ShowDialog();
    }
    
    private void OnAdDismissed(System.Object sender, EventArgs e)
    {
        print("dismiss");
    }
    
    private void OnAdDisplayed(System.Object sender, EventArgs e)
    {
        print("display!");
    }
    
    private void OnAdVideoCompleted(System.Object sender, EventArgs e)
    {
        print("video completed");
    }
    
    private void OnAdClicked(System.Object sender, EventArgs e)
    {
        print("click");
    }
    
    private void OnAdLoadFailed(System.Object sender, ErrorEventArgs e)
    {
        Debug.Log("OnAdLoadFailed: " + e.Message);
    }
}
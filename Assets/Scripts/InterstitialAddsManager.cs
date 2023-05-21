using UnityEngine;
using UnityEngine.Events;

public sealed class InterstitialAddsManager : MonoBehaviour
{
    public UnityEvent AddClosed;

    public UnityEvent AddLoading;

    [SerializeField] private float _failedRetryDelay;

    [SerializeField] private float _timeout;

    private string _adUnitId = "d9a62d3d22828eee";
    
    private AddsTimer _addsTimer;

    public void LoadInterstitial()
    {
        if (_addsTimer.CanBeShown())
        {
            AddLoading.Invoke();

            MaxSdk.LoadInterstitial(_adUnitId);
        }
    }

    private void Start()
    {
        _addsTimer = new AddsTimer(_timeout);

        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoaded;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailed;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplay;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHidden;
    }

    private void OnInterstitialLoaded(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        MaxSdk.ShowInterstitial(_adUnitId);
    }

    private void OnInterstitialLoadFailed(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        Invoke("LoadInterstitial", _failedRetryDelay);
    }

    private void OnInterstitialHidden(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        AddClosed.Invoke();
    }

    private void OnInterstitialAdFailedToDisplay(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        LoadInterstitial();
    }
}

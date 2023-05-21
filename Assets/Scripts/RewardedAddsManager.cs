using UnityEngine;
using UnityEngine.Events;

public sealed class RewardedAddsManager : MonoBehaviour
{
    public UnityEvent RewardRecived;

    public UnityEvent AddClosed;

    [SerializeField] private float _failedRetryDelay;
    
    private string _adUnitId = "1882960987cccae9";

    public void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(_adUnitId);
    }

    private void Start()
    {
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoaded;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailed;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplay;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedReward;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHidden;
    }

    private void OnRewardedAdLoaded(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        MaxSdk.ShowRewardedAd(_adUnitId);
    }

    private void OnRewardedAdLoadFailed(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        Invoke("LoadRewardedAd", _failedRetryDelay);
    }

    private void OnRewardedAdFailedToDisplay(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        LoadRewardedAd();
    }

    private void OnRewardedAdHidden(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        AddClosed.Invoke();
    }

    private void OnRewardedAdReceivedReward(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        RewardRecived.Invoke();
    }
}

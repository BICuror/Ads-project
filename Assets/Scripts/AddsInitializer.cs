using UnityEngine;

public sealed class AddsInitializer : MonoBehaviour
{
    private void Start()
    {
        MaxSdk.SetSdkKey("KXOe13lVH3tPuQzenHpTHp24XGqGARV16QLmDnezCevXkNNK2iOb61GmAOc3AaBxZnBRPWKh_cgkE8lqbrpVPp");
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
    }
}

using Plugin.AdMob.Configuration;

namespace Plugin.AdMob.Services;

public interface IRewardedInterstitialAdService
{
    IRewardedInterstitialAd CreateAd(string adUnitId = null);

    void PrepareAd(string adUnitId = null, Action<RewardItem> onUserEarnedReward = null);

    void ShowAd();
}

internal class RewardedInterstitialAdService : IRewardedInterstitialAdService
{
    private IRewardedInterstitialAd _rewardedInterstitialAd;
    
    public IRewardedInterstitialAd CreateAd(string adUnitId = null)
    {
        adUnitId = GetAdUnitId(adUnitId);
        
        return new RewardedInterstitialAd(adUnitId);
    }
    
    public void PrepareAd(string adUnitId = null, Action<RewardItem> onUserEarnedReward = null)
    {
        var rewardedInterstitialAd = CreateAd(adUnitId);
        
        if (onUserEarnedReward != null)
        {
            rewardedInterstitialAd.OnUserEarnedReward += (_, reward) => onUserEarnedReward(reward);
        }
        rewardedInterstitialAd.Load();
        
        _rewardedInterstitialAd = rewardedInterstitialAd;
    }
    
    public void ShowAd()
    {
        _rewardedInterstitialAd?.Show();
    }
    
    private string GetAdUnitId(string adUnitId)
    {
        if (AdConfig.UseTestAdUnitIds)
        {
            return AdMobTestAdUnits.RewardedInterstitial;
        }

        return adUnitId ?? AdConfig.DefaultRewardedInterstitialAdUnitId;
    }
}
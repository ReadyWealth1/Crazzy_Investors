mergeInto(LibraryManager.library, {

  PostScore: function (score) {
    window.postScore(score);
  },

  CacheInterstitial: function (adKeyId, package) {
    window.cacheAdMidRoll(UTF8ToString(adKeyId), UTF8ToString(package));
  },

  ShowInterstitial: function (adKeyId, package) {
    window.showAdMidRoll(UTF8ToString(adKeyId), UTF8ToString(package));
  },

  CacheRewarded: function (adKeyId, package) {
    window.cacheAdRewardedVideo(UTF8ToString(adKeyId), UTF8ToString(package));
  },

  ShowRewarded: function (adKeyId, package) {
    window.showAdRewardedVideo(UTF8ToString(adKeyId), UTF8ToString(package));
  },

  GetUserProfile: function () {
    window.getUserProfile();
  },

  LoadBanner: function () {
    window.loadBanner();
  },
  SetTopBanner: function () {
    window.setTopBanner();
  },
  SetBottomBanner: function () {
    window.setBottomBanner();
  },
  ShowBanner: function (adKeyId, package) {
    window.showBanner(UTF8ToString(adKeyId), UTF8ToString(package));
  },
  ShowNativeBanner: function (adKeyId, package) {
    window.showNativeBanner(UTF8ToString(adKeyId), UTF8ToString(package));
  },
  HideBanner: function () {
    window.hideBanner();
  },

});
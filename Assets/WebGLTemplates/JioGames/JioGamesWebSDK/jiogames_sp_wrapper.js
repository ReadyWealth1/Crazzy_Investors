
function postScore(score) {
    console.log("JioGames: postScore() ",score);
    
    if  (window.DroidHandler) {
        window.DroidHandler.postScore(score);
    }
}

function cacheAdMidRoll(adKeyId, source) {
    if(!adKeyId || !source){
        adKeyId? null: (console.log("JioGames: cacheAdMidRoll() no adKeyId to cacheAd ",adKeyId));
        source? null : (console.log("JioGames: cacheAdMidRoll() no source to cacheAd ",source));
        return;
    }
    else{
        console.log("JioGames: cacheAdMidRoll() adKeyId : " + adKeyId + " source : " + source);
    }
    if (window.DroidHandler) {
        window.DroidHandler.cacheAd(adKeyId, source);
    }
}

function showAdMidRoll(adKeyId, source) {
    if(!adKeyId || !source){
        adKeyId? null: (console.log("JioGames: showAdMidRoll() no adKeyId to showAd ",adKeyId));
        source? null : (console.log("JioGames: showAdMidRoll() no source to showAd ",source));
        return;
    }
    else{
        console.log("JioGames: showAdMidRoll() adKeyId : " + adKeyId + " source : " + source);
    }
    if (window.DroidHandler) {
        window.DroidHandler.showAd(adKeyId, source);
    }
}

function cacheAdRewardedVideo(adKeyId, source) {
    if (!adKeyId || !source) {
        adKeyId ? null : (console.log("JioGames: cacheAdRewardedVideo() no adKeyId to cacheAd ", adKeyId));
        source ? null : (console.log("JioGames: cacheAdRewardedVideo() no source to cacheAd ", source));
        return;
    }
    else{
        console.log("JioGames: cacheAdRewardedVideo() adKeyId : " + adKeyId + " source : " + source);
    }
    if (window.DroidHandler) {
        window.DroidHandler.cacheAdRewarded(adKeyId, source);    
    }
}

function showAdRewardedVideo(adKeyId, source) {
    if (!adKeyId || !source) {
        adKeyId ? null : (console.log("JioGames: showAdRewardedVideo() no adKeyId to showAd ", adKeyId));
        source ? null : (console.log("JioGames: showAdRewardedVideo() no source to showAd ", source));
        return;
    }
    else{
        console.log("JioGames: showAdRewardedVideo() adKeyId : " + adKeyId + " source : " + source);
    }
    if (window.DroidHandler) {
        window.DroidHandler.showAdRewarded(adKeyId, source);
    }
}

function getUserProfile() {
    console.log("JioGames: getUserProfile called");
    if (window.DroidHandler) {
        window.DroidHandler.getUserProfile();
    }
}

window.onAdPrepared = function (adSpotKey) {
    console.log("JioGames: onAdPrepared "+adSpotKey.toString());
    unityInstance.SendMessage('JioWrapperJS', 'onAdPrepared', adSpotKey);
};
window.onAdClosed = function (data, pIsVideoCompleted, pIsEligibleForReward) {
    console.log("JioGames: onAdClosed data : "+data.toString(), "pIsVideoCompleted : "+pIsVideoCompleted+" pIsEligibleForReward : "+pIsEligibleForReward);
    var localData = data + "|" + pIsVideoCompleted + "|" + pIsEligibleForReward;
    unityInstance.SendMessage('JioWrapperJS', 'onAdClosed', localData);
};
window.onAdFailedToLoad = function (data, pDescription){
    console.log("JioGames: onAdFailedToLoad data : "+data.toString()+" pDescription : "+pDescription);
    var localData = data + "|" + pDescription;
    unityInstance.SendMessage('JioWrapperJS', 'onAdFailedToLoad', localData);
};

window.onAdReady = function (adSpotKey) { };
window.onAdClose = function (adSpotKey) { };
window.onAdMediaEnd = function (data, pSuccess, pValue) { };
window.onAdClick = function (adSpotKey) {};
window.onAdMediaCollapse = function (adSpotKey) {};
window.onAdMediaExpand = function (adSpotKey) {};
window.onAdMediaStart = function (adSpotKey) {};
window.onAdRefresh = function (adSpotKey) {};
window.onAdRender = function (adSpotKey) {};
window.onAdRender = function (adSpotKey) {};
window.onAdReceived = function (adSpotKey) {};
window.onAdSkippable = function (adSpotKey) {};
window.onAdView = function (adSpotKey) {};

window.onUserProfileResponse = function(message)
{
   console.log("JioGames: onUserProfileResponse "+[JSON.stringify(message)]);
   unityInstance.SendMessage('JioWrapperJS', 'onUserProfileResponse', JSON.stringify(message));
};

window.onClientPause = function () {
    console.log("JioGames: onClientPause called");
    unityInstance.SendMessage('JioWrapperJS', 'onClientPause');  // Set the timescale to zero 
};

window.onClientResume = function () {
    console.log("JioGames: onClientResume called");
    unityInstance.SendMessage('JioWrapperJS', 'onClientResume');  // Set the timescale to zero 
};

// Callback received whenever the Jio app is sent to background or brought to foreground
document.addEventListener("visibilitychange", function() {
    if (document.visibilityState === 'visible') {
       console.log("JioGames: App Visible");
       unityInstance.SendMessage('JioWrapperJS', 'ResumeGameSound');
    } else {  
       console.log("JioGames: App Hidden");
       unityInstance.SendMessage('JioWrapperJS', 'PauseGameSound');
    }
});


console.log("JioGames: SDK initialize : 1.0.0");

// Banner ad impliment code
function loadBanner() {
    console.log("JioGames: loadBanner called");
	if (window.DroidHandler) {
        window.DroidHandler.postMessage('{"key":"getUserProperties"}')
    }
    else{
        window.onUserPropertiesResponse('{"detail":{"uid":"","ifa":""}}');
    }
}

window.onUserPropertiesResponse = function(message)
{
    console.log("JioGames: onUserPropertiesResponse "+message);
    const obj = JSON.parse(JSON.stringify(message));
	
    var element = document.createElement("div");
    element.id = 'bannercontainer';	
    element.style.position = 'absolute';
    element.style.width = 'fit-content';
    element.style.height = '50';
    element.style.left = '50%';
    element.style.top = '100%';
    element.style.backgroundPosition = 'center center';
    element.style.backgroundRepeat = 'no-repeat';
    element.style.transform = 'translate(-50%, -100%)';
	
    document.body.appendChild(element);
    
    var script = document.createElement('script');
    script.src = 'https://jioadsweb.akamaized.net/jioads/websdk/default/stable/v2/jioAds.js';

    script.onload = () => {
        callback_Banner();
        banner_Configuration(obj);
    };

    script.onerror = () => {
        console.log('Error occurred while loading script');
    };

    document.body.appendChild(script);
}

function setTopBanner(){
    console.log("JioGames: setTopBanner");
    var element = document.getElementById('bannercontainer');
    element.style.top = '0%';
	element.style.transform = 'translate(-50%, -0%)';
}

function setBottomBanner(){
    console.log("JioGames: setBottomBanner");
    var element = document.getElementById('bannercontainer');
    element.style.top = '100%'
	element.style.transform = 'translate(-50%, -100%)';
}

function banner_Configuration(obj){
    console.log("JioGames: banner_Configuration IFA : " , obj.detail.ifa);
    console.log("JioGames: banner_Configuration UID : " , obj.detail.uid);
    JioAds.setConfiguration({
        endpoint: "jioads",
        clkSelf: true,
        reqType: "prod", //stg, prod
        logLevel: 1,
		ifa: obj.detail.ifa,
		uid: obj.detail.uid,
        adRequestTimeout: 6000,
        adRenderingTimeout: 5000
    });
}

function showBanner(adKeyId, source) {
    console.log("JioGames: showBanner : " + adKeyId + " : " + source);
    document.getElementById("bannercontainer").innerHTML = `<ins id="uid1" data-adspot-key=${adKeyId} data-source=${source} data-ad-sizes="320x50"></ins>`;
}

function showNativeBanner(adKeyId, source) {
    console.log("JioGames: showNativeBanner : " + adKeyId + " : " + source);
    document.getElementById("bannercontainer").innerHTML = `<ins id="uid1" data-adspot-key=${adKeyId} data-source=${source} data-ad-sizes="300x250"></ins>`;
    
    var element = document.getElementById('bannercontainer');
    element.style.top = '50%'
	element.style.transform = 'translate(-50%, -50%)';
}

function hideBanner() {
    console.log("JioGames: hideBanner");
    document.getElementById("bannercontainer").innerHTML = '';
}

function callback_Banner(){
    JioAds.onAdFailedToLoad = function(placementId, options) {
        console.log ("JioGames: onAdFailedToLoad "+placementId+" options "+JSON.stringify(options));
    };
    JioAds.onAdPrepared = function(placementId, adUxType) {
        console.log ("JioGames: onAdPrepared "+placemenId);
    };
    JioAds.onAdRender = function(placementId) {
        console.log ("JioGames: onAdRender "+placementId);
    };
    JioAds.onAdChange = function(placementId, options) {
        console.log ("JioGames: onAdChange "+placementId);
    };
    JioAds.onAdClosed = function(placementId, isVideoCompleted, reward) {
        console.log ("JioGames: onAdClosed "+placementId);
    };
    JioAds.onAdClicked = function(placementId, url) {
        console.log ("JioGames: onAdClicked "+placementId + " URL : " +url);
        window.DroidHandler.postMessage('{"key":"openLink","value":{"url":"' + url + '"}}')
    };
    JioAds.onAdMediaStart = function(placementId) {
        console.log ("JioGames: onAdMediaStart "+placementId);
    };
    JioAds.onAdProgress = function(placementId, quartileInfo) {
        console.log ("JioGames: onAdProgress "+placementId);
    };
    JioAds.onAdMediaEnd = function(placementId, reward) {
        console.log ("JioGames: onAdMediaEnd "+placementId);
    };
    JioAds.onAdRefresh = function(placementId, options) {
        console.log ("JioGames: onAdRefresh "+placementId);
    };
    JioAds.onAdSkippable = function(placementId, options) {
        console.log ("JioGames: onAdSkippable "+placementId);
    };
    JioAds.onAdsReceived = function(placementId, ads) {
        console.log ("JioGames: onAdsReceived "+placementId);
    };
    JioAds.onAdDuration = function(placementId, adDuration) {
        console.log ("JioGames: onAdDuration "+placementId);
    };
}

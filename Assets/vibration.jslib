mergeInto(LibraryManager.library, {
  TriggerVibration: function (duration) {
    if (navigator.vibrate) {
      navigator.vibrate(duration);
    } else {
      console.log("Vibration not supported.");
    }
  }
});

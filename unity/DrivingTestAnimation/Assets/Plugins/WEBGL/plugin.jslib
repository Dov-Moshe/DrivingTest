// File: MyPlugin.jslib

mergeInto(LibraryManager.library, {
  TestDone: function (score, summary) {
    window.dispatchReactUnityEvent(
      "TestDone",
      score,
      Pointer_stringify(summary)
    );
  },

  TestStart: function () {
    window.dispatchReactUnityEvent(
      "TestStart"
    );
  },
});
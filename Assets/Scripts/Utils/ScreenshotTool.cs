#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class ScreenshotTool
{
    private static string SAVE_FORMAT = "{0}.png";

    [MenuItem("EasyGames/ASO/Take screenshot")]
    public static void TakeScreenshot()
    {
        //TODO: Create directory if not exist
        ScreenCapture.CaptureScreenshot(string.Format(SAVE_FORMAT, Time.time), 2);
    }
}
#endif
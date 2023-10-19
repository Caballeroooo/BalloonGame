using UnityEngine;

public static class PlayerPrefsProvider
{
    private const string PlayerGuidKey = "PlayerGUID";
    private const string PlayerNickname = "PlayerNickname";
    private const string PlayerBestScoreKey = "BestScore";
    private const string SlashTutorialShownKey = "SlashTutorialShown";
    private const string MegaTapTutorialShownKey = "MegaTapTutorialShown";

    public static string GetPlayerGuid()
    {
        return PlayerPrefs.GetString(PlayerGuidKey);
    }

    public static void SetPlayerGuid(string guid)
    {
        PlayerPrefs.SetString(PlayerGuidKey, guid);
    }

    public static string GetPlayerNickname()
    {
        return PlayerPrefs.GetString(PlayerNickname);
    }

    public static void SetPlayerNickname(string nickname)
    {
        PlayerPrefs.SetString(PlayerNickname, nickname);
    }

    public static int GetPlayerBestScore()
    {
        return PlayerPrefs.GetInt(PlayerBestScoreKey, 0);
    }

    public static void SetPlayerBestScore(int bestScore)
    {
        PlayerPrefs.SetInt(PlayerBestScoreKey, bestScore);
    }

    public static string GetSlashTutorialShown()
    {
        return PlayerPrefs.GetString(SlashTutorialShownKey);
    }

    public static void SetSlashTutorialShown()
    {
        PlayerPrefs.SetString(SlashTutorialShownKey, "Shown");
    }

    public static string GetMegaTapTutorialShown()
    {
        return PlayerPrefs.GetString(MegaTapTutorialShownKey);
    }

    public static void SetMegaTapTutorialShown()
    {
        PlayerPrefs.SetString(MegaTapTutorialShownKey, "Shown");
    }
}

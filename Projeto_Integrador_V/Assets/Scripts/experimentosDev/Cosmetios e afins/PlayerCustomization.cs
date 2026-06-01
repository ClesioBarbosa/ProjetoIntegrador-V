using UnityEngine;

public static class PlayerCustomization
{
    public static int SelectedHat = 0;
    public static int SelectedFish = 0;

    public static void Save()
    {
        PlayerPrefs.SetInt("SelectedHat", SelectedHat);
        PlayerPrefs.SetInt("SelectedFish", SelectedFish);
        PlayerPrefs.Save();
    }

    public static void Load()
    {
        SelectedHat =
            PlayerPrefs.GetInt("SelectedHat", 0);

        SelectedFish =
            PlayerPrefs.GetInt("SelectedFish", 0);
    }
}
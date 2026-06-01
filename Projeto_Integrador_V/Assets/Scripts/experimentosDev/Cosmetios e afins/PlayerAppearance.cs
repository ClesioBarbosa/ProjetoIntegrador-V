using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    [Header("Modelos do Peixe")]
    public GameObject[] fishModels;
    public CosmeticDatabase database;

    public Transform fishAnchor;
    public Transform hatAnchor;

    void Start()
    {
        PlayerCustomization.Load();

        int fish =
            PlayerCustomization.SelectedFish;

        int hat =
            PlayerCustomization.SelectedHat;

        GameObject fishObj =
            Instantiate(
                database.fishes[fish].fishPrefab,
                fishAnchor
            );

        fishObj.transform.localPosition =
            Vector3.zero;

        fishObj.transform.localRotation =
            Quaternion.identity;

        GameObject hatObj =
            Instantiate(
                database.hats[hat].hatPrefab,
                hatAnchor
            );

        hatObj.transform.localPosition =
            Vector3.zero;

        hatObj.transform.localRotation =
            Quaternion.identity;
    }
}
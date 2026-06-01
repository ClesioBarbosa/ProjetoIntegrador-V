using TMPro;
using UnityEngine;

public class DressingRoomManager : MonoBehaviour
{
    [Header("Banco de Dados")]
    public CosmeticDatabase database;

    [Header("Preview dos Peixes")]
    public GameObject[] fishPreviewModels;

    [Header("Preview dos Chapéus")]
    public Transform hatPreviewAnchor;

    [Header("Textos")]
    public TMP_Text fishNameText;
    public TMP_Text hatNameText;

    private int currentFishIndex;
    private int currentHatIndex;

    private GameObject currentHatPreview;

    void Start()
    {
        PlayerCustomization.Load();

        currentFishIndex = PlayerCustomization.SelectedFish;
        currentHatIndex = PlayerCustomization.SelectedHat;

        RefreshFish();
        RefreshHat();
    }

    // =========================
    // PEIXES
    // =========================

    void RefreshFish()
    {
        for (int i = 0; i < fishPreviewModels.Length; i++)
        {
            fishPreviewModels[i].SetActive(
                i == currentFishIndex
            );
        }

        if (fishNameText != null)
        {
            fishNameText.text =
                database.fishes[currentFishIndex].fishName;
        }
    }

    public void NextFish()
    {
        currentFishIndex++;

        if (currentFishIndex >= fishPreviewModels.Length)
            currentFishIndex = 0;

        RefreshFish();
    }

    public void PreviousFish()
    {
        currentFishIndex--;

        if (currentFishIndex < 0)
            currentFishIndex =
                fishPreviewModels.Length - 1;

        RefreshFish();
    }

    // =========================
    // CHAPÉUS
    // =========================

    void RefreshHat()
    {
      if (currentHatPreview != null && hatPreviewAnchor != null)
    {
        // Define o hatPreviewAnchor como o novo pai do chapéu
        currentHatPreview.transform.SetParent(hatPreviewAnchor);

        // Zera a posição/rotação RELATIVA ao pai (ou seja, vai direto para o Anchor)
        currentHatPreview.transform.localPosition = Vector3.zero;
        currentHatPreview.transform.localRotation = Quaternion.identity;
        currentHatPreview.transform.localScale = Vector3.one;
    }
    }

    public void NextHat()
    {
        currentHatIndex++;

        if (currentHatIndex >= database.hats.Count)
            currentHatIndex = 0;

        RefreshHat();
    }

    public void PreviousHat()
    {
        currentHatIndex--;

        if (currentHatIndex < 0)
            currentHatIndex =
                database.hats.Count - 1;

        RefreshHat();
    }

    // =========================
    // CONFIRMAR
    // =========================

    public void ConfirmSelection()
    {
        PlayerCustomization.SelectedFish =
            currentFishIndex;

        PlayerCustomization.SelectedHat =
            currentHatIndex;

        PlayerCustomization.Save();

        Debug.Log(
            "Salvo: " +
            database.fishes[currentFishIndex].fishName +
            " | " +
            database.hats[currentHatIndex].hatName
        );
    }
}
using System.Collections.Generic;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    [System.Serializable]
    public class HatData
    {
        public string hatName;
        public GameObject hatPrefab;
    }

    [Header("Lista de Chapeus")]
    public List<HatData> hats = new List<HatData>();

    [Header("Ponto de Encaixe")]
    public Transform hatAnchor;

    // Dados acessíveis em qualquer cena
    public static int equippedHatIndex = -1;
    public static string equippedHatName = "";

    private GameObject currentHat;

    void Start()
    {
        if (equippedHatIndex >= 0)
        {
            EquipHat(equippedHatIndex);
        }
    }

    public void EquipHat(int index)
    {
        if (index < 0 || index >= hats.Count)
            return;

        if (currentHat != null)
            Destroy(currentHat);

        currentHat = Instantiate(
            hats[index].hatPrefab,
            hatAnchor
        );

        currentHat.transform.localPosition = Vector3.zero;
        currentHat.transform.localRotation = Quaternion.identity;
        currentHat.transform.localScale = Vector3.one;

        equippedHatIndex = index;
        equippedHatName = hats[index].hatName;

        PlayerPrefs.SetInt("SelectedHat", index);
        PlayerPrefs.Save();
    }

    public void EquipHat(string hatName)
    {
        for (int i = 0; i < hats.Count; i++)
        {
            if (hats[i].hatName == hatName)
            {
                EquipHat(i);
                return;
            }
        }
    }

    public void RemoveHat()
    {
        if (currentHat != null)
            Destroy(currentHat);

        equippedHatIndex = -1;
        equippedHatName = "";

        PlayerPrefs.DeleteKey("SelectedHat");
    }

    public void LoadSavedHat()
    {
        if (PlayerPrefs.HasKey("SelectedHat"))
        {
            EquipHat(PlayerPrefs.GetInt("SelectedHat"));
        }
    }
}
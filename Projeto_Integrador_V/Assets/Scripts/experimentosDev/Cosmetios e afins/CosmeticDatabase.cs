using System.Collections.Generic;
using UnityEngine;

public class CosmeticDatabase : MonoBehaviour
{
    [System.Serializable]
    public class HatData
    {
        public string hatName;
        public GameObject hatPrefab;
    }

    [System.Serializable]
    public class FishData
    {
    public GameObject fishPrefab;
    public string fishName;
    }

    [Header("Chapéus")]
    public List<HatData> hats = new();

    [Header("Peixes")]
    public List<FishData> fishes = new();
}
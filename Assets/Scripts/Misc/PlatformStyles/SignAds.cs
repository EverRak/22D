using UnityEngine;
using UnityEngine.UI;

public class SignAds : MonoBehaviour
{
    [SerializeField] private Image SignAd;

    private void Awake()
    {
        Sprite[] ads = Resources.LoadAll<Sprite>("Ads");

        SignAd.sprite = ads[Random.Range(0, ads.Length)];
    }
}

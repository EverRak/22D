using UnityEngine;

public class PrefsSaveUtil : MonoBehaviour
{
    [SerializeField] private string PrefsSaveString;

    public void SaveInt(int value) => PlayerPrefs.SetInt(PrefsSaveString, value);
    public void SaveFloat(float value) => PlayerPrefs.SetFloat(PrefsSaveString, value);
    public void SaveString(string value) => PlayerPrefs.SetString(PrefsSaveString, value);
}
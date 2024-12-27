using UnityEngine;

public class PrefsToTextUtil : MonoBehaviour
{
    [SerializeField] private string PrefsString;

    public enum PrefsType
    {
        FLOAT,
        INTEGER,
        STRING,
        BOOLEAN
    }

    [SerializeField] private PrefsType ValueType;

    [SerializeField] private TranslationIntermediate TargetTranslator;
    
    private void Update()
    {
        object value = 0;

        switch (ValueType)
        {
            case PrefsType.FLOAT:
                value = PlayerPrefs.GetFloat(PrefsString);
                break;
            case PrefsType.INTEGER:
                value = PlayerPrefs.GetInt(PrefsString);
                break;
            case PrefsType.STRING:
                value = PlayerPrefs.GetString(PrefsString);
                break;
            case PrefsType.BOOLEAN:
                value = GameManager.GetPrefsBool(PrefsString, false);
                break;
        }

        TargetTranslator.Translate(value);
    }
}

using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    // Game Objects
    [SerializeField] private GameObject JumpButton;
    [SerializeField] private GameObject ScreenJumpButton;
    [SerializeField] private GameObject JumpButtonToggle;
    [SerializeField] private GameObject PauseScreen;

    // Translators
    [SerializeField] private TranslationIntermediate RecordTranslator;
    [SerializeField] private TranslationIntermediate LastScoreTranslator;
    [SerializeField] private TranslationIntermediate PointsTranslator;

    // Texts
    [SerializeField] private TMPro.TMP_Text VersionText;

    // Dropdowns
    [SerializeField] private TMPro.TMP_Dropdown LanguageDD;

    [SerializeField] private AudioMixer MainMixer;
    
    [SerializeField] private ToggleUtil SoundToggle;

    // [SerializeField] private float DayNightCycle = 30;

    // private bool IsNight;
    private bool Paused;

    private void SaveRecord()
    {
        // Local variable for points.
        float points = FindObjectOfType<PlayerMovement>().Points;

        // Save the last score.
        PlayerPrefs.SetFloat("Last", points);

        PlayerPrefs.SetFloat("AllTimePoints", PlayerPrefs.GetFloat("AllTimePoints") + points);

        // Add current points to the saved total.
        PlayerPrefs.SetInt("Points", PlayerPrefs.GetInt("Points") + (int)points);

        // Check if we already saved a record,
        if (PlayerPrefs.HasKey("Record"))
            // If the saved record is greater or equal than the current points,
            if (PlayerPrefs.GetFloat("Record") >= points)
                // Just return, no need to continue.
                return;

        // Save a record with the current points.
        PlayerPrefs.SetFloat("Record", points);
    }

    public void Die()
    {
        PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths", 0) + 1);
        SaveRecord();
    }

    // private void ToggleDayNight()
    // {
    //     IsNight = !IsNight;

    //     BGAnimator.Play(IsNight ? "NightFade" : "DayFade");

    //     Invoke("ToggleDayNight", DayNightCycle);
    // }
    
    private void Update()
    {
        PauseScreen.SetActive(Paused);

        Time.timeScale = Paused ? 0 : 1;

        if (Input.GetKeyDown(KeyCode.M))
            SoundToggle.Toggle();

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        Paused = !Paused;
    }

    public void Mute(bool state)
    {
        MainMixer.SetFloat("MainVolume", state ? Mathf.Log10(1) * 20 : Mathf.Log10(0.001f) * 20);
    }

    private void Start()
    {
        // Invoke("ToggleDayNight", DayNightCycle);

        PointsTranslator.Translate(PlayerPrefs.GetInt("Points"));
        
        bool isMobile = Application.platform == RuntimePlatform.Android;

        JumpButton.SetActive(isMobile);
        JumpButtonToggle.SetActive(isMobile);

        MainMixer.SetFloat("MainVolume", GetPrefsBool("SoundsEnabled", false) ? Mathf.Log10(0.001f) * 20 : Mathf.Log10(1) * 20);

        // Set the record text based on the saved value.
        RecordTranslator.Translate(PlayerPrefs.GetFloat("Record"));
        // RecordText.text = $"Record: {PlayerPrefs.GetFloat("Record")}";

        LastScoreTranslator.Translate(PlayerPrefs.GetFloat("Last"));

        // Set the version text to the application version followed by the unity version.
        VersionText.text = $"Too 2D - {Application.version} Unity {Application.unityVersion}";

        // Check if the jump button should be
        bool screenJump = GetPrefsBool("ScreenJump", false);
        // Full screen or
        JumpButton.SetActive(!screenJump);
        // "Physical"
        ScreenJumpButton.SetActive(screenJump);

        // Set the language dropdown (DD) index to the saved value.
        LanguageDD.SetValueWithoutNotify(PlayerPrefs.GetInt("Language", 0));
    }

    public void SetLanguageIndex(int index)
    {
        // Save the selected language index.
        PlayerPrefs.SetInt("Language", index);
    }

    // Get a boolean using PlayerPrefs.
    public static bool GetPrefsBool(string _name, bool _default) => PlayerPrefs.GetInt(_name, _default ? 1 : 0) == 1;

    // Save a boolean using PlayerPrefs.
    public static void StorePrefsBool(bool _value, string _name) => PlayerPrefs.SetInt(_name, _value ? 1 : 0);

    // This causes some problems so I'll just delete it :clown
//     // Option to erase the saved record.
//     public void EraseRecords()
//     {
//         // If we ever saved a record,
//         if (PlayerPrefs.HasKey("Record"))
//             // Erase it.
//             PlayerPrefs.DeleteKey("Record");
//     }
}
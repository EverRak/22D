using UnityEngine.UI;
using UnityEngine;

public class BoolUnlockable : MonoBehaviour
{
    [SerializeField] private bool AlwaysCheck;
    
    [SerializeField] private string SaveString;
    
    [SerializeField] private Button ThisButton;

    [SerializeField] private GameObject LockedFilter;

    private void Update()
    {
        if (AlwaysCheck)
            Awake();
    }

    private void Awake()
    {
        bool unlocked = GameManager.GetPrefsBool(SaveString, false);

        if (ThisButton)
            ThisButton.interactable = unlocked;
            
        if (LockedFilter)
            LockedFilter.SetActive(!unlocked);
    }
}
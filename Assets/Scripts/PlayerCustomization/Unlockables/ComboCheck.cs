using System;
using UnityEngine;

public class ComboCheck : MonoBehaviour
{
    [SerializeField] private Combo[] Combos;

    private void Update()
    {
        foreach (Combo c in Combos)
        {
            if (GameManager.GetPrefsBool(c.Name, false))
                continue;

            bool equal = true;

            foreach (var unlockableType in Enum.GetValues(typeof(UnlockableItem.UnlockableType)))
            {                
                int i = (int)unlockableType;

                string itemName = PlayerPrefs.GetString(CustomizationManager.SaveStrings[i]);

                // This will check this:
                // Eyes from combo = Monocle
                // Eyes from Prefs = Monocle?
                // Head from combo = TopHat
                // Head from Prefs = TopHat?
                // Face from combo = EvilMustache
                // Face from Prefs = EvilMustache?

                if (!string.IsNullOrEmpty(c.ItemsName[i]))
                    if (itemName != c.ItemsName[i])
                        equal = false;
            }

            GameManager.StorePrefsBool(equal, c.Name);
        }
    }
}

[Serializable]
public class Combo
{
    public string Name;

    //!
    //!
    //!
    //!
    //!
    //!
    //!
    //? Change this if I add more types.
    [Tooltip("Following the item type order: Eye, Head, Face.")]
    public string[] ItemsName;
}
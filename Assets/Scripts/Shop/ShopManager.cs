using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Animator ToastAnimator;
    [SerializeField] private Animator BuyWidgetAnimator;
    [SerializeField] private Animator PlayerAnimator;

    [SerializeField] private TranslationIntermediate PointsTranslator;

    [SerializeField] private TMPro.TMP_Text ValueText;

    private string CurrentSaveString;

    private int CurrentValue;

    private void Update()
    {
        PointsTranslator.Translate(PlayerPrefs.GetInt("Points").ToString());
    }

    public void PreviewAnimation(int ID)
    {
        PlayerAnimator.Play("Walk");
        PlayerAnimator.SetInteger("WalkingIndex", ID);
    }

    public void RequestBuy(string buyString)
    {
        // buyString = "saveString:value" 👍
        string[] strings = buyString.Split(':');
        CurrentSaveString = strings[0];
        CurrentValue = int.Parse(strings[1]);

        BuyWidgetAnimator.Play("FadeIn");
        ValueText.text = $"({CurrentValue})";
    }

    public void ConfirmBuy()
    {
        if (PlayerPrefs.GetInt("Points") >= CurrentValue)
        {
            GameManager.StorePrefsBool(true, CurrentSaveString);
            int points = PlayerPrefs.GetInt("Points");
            PlayerPrefs.SetInt("Points", points - CurrentValue);
        }
        else
        {
            ToastAnimator.Play("FadeIn");
        }
    }
}

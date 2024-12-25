using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    [SerializeField] private UnlockableItem[] EyeItems;
    [SerializeField] private UnlockableItem[] HeadItems;
    [SerializeField] private UnlockableItem[] FaceItems;
    [SerializeField] private TrailRenderer[] Trails;

    [SerializeField] private SpriteRenderer EyeEquip, HeadEquip, FaceEquip;

    [SerializeField] private TrailRenderer PlayerTrail;

    [SerializeField] private Animator PlayerAnimator;

    [SerializeField] private TranslationIntermediate RecordTranslator;

    private void Start()
    {
        if (RecordTranslator)
            RecordTranslator.Translate(PlayerPrefs.GetFloat("Record"));

        if (!PlayerPrefs.HasKey("HeadEquip"))
            PlayerPrefs.SetInt("HeadEquip", -1);

        if (!PlayerPrefs.HasKey("EyeEquip"))
            PlayerPrefs.SetInt("EyeEquip", -1);

        if (!PlayerPrefs.HasKey("FaceEquip"))
            PlayerPrefs.SetInt("FaceEquip", -1);

        if (!PlayerPrefs.HasKey("TrailEquip"))
            PlayerPrefs.SetInt("TrailEquip", -1);

        if (!PlayerPrefs.HasKey("WalkingAnim"))
            PlayerPrefs.SetInt("WalkingAnim", 0);

        if (!PlayerPrefs.HasKey("IdleAnim"))
            PlayerPrefs.SetInt("IdleAnim", 0);

        EquipHead(PlayerPrefs.GetInt("HeadEquip"));
        EquipEye(PlayerPrefs.GetInt("EyeEquip"));
        EquipFace(PlayerPrefs.GetInt("FaceEquip"));
        EquipTrail(PlayerPrefs.GetInt("TrailEquip"));

        PlayerAnimator.SetInteger("WalkingIndex", PlayerPrefs.GetInt("WalkingAnim"));
        PlayerAnimator.SetInteger("IdleIndex", PlayerPrefs.GetInt("IdleAnim"));

        PlayerAnimator.Play(PlayerPrefs.GetString("LastUsedAnimation", "Idle"));
    }

    // TODO: Maybe make this methods into only 1 (with multi-functionality)

    public void EquipEye(int itemID)
    {
        if (itemID == -1)
        {
            EyeEquip.sprite = null;
            EyeEquip.transform.localScale = Vector2.one;
            EyeEquip.transform.localPosition = Vector2.zero;
            PlayerPrefs.SetInt("EyeEquip", -1);
            return;
        }

        UnlockableItem item = EyeItems[itemID];

        EyeEquip.sprite = item.ThisSprite;
        EyeEquip.transform.localScale = item.Size;
        EyeEquip.transform.localPosition = item.Offset;
        PlayerPrefs.SetInt("EyeEquip", itemID);
    }

    public void EquipHead(int itemID)
    {
        if (itemID == -1)
        {
            HeadEquip.sprite = null;
            HeadEquip.transform.localScale = Vector2.one;
            HeadEquip.transform.localPosition = Vector2.zero;
            PlayerPrefs.SetInt("HeadEquip", -1);
            return;
        }

        UnlockableItem item = HeadItems[itemID];
        
        HeadEquip.sprite = item.ThisSprite;
        HeadEquip.transform.localScale = item.Size;
        HeadEquip.transform.localPosition = item.Offset;
        PlayerPrefs.SetInt("HeadEquip", itemID);
    }

    public void EquipFace(int itemID)
    {
        if (itemID == -1)
        {
            FaceEquip.sprite = null;
            FaceEquip.transform.localScale = Vector2.one;
            FaceEquip.transform.localPosition = Vector2.zero;
            PlayerPrefs.SetInt("FaceEquip", -1);
            return;
        }

        UnlockableItem item = FaceItems[itemID];
        
        FaceEquip.sprite = item.ThisSprite;
        FaceEquip.transform.localScale = item.Size;
        FaceEquip.transform.localPosition = item.Offset;
        PlayerPrefs.SetInt("FaceEquip", itemID);
    }

    public void EquipTrail(int itemID)
    {
        if (itemID == -1)
        {
            PlayerTrail.enabled = false;
            PlayerPrefs.SetInt("TrailEquip", -1);
            return;
        }

        PlayerTrail.enabled = true;

        CopyTrailValues(Trails[itemID], PlayerTrail);

        PlayerPrefs.SetInt("TrailEquip", itemID);
    }

    // This is used to set the last defined animation in the customization menu, so in every other screen, the animation that will play is that from the last selected category.
    public void SetLastUsedAnimation(string animationBaseName)
    {
        PlayerPrefs.SetString("LastUsedAnimation", animationBaseName);
    }

    public void SetWalkingAnimation(int ID)
    {
        PlayerPrefs.SetInt("WalkingAnim", ID);
        PlayerAnimator.Play("Walk");
        PlayerAnimator.SetInteger("WalkingIndex", ID);
    }

    public void SetIdleAnimation(int ID)
    {
        PlayerPrefs.SetInt("IdleAnim", ID);
        PlayerAnimator.Play("Idle");
        PlayerAnimator.SetInteger("IdleIndex", ID);
    }

    private void CopyTrailValues(TrailRenderer source, TrailRenderer destination)
    {
        // destination.startWidth = source.startWidth;
        // destination.endWidth = source.endWidth;
        destination.widthCurve = source.widthCurve;
        destination.time = source.time;
        destination.colorGradient = source.colorGradient;
    }
}
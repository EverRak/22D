using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    [SerializeField] private GameObject UpdateDot;

    [SerializeField] private Animator UpdateWidget;
    [SerializeField] private Animator ToastAnimator;

    [SerializeField] private TranslationIntermediate ToastTranslator;
    [SerializeField] private TranslationIntermediate UpdateWidgetTranslator;

    private bool Updated;
    private string FoundVersion;

    private void Start()
    {
        StartCoroutine(GetRequestAndShow());
    }

    private void UpdateCheck()
    {
        if (Updated)
        {
            ToastTranslator.TranslateTarget();
            ToastAnimator.Play("FadeIn");
        }
        else
        {
            UpdateWidgetTranslator.Translate(FoundVersion);
            UpdateWidget.Play("FadeIn");
        }
    }

    public void CheckForUpdate() => StartCoroutine(GetRequestAndCheck());

    private IEnumerator GetRequestAndCheck()
    {
        yield return StartCoroutine(GetRequest("https://raw.githubusercontent.com/EverRak/22D/refs/heads/main/version.txt"));
        UpdateCheck();
    }

    private IEnumerator GetRequestAndShow()
    {
        yield return StartCoroutine(GetRequest("https://raw.githubusercontent.com/EverRak/22D/refs/heads/main/version.txt"));
        // If it's not updated, show this.
        UpdateDot.SetActive(!Updated);
    }

    private IEnumerator GetRequest(string uri)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            Debug.Log("Error: " + webRequest.error);
        else
        {
            FoundVersion = webRequest.downloadHandler.text;
            Updated = IsUpdated(FoundVersion);
            Debug.Log("Received: " + FoundVersion);
        }
    }

    private bool IsUpdated(string newVersion) => newVersion.Trim() == Application.version;

    public void GotoItchIOPage() => Application.OpenURL("https://everrak.itch.io/too2d");
}
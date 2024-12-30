using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatformList
{
    public GameObject[] PlatformPrefabs;
}

public class PlatformCreator : MonoBehaviour
{
    [SerializeField] private PlatformList[] PlatformPrefabsStyles;

    [SerializeField] private Transform PlatformParent;
    [SerializeField] private Transform LastCreated;

    private List<Transform> Platforms = new List<Transform>();

    private int LastPlatform = -1;

    private int PlatformStyle;

    private void Start()
    {
        PlatformStyle = PlayerPrefs.GetInt("PlatformStyle", 0);

        while (Platforms.Count < 30)
            CreatePlatform();
    }

    private void CreatePlatform()
    {
        int randomPlatform = 0;
        
        while (randomPlatform == LastPlatform || PlatformPrefabsStyles[PlatformStyle].PlatformPrefabs[randomPlatform].GetComponent<Platform>().StartingDistance > FindObjectOfType<PlayerMovement>().Points)
            randomPlatform = Random.Range(0, PlatformPrefabsStyles[PlatformStyle].PlatformPrefabs.Length);

        LastPlatform = randomPlatform;
        Transform platform = Instantiate(PlatformPrefabsStyles[PlatformStyle].PlatformPrefabs[randomPlatform], PlatformParent).transform;
        platform.position = LastCreated.position + Vector3.right * (LastCreated.localScale.x / 2 + FindObjectOfType<PlayerMovement>().MoveSpeed * 2 + Random.Range(-1f, 1f));
        platform.localScale = new Vector3(Random.Range(7f, 10f), platform.localScale.y/* Random.Range(6f, 10f)*/);
        platform.position = platform.GetComponent<Platform>().Offset + new Vector3(platform.position.x + platform.localScale.x / 2, Mathf.Clamp(LastCreated.position.y + Random.Range(-1f, 1f), -5.5f, -6));

        Platforms.Add(platform);

        LastCreated = platform;
    }

    private void Update()
    {
        if (Platforms.Count < 30)
        {
            CreatePlatform();
        }
        else if (Platforms[0].position.x < transform.position.x - 30)
        {
            Destroy(Platforms[0].gameObject, 1);
            Platforms.RemoveAt(0);
        }
    }
}

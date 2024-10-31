using System.Collections.Generic;
using UnityEngine;

public class BGSquareCreator : MonoBehaviour
{
    [SerializeField] private GameObject SquarePrefab;

    [SerializeField] private Transform BGParent;
    [SerializeField] private Transform LastCreated;

    [SerializeField] private float ScrollSpeed;
    [SerializeField] private float MinDistance;
    [SerializeField] private float MaxDistance;
    [SerializeField] private float MinScale;
    [SerializeField] private float MaxScale;
    [SerializeField] private float VerticalMin;
    [SerializeField] private float VerticalMax;

    private List<Transform> Squares = new List<Transform>();

    private void Start()
    {
        while (Squares.Count < 40)
            CreateBG();
    }

    private void CreateBG()
    {
        Transform bg = Instantiate(SquarePrefab, BGParent).transform;
        bg.position = LastCreated.position + Vector3.right * (LastCreated.localScale.x / 2 + Random.Range(MinDistance, MaxDistance));
        bg.localScale = Vector3.one * Random.Range(MinScale, MaxScale);
        bg.position = new Vector3(bg.position.x, Random.Range(VerticalMin, VerticalMax));
        bg.Rotate(Vector3.forward, Random.Range(0f, 360f));

        Squares.Add(bg);

        LastCreated = bg;
    }

    private void Update()
    {
        if (Squares.Count < 40)
        {
            CreateBG();
        }
        else if (Squares[0].position.x < transform.position.x - 30)
        {
            Destroy(Squares[0].gameObject, 1);
            Squares.RemoveAt(0);
        }

        if (FindObjectOfType<PlayerMovement>().GameStarted)
            BGParent.position += Vector3.left * ScrollSpeed * FindObjectOfType<PlayerMovement>().MoveSpeed / 2 * Time.deltaTime;
    }
}
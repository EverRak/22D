using UnityEngine;

public class BGSquare : MonoBehaviour
{
    [SerializeField] private float MinRotationSpeed;
    [SerializeField] private float MaxRotationSpeed;

    private float RotationSpeed;

    private void Awake()
    {
        RotationSpeed = Random.Range(MinRotationSpeed, MaxRotationSpeed);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * RotationSpeed);
    }
}
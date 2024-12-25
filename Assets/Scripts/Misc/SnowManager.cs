using UnityEngine;

public class SnowManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem Snow;

    [SerializeField] private PlayerMovement Player;

    private void Update()
    {
        ParticleSystem.EmissionModule em = Snow.emission;
        em.rateOverTime = Player.MoveSpeed - 5;
    }
}

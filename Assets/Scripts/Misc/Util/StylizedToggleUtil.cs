using UnityEngine;

// Toggle estilizado (com animações)
public class StylizedToggleUtil : MonoBehaviour
{
    // O animador do toggle.
    [SerializeField] private Animator ToggleAnimator;

    // Se está ativo ou não.
    private bool Toggled;

    // Muda o estado do toggle.
    public void Toggle(bool state)
    {
        // Deixa o estado atual como o definido.
        Toggled = state;

        // Toca a animação dependendo do estado atual.
        ToggleAnimator.Play(Toggled ? "ToggleOn" : "ToggleOff");
    }

    // Apenas inverte o estado (ação padrão do toggle).
    public void Toggle() => Toggle(!Toggled);
}
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        // Asegúrate de que ContentFolder tenga el componente Animator
        anim = GetComponentInChildren<Animator>();
    }

    public void GoToSection(string stateName)
    {
        // Esto reproducirá la animación que creaste
        anim.Play(stateName);
    }
}

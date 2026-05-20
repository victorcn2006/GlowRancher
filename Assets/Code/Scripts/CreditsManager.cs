using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        if (_animator != null)
        {
            _animator.Play("Credits");
        }
    }
}

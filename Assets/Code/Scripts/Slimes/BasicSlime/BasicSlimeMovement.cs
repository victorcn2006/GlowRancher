using DG.Tweening;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlimeMovement : MonoBehaviour
{
    private BasicSlime _BasicSlime;

    // --------------------------------------------MOVEMENT PARAMETERS-------------------------------------------- \\
    [Header("RANGO DE TIEMPO ENTRE SALTOS")]
    [SerializeField] private float MIN_TIME = 1f;
    [SerializeField] private float MAX_TIME = 3f;

    [Header("VALORES DEL SALTO")]
    [SerializeField] private float JUMP_FORCE = 170f;
    [SerializeField] private float JUMP_TIME = 1f;
    [SerializeField] private float JUMP_DISTANCE = 1f;

    [Header("VALORES DE ROTACIÓN")]
    [SerializeField] private float ROTATE_DURATION = 0.5f;

    [Header("AUDIO")]
    [SerializeField] private EventReference _landSoundSlime;


    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float _jumpTimer;

    private Rigidbody _rb;

    private bool _beingAspired = false;

    private Vector3 jumpDirection;
    [SerializeField] private Transform groundChecker;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    public float groundCheckDistance = 0.7f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        _BasicSlime = GetComponent<BasicSlime>();
        _jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
    }

    private void Update()
    {
        if (Grounded() && !_beingAspired)
        {
            _jumpTimer -= Time.deltaTime;
            if (_jumpTimer <= 0)
            {
                GoJump();
                ResetJumpTimer();
            }
        }
    }

    private void GoJump()
    {
        _BasicSlime.animator.SetBool("Jump", true);
        
        Vector3 jumpDir = Vector3.zero;

        // Check if we should move towards food
        if (_BasicSlime.hungerSystem != null && _BasicSlime.hungerSystem.IsHungry() && _BasicSlime.foodDetector != null)
        {
            Transform closestFood = _BasicSlime.foodDetector.GetClosestFood(transform.position);
            if (closestFood != null)
            {
                jumpDir = (closestFood.position - transform.position).normalized;
                jumpDir.y = 0;
            }
        }

        // If no food target, jump in random direction
        if (jumpDir == Vector3.zero)
        {
            jumpDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }

        if (jumpDir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(jumpDir);
            transform.DORotateQuaternion(lookRotation, ROTATE_DURATION).SetEase(Ease.OutSine).OnComplete(() => 
            {
                _rb.AddForce((transform.up + transform.forward) * JUMP_FORCE);
                StartCoroutine(WaitToLand());
            });
        }
        else
        {
            _rb.AddForce(transform.up * JUMP_FORCE);
            StartCoroutine(WaitToLand());
        }
    }

    private IEnumerator WaitToLand()
    {
        yield return new WaitForSeconds(0.2f); // Give it time to leave the ground
        yield return new WaitUntil(() => Grounded());
        PlayLandingSound();
        _BasicSlime.animator.SetBool("Jump", false);
    }

    private void ResetJumpTimer()
    {
        _jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
        
    }

    private bool Grounded()
    {
        return Physics.Raycast(groundChecker.position, Vector3.down, groundCheckDistance);
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundCheckDistance);
    }

    public void SetBeingAspired(bool state)
    {
        _beingAspired = state;
    }

    public void SetGravity(bool state)
    {
        _rb.useGravity = state;
    }

    private void PlayLandingSound()
    {
        if (!_landSoundSlime.IsNull)
        {
            RuntimeManager.PlayOneShot(_landSoundSlime, transform.position);
        }
    }
}

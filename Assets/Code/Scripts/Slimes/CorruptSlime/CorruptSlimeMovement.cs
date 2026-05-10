using DG.Tweening;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptSlimeMovement : MonoBehaviour
{
    private CorruptSlime _corruptSlime;

    // --------------------------------------------MOVEMENT PARAMETERS-------------------------------------------- \\
    [Header("RANGO DE TIEMPO ENTRE SALTOS")]
    [SerializeField] private float MIN_TIME = 1f;
    [SerializeField] private float MAX_TIME = 2f; // Slightly more aggressive than basic

    [Header("VALORES DEL SALTO")]
    [SerializeField] private float JUMP_FORCE = 180f; // Slightly more force
    [SerializeField] private float JUMP_TIME = 1f;
    [SerializeField] private float JUMP_DISTANCE = 1.2f;
    [SerializeField] private float MAX_PHYSICS_JUMP_DISTANCE = 3.5f; // CALIBRATE THIS

    [Header("VALORES DE ROTACIÓN")]
    [SerializeField] private float ROTATE_DURATION = 0.3f; // Faster rotation

    [Header("AUDIO")]
    [SerializeField] private EventReference _landSoundSlime;
    [SerializeField] private EventReference _jumpSoundSlime;


    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float _jumpTimer;
    private Rigidbody _rb;
    private bool _beingAspired = false;
    private float _currentTargetDistance = -1f;
    [SerializeField] private Transform groundChecker;

    [Header("VALORES GROUNDED")]
    public float groundCheckDistance = 0.7f;

    [Header("DETECTION SETTINGS")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private LayerMask detectionMask;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _corruptSlime = GetComponent<CorruptSlime>();
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
        StartCoroutine(Jump());
        _corruptSlime.animator.SetBool("Jump", true);
        
        Vector3 jumpDir = GetTargetDirection();

        // If no target, jump in random direction
        if (jumpDir == Vector3.zero)
        {
            _currentTargetDistance = -1f;
            jumpDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }

        if (jumpDir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(jumpDir);
            transform.DORotateQuaternion(lookRotation, ROTATE_DURATION).SetEase(Ease.OutSine).OnComplete(() => 
            {
                float forceMultiplier = 1.0f;
                if (_currentTargetDistance > 0)
                {
                    forceMultiplier = Mathf.Clamp(_currentTargetDistance / MAX_PHYSICS_JUMP_DISTANCE, 0.3f, 1.0f);
                }

                Vector3 jumpForceVector = (transform.up * JUMP_FORCE) + (transform.forward * JUMP_FORCE * forceMultiplier);
                _rb.AddForce(jumpForceVector);
                StartCoroutine(WaitToLand());
            });
        }
        else
        {
            _rb.AddForce(transform.up * JUMP_FORCE);
            StartCoroutine(WaitToLand());
        }
    }

    private Vector3 GetTargetDirection()
    {
        Transform target = FindClosestTarget();
        if (target != null)
        {
            Vector3 posNoY = transform.position; posNoY.y = 0;
            Vector3 targetNoY = target.position; targetNoY.y = 0;
            _currentTargetDistance = Vector3.Distance(posNoY, targetNoY);

            Vector3 dir = (target.position - transform.position).normalized;
            dir.y = 0;
            return dir;
        }
        _currentTargetDistance = -1f;
        return Vector3.zero;
    }

    private Transform FindClosestTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, detectionMask);
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider col in colliders)
        {
            // Check for Player or IEatable
            bool isTarget = col.CompareTag("Player") || col.GetComponent<IEatable>() != null;
            
            if (isTarget)
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = col.transform;
                }
            }
        }
        return closest;
    }

    private IEnumerator WaitToLand()
    {
        yield return new WaitForSeconds(0.7f);
        yield return new WaitUntil(() => Grounded());
        PlayLandingSound();
        _corruptSlime.animator.SetBool("Jump", false);
    }
    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.7f); // Give it time to leave the ground
        PlayJumpSound();

    }

    private void ResetJumpTimer()
    {
        _jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
    }

    private bool Grounded()
    {
        return Physics.Raycast(groundChecker.position, Vector3.down, groundCheckDistance);
    }

    public void SetBeingAspired(bool state)
    {
        _beingAspired = state;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundChecker.position, Vector3.down * groundCheckDistance);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    private void PlayLandingSound()
    {
        if (!_landSoundSlime.IsNull)
        {
            RuntimeManager.PlayOneShot(_landSoundSlime, transform.position);
        }
    }
    private void PlayJumpSound()
    {
        if (!_landSoundSlime.IsNull)
        {
            RuntimeManager.PlayOneShot(_landSoundSlime, transform.position);
        }
    }
}

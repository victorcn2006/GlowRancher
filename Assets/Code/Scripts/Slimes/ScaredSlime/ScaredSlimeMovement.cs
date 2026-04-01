using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredSlimeMovement : MonoBehaviour
{
    private ScaredSlime _ScaredSlime;

    // --------------------------------------------MOVEMENT PARAMETERS-------------------------------------------- \\

    [Header("RANGO DE TIEMPO ENTRE SALTOS")]
    [SerializeField] private float minTime = 5f;
    [SerializeField] private float maxTime = 10f;

    [Header("RANGO DE TIEMPO ENTRE SALTOS ASUSTADO")]
    [SerializeField] private float scaredJumpTime = 0.01f;

    [Header("VALORES DEL SALTO")]
    [SerializeField] private float jumpForce = 170f;
    [SerializeField] private float jumpDistance = 1f;

    [Header("VALORES DEL SALTO ASUSTADO")]
    [SerializeField] private float scaredJumpForce = 120f;
    [SerializeField] private float scaredJumpDistance = 0.5f;

    [Header("VALORES DE ROTACIÓN")]
    [SerializeField] private float rotateDuration = 2f;
    [SerializeField] private float scaredRotateDuration = 0.01f;

    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float _jumpTimer;
    private bool _isJumping;

    private Rigidbody _rb;

    private bool _beingAspired = false;
    private bool _scared = false;

    private Vector3 jumpDirection;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    [SerializeField] private float groundCheckDistance = 0.35f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        _ScaredSlime = GetComponent<ScaredSlime>();
        _jumpTimer = Random.Range(minTime, maxTime);
    }

    private void Update()
    {

        if (Grounded() && !_beingAspired && !_isJumping)
        {
            _jumpTimer -= Time.deltaTime;

            if (_jumpTimer <= 0)
            {
                _isJumping = true;
                GoJump();
                ResetJumpTimer();
            }
        }
    }

    private void GoJump()
    {

        Vector3 destination = Vector3.zero;
        Sequence seq = DOTween.Sequence();
                
        if (_scared)
        {
            Transform player = _ScaredSlime.playerDetector.GetPlayerTransform();
            if (player != null)
            {
                Vector3 fleeDirection = (transform.position - player.position).normalized;
                fleeDirection.y = 0;
                if (fleeDirection == Vector3.zero) fleeDirection = -transform.forward;
                destination = transform.position + fleeDirection * scaredJumpDistance;
            }
            else
            {
                Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                destination = transform.position + randomDir * jumpDistance;
            }
        }
        else
        {
            Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            destination = transform.position + randomDir * jumpDistance;
        }


        Vector3 direction = destination - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            if (_scared) seq.Append(transform.DORotateQuaternion(lookRotation, scaredRotateDuration).SetEase(Ease.OutSine));
            else seq.Append(transform.DORotateQuaternion(lookRotation, rotateDuration).SetEase(Ease.OutSine));
        }
        

        StartCoroutine(Jump());

    }

    private IEnumerator Jump()
    {
        if (_scared)
        {
            yield return new WaitForSeconds(scaredRotateDuration);
            _rb.AddForce((transform.up + transform.forward) * scaredJumpForce);
        }
        else
        {
            yield return new WaitForSeconds(rotateDuration);
            _rb.AddForce((transform.up + transform.forward) * jumpForce);
        }
    }

    private void ResetJumpTimer()
    {
        if (_scared) _jumpTimer = scaredJumpTime;
        else _jumpTimer = Random.Range(minTime, maxTime);
    }

    private bool Grounded()
    {
        bool check = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
        if(!check) _isJumping = false;
        return check;
       
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

    public void SetScared(bool state)
    {
        if (_scared == state) return;
        _scared = state;
        ResetJumpTimer();
    }

}

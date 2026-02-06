using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredSlimeMovement : MonoBehaviour
{
    private ScaredSlime _ScaredSlime;

    // --------------------------------------------MOVEMENT PARAMETERS-------------------------------------------- \\

    [Header("RANGO DE TIEMPO ENTRE SALTOS")]
    private const float MIN_TIME = 5f;
    private const float MAX_TIME = 10f;

    [Header("RANGO DE TIEMPO ENTRE SALTOS ASUSTADO")]
    private const float SCARED_JUMP_TIME = 0.2f;

    [Header("VALORES DEL SALTO")]
    private const float JUMP_FORCE = 170f;
    private const float JUMP_DISTANCE = 1f;

    [Header("VALORES DEL SALTO ASUSTADO")]
    private const float SCARED_JUMP_FORCE = 200f;
    private const float SCARED_JUMP_DISTANCE = 1.5f;

    [Header("VALORES DE ROTACIÓN")]
    private const float ROTATE_DURATION = 2f;
    private const float SCARED_ROTATE_DURATION = 0.2f;

    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float _jumpTimer;
    private bool _isJumping;

    private Rigidbody _rb;

    private bool _beingAspired = false;
    private bool _scared = false;

    private Vector3 jumpDirection;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    float groundCheckDistance = 0.35f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        _ScaredSlime = GetComponent<ScaredSlime>();
        _jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
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
            destination = transform.position + (_ScaredSlime.playerDetector.GetPlayerTransform().position - transform.position ).normalized * -1;
            //destination *= SCARED_JUMP_DISTANCE;
        }
        else
        {
            Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            destination = transform.position + randomDir;
        }
            destination *= JUMP_DISTANCE;


        Vector3 direction = destination - transform.position;
        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        if(_scared) seq.Append(transform.DORotateQuaternion(lookRotation, SCARED_ROTATE_DURATION).SetEase(Ease.OutSine));
        else seq.Append(transform.DORotateQuaternion(lookRotation, ROTATE_DURATION).SetEase(Ease.OutSine));
        

        StartCoroutine(Jump());

    }

    private IEnumerator Jump()
    {
        if (_scared)
        {
            yield return new WaitForSeconds(SCARED_ROTATE_DURATION);
            _rb.AddForce((transform.up + transform.forward) * SCARED_JUMP_FORCE);
        }
        else
        {
            yield return new WaitForSeconds(ROTATE_DURATION);
            _rb.AddForce((transform.up + transform.forward) * JUMP_FORCE);
        }
    }

    private void ResetJumpTimer()
    {
        if (_scared) _jumpTimer = SCARED_JUMP_TIME;
        else _jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
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
        _scared = state;
        ResetJumpTimer();

    }

}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlimeMovement : MonoBehaviour
{
    private BasicSlime _BasicSlime;

    // --------------------------------------------MOVEMENT PARAMETERS-------------------------------------------- \\
    [Header("RANGO DE TIEMPO ENTRE SALTOS")]
    private const float MIN_TIME = 5f;
    private const float MAX_TIME = 10f;

    [Header("VALORES DEL SALTO")]
    [SerializeField] private float JUMP_FORCE = 3f;
    [SerializeField] private float JUMP_TIME = 1f;
    [SerializeField] private float JUMP_DISTANCE = 1f;

    [Header("VALORES DE ROTACIÓN")]
    [SerializeField] private float ROTATE_DURATION = 2f;

    // --------------------------------------------PRIVATE VARIABLES--------------------------------------------\\
    private float jumpTimer;

    private Rigidbody rb;

    private bool beingAspired = false;

    private Vector3 jumpDirection;


    // --------------------------------------------RAYCAST SETTINGS--------------------------------------------\\
    [Header("VALORES GROUNDED")]
    public float groundCheckDistance = 0.7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        _BasicSlime = GetComponent<BasicSlime>();
        jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
    }

    private void Update()
    {

        if (Grounded() && !beingAspired)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0)
            {
                GoJump();
                ResetJumpTimer();
            }
        }
    }

    private void GoJump()
    {

        Vector3 destination = Vector3.zero;
        Sequence seq = DOTween.Sequence();

        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        destination = transform.position + randomDir;

        destination *= JUMP_DISTANCE;

        Vector3 direction = destination - transform.position;
        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        seq.Append(transform.DORotateQuaternion(lookRotation, ROTATE_DURATION).SetEase(Ease.OutSine));

        StartCoroutine(Jump());

    }

    private IEnumerator Jump()
    {
        yield return new WaitForSeconds(ROTATE_DURATION);
        rb.AddForce((transform.up + transform.forward) * JUMP_FORCE);
    }

    private void ResetJumpTimer()
    {
        jumpTimer = Random.Range(MIN_TIME, MAX_TIME);
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundCheckDistance);
    }

    public void SetBeingAspired(bool state)
    {
        beingAspired = state;
    }

    public void SetGravity(bool state)
    {
        rb.useGravity = state;
    }
}

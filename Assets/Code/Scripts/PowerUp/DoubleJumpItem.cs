using UnityEngine;

public class DoubleJumpItem : MonoBehaviour
{
    [Header("Visual Settings")]
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float _floatAmplitude = 0.5f;
    [SerializeField] private float _floatFrequency = 1f;

    private bool _playerDoubleJump = false;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        // Animación simple: Rotar y Flotar
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        float newY = _startPos.y + Mathf.Sin(Time.time * _floatFrequency) * _floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Intentar obtener el componente de movimiento del jugador
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            _playerDoubleJump = true;
            player.EnableDoubleJumpItem();

            // Aquí puedes añadir un efecto de sonido o partículas antes de destruir
            Destroy(gameObject);
        }

    }
}

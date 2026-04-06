using UnityEngine;

public class Incinerator : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IAspirable>() != null) collision.gameObject.SetActive(false);
    }
}

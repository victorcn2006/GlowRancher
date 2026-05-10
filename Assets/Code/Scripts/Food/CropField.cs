using UnityEngine;

public class CropField : MonoBehaviour
{
    private bool _plantCreated;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Seed") && !_plantCreated)
        {
            Seed seed = other.GetComponent<Seed>();
            Debug.LogError("entraaaaaa");
            if (seed != null)
            {
                GameObject crop = Instantiate(seed.cropPrefab, transform.position, Quaternion.identity, transform);
                crop.SetActive(true);
            }

            Destroy(other.gameObject);
            _plantCreated = true;
        }
    }

    public void ClearHarvestState() {
        _plantCreated = false;
    }
}

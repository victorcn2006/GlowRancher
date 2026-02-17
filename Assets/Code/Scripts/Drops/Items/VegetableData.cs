using UnityEngine;

public class VegetableData : MonoBehaviour
{
    [SerializeField] private Vegetable _vegetableData;

    private void Update()
    {
        _vegetableData.time += Time.deltaTime;

        if(_vegetableData.time >= 60 )
        {
            Debug.Log("Reset" + this.gameObject.name);
            _vegetableData.time = 0f;
        }
    }

}

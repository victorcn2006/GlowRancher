using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSystem : MonoBehaviour
{

    [SerializeField] private GemsPool.gemTypes _gemTypeToDrop;
    private ISlime _slime;

    private void Start()
    {
        _slime = GetComponentInParent<ISlime>();
    }

    public IEnumerator SpawnGem()
    {
        _slime.animator.SetBool("DropGem", true);
        yield return new WaitForSeconds(2f);
        GameObject newGem = GemsPool.Instance.GetGem(_gemTypeToDrop);
        newGem.SetActive(true);
        newGem.transform.position = transform.position;
        _slime.animator.SetBool("DropGem", false);
    }
}

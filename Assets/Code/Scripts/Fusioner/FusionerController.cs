using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionerController : MonoBehaviour
{
    [Header("Position")]
    public Transform _spawnLocalization;

    [Header("Var")]
    private string _firstCollision = null;
    [Tooltip("variable que servira per enmagatzemar el name una gema i fer la fusio")]
    private string _secondCollision = null;

    private void OnTriggerEnter(Collider collision)
    {
        
        try
        {
            if (collision.CompareTag("Gem")) {
                string obj = collision.gameObject.GetComponent<Gem>().GetName();//agafem el nom de la gema
                if (collision.CompareTag("Gem") && _firstCollision == null)
                {
                    _firstCollision = obj;
                    Debug.Log($"Has oferit {collision.gameObject.GetComponent<Gem>().data.gemName}");
                    collision.gameObject.SetActive(false);
                }
                else if (collision.CompareTag("Gem") && _secondCollision == null && obj != _firstCollision)
                {
                    _secondCollision = obj;
                    collision.gameObject.SetActive(false);
                    string result = CheckFusions(_firstCollision, _secondCollision);
                    if (result != null)
                    {
                        Debug.Log("Resultat: " + result);
                    }
                    else
                    {
                        Debug.Log("Combinació no reconeguda");
                    }

                    _firstCollision = null;             //fem reset de les dos variables
                    _secondCollision = null;
                }
            }
            
            

        }catch(System.Exception e)
        {
            Debug.Log(e);
        }
    }
    private string CheckFusions(string _firstGem, string _secondGem)
    {
        if ((_firstGem == "BlueSlimestone" && _secondGem == "RedSlimeStone") ||
           (_firstGem == "RedSlimeStone" && _secondGem == "BlueSlimestone"))
        {

            StartCoroutine(DelayTime());

            return "Red and Blue SlimeStone";
        }
        else
        {
            return null;
        }
    }
    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(5f);
        GameObject gem = GemsPool.Instance.GetGem(GemsPool.gemTypes.REDBLUE_GEM);
        gem.SetActive(true);
        gem.transform.position = _spawnLocalization.position;
    }
}

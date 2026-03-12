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

    private void OnCollisionEnter(Collision collision)
    {
        string obj = collision.gameObject.GetComponent<Gem>().GetName();//agafem el nom de la gema
        if (collision.collider.tag == "Gem" && _firstCollision == null)
        {
            _firstCollision = obj;
            Debug.Log($"Has oferit {collision.gameObject.GetComponent<Gem>().data.gemName}");
            collision.gameObject.SetActive(false);

        }else if (collision.collider.tag == "Gem" && _secondCollision == null && obj != _firstCollision)
        {
            _secondCollision = obj;
            string result = CheckFusions(_firstCollision, _secondCollision);
            if (result != null) {
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

    private string CheckFusions(string _firstGem, string _secondGem)
    {
        if ((_firstGem == "BlueSlimestone" && _secondGem == "RedSlimeStone") ||
           (_firstGem == "RedSlimeStone" && _secondGem == "BlueSlimestone")){
            GameObject gem = GemsPool.Instance.GetGem(GemsPool.gemTypes.REDBLUE_GEM);
            gem.transform.position = _spawnLocalization.position;
            return "Red and Blue SlimeStone";
        }
        else {
            return null;
        }  
    }
}

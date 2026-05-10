using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarCollider : MonoBehaviour, IInteractive
{
    public void OnInteract() => GetComponentInParent<MagicRock>().ActiveRock();

}

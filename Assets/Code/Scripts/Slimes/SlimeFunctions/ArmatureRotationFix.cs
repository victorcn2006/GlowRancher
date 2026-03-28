using UnityEngine;

public class ArmatureRotationFix : MonoBehaviour
{
    [SerializeField] private Transform _armature;
    [SerializeField] private Vector3 _lockedLocalRotation = new Vector3(0, 0, 90);

    private void Awake()
    {
        if (_armature == null)
        {
            _armature = transform.Find("Armature");
        }
    }

    private void LateUpdate()
    {
        if (_armature != null)
        {
            _armature.localEulerAngles = _lockedLocalRotation;
        }
    }
}

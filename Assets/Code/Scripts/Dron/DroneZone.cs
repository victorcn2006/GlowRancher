using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DroneZone : MonoBehaviour
{
    [SerializeField] private Transform _droneStartingPoint;
    [SerializeField] private Transform _droneDeliveryPoint;
    [SerializeField] private Transform _dronefinishPoint;

    [SerializeField] private GameObject _dronePrefab;

    public void CallDrone(GameObject buildingToDelivery)
    {
        GameObject drone = Instantiate(_dronePrefab, transform);
        drone.transform.position = _droneStartingPoint.position;
        drone.GetComponent<Drone>().FillBox(buildingToDelivery);

        drone.transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(
            drone.transform.DOScale(Vector3.one, 0.5f)
                 .SetEase(Ease.Linear)
        );
        seq.Join(
            drone.transform.DOMove(_droneDeliveryPoint.position, 5f)
                 .SetEase(Ease.InOutQuad)
                 .OnComplete(() => {
                     drone.GetComponent<Drone>().DropBox();
                 })
        );
        seq.AppendInterval(2f);
        seq.Append(
            drone.transform.DOMove(_dronefinishPoint.position, 5f)
                 .SetEase(Ease.InOutQuad)
        );
        seq.Append(
            drone.transform.DOScale(Vector3.zero, 0.5f)
                 .SetEase(Ease.Linear)
        );
        seq.OnComplete(() => Destroy(drone));
    }

}

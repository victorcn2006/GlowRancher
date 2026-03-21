using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadScreenAnimation : MonoBehaviour
{
    [SerializeField] private Image _imatgeLoading;
    [SerializeField] private TMP_Text _textLoading;

    
    private void OnEnable()
    {
        StartCoroutine(TextLoadingDots());
    }

    private void FixedUpdate()
    {
        RotationImage();
    }

    private void RotationImage()
    {
        Vector3 rotation = new Vector3(0, 0, -10);
        _imatgeLoading.transform.Rotate(rotation * 10 * Time.fixedDeltaTime);
    }
    private IEnumerator TextLoadingDots()
    {
        _textLoading.text = "Loading";
        yield return new WaitForSeconds(0.5f);

        _textLoading.text = "Loading.";
        yield return new WaitForSeconds(0.5f);

        _textLoading.text = "Loading..";
        yield return new WaitForSeconds(0.5f);

        _textLoading.text = "Loading...";
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(TextLoadingDots());
    }


}

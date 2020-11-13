using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBuild : MonoBehaviour
{
    [SerializeField] private Slider prepareValue;
    [SerializeField] private float maxValue;
    private float value;
    private void Start()
    {
        value = 0f;
        prepareValue.maxValue = maxValue;
        StartCoroutine(MainBuildingLogic());
    }

    IEnumerator MainBuildingLogic() {
        while (value < maxValue) {
            value += Time.deltaTime;
            prepareValue.value = value;
            yield return null;
        }
    }
}

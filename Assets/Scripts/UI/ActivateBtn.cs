using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class ActivateBtn : MonoBehaviour
{
    public static ActivateBtn Instantiate;
    [SerializeField] private BtnState state;
    [SerializeField] private LeanButton btn;
    [SerializeField] private ProceduralImage img;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    float activationTime;

    private Coroutine activationCoroutine;
    private Tower currentTower;
    private enum BtnState
    {
        InActive,
        Active
    }

    private void Awake()
    {
        if (!Instantiate)
        {
            Instantiate = this;
        }

        btn.OnClick.AddListener(BtnUnpressed);
        btn.OnDown.AddListener(BtnPressed);
    }

    private void BtnPressed()
    {
        activationCoroutine = StartCoroutine(StartActivate());
        SetDefaultBtnState();
    }

    private IEnumerator StartActivate()
    {
        float tempValue = 0f;
        while (tempValue <= activationTime)
        {
            tempValue += Time.deltaTime;
            img.fillAmount = tempValue / activationTime;
            yield return null;
        }

        currentTower.ActivateTower();
    }

    private void BtnUnpressed()
    {
        StopCoroutine(activationCoroutine);
        SetDefaultBtnState();
    }

    public void UpdateActivationData(List<Tower> towerList) {
        if (towerList.Count == 0)
        {
            state = BtnState.InActive;
            btn.interactable = false;
        }
        else {
            state = BtnState.Active;
            btn.interactable = true;
            currentTower = towerList[towerList.Count - 1];
            this.activationTime = currentTower.activationTime;
            
        }
        //this.activationTime = activationTime;
    }


    private void SetDefaultBtnState() {
        img.fillAmount = 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Gui;
using UnityEngine.UI.ProceduralImage;

public class AtackBtn : MonoBehaviour
{
    public static AtackBtn Instantiate;
    [SerializeField] private BtnState state;
    [SerializeField] private LeanButton btn;
    [SerializeField] private ProceduralImage img;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;

    private Coroutine atackCoroutine;
    private enum BtnState { 
        InActive,
        Active
    }

    private void Awake()
    {
        if (!Instantiate) {
            Instantiate = this;
        }

        btn.OnClick.AddListener(AtackUnpressed);
        btn.OnDown.AddListener(AtackPressed);
    }

    public void UpdateState(int numOfEnemies) {
        if (numOfEnemies == 0)
        {
            state = BtnState.InActive;
            img.color = inactiveColor;
            btn.interactable = false;
        }
        else {
            state = BtnState.Active;
            img.color = activeColor;
            btn.interactable = true;
        }
    }

    private void AtackPressed() {
        atackCoroutine = StartCoroutine(StartAtacking());
    }

    private IEnumerator StartAtacking() {
        while (true)
        {
            Player.Instantiate.Atack();
            yield return new WaitForSeconds(1f);
        }
    }

    private void AtackUnpressed()
    {
        StopCoroutine(atackCoroutine);
    }
}

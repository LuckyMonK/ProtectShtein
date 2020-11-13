using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Gui;
public class StartMenuController : MonoBehaviour
{
    [SerializeField] private LeanButton startBtn;
    [SerializeField] private LeanButton upgradeBtn;
    [SerializeField] private LeanButton settingsBtn;
    [SerializeField] private LeanButton exitBtn;

    private void Start()
    {
        startBtn.OnClick.AddListener(StartBtnClick);
    }

    private void StartBtnClick() {
        SceneManager.LoadSceneAsync(1);
    }
}

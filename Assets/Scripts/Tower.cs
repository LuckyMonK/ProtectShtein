using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DigitalRuby.LightningBolt;

[Serializable]
public class Tower : MonoBehaviour
{
    [SerializeField] private TowerType towerType;
    [SerializeField] private TowerState towerState;

    [SerializeField] private float timeToActivate = 3f;
    [SerializeField] private float energy = 100f;
    private float _energy;
    [SerializeField] private Slider energySlider;

    public List<Enemy> targets = new List<Enemy>();

    public float activationTime = 5f;

    [SerializeField] private float cooldown = 1f;

    [SerializeField] private float power = 10f;

    [SerializeField] private LightningBoltScript lightting;
    private enum TowerType { 
        Atack
    }

    private enum TowerState { 
        Inactive,
        Active
    }

    public void ActivateTower() {
        StartCoroutine(TowerLogic());
    }

    private IEnumerator TowerLogic() {
        towerState = TowerState.Active;
        _energy = energy;
        while (_energy >= 0f) {
            if (targets.Count > 0)
            {
                AtackTarget();
                yield return new WaitForSeconds(cooldown);
            }
            else {
                yield return null;
            }
        }
    }

    private void AtackTarget() {
        if (targets[0]._hp > 0)
        {
            StartCoroutine(LightingWork(targets[0].gameObject));
            targets[0].DamageFromTower(power);
            return;
        }
        else {
            if (targets.Count > 1)
            {
                targets.Remove(targets[0]);
                AtackTarget();
            }
            else {
                return;
            }
        }
    }

    private IEnumerator LightingWork(GameObject target) {
        lightting.EndObject = target;
        lightting.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        lightting.gameObject.SetActive(false);
    }

    public void DeactivateTower() { }


}

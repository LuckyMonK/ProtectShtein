using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player Instantiate;
    [SerializeField] private List<Enemy> enemiesList = new List<Enemy>();

    [SerializeField] private List<Tower> towerList = new List<Tower>();
    public int power;
    private AnimationController animationController;

    private void Awake()
    {
        if (!Instantiate) {
            Instantiate = this;
        }

        animationController = GetComponent<AnimationController>();
    }
    public void Atack() {
        if (enemiesList.Count > 0)
        {
            enemiesList = enemiesList.Distinct().ToList();
            for (int i = 0; i < enemiesList.Count; i++) {
                enemiesList[i].DamageFromPlayer(power, enemiesList);
            }
            //foreach (var enemy in enemiesList)
            //{
            //    enemy.Damage(power, enemiesList);
            //}
            animationController.Atack();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy) {
            enemiesList.Add(enemy);
            AtackBtn.Instantiate.UpdateState(enemiesList.Count);
        }
        var tower = other.GetComponent<Tower>();
        if (tower)
        {
            towerList.Add(tower);
            towerList = towerList.Distinct().ToList();
            ActivateBtn.Instantiate.UpdateActivationData(towerList);
           
        }

    }

    public void OnTriggerExit(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemiesList.Remove(enemy);
        }
        AtackBtn.Instantiate.UpdateState(enemiesList.Count);

        var tower = other.GetComponent<Tower>();
        if (tower)
        {
            towerList.Remove(tower);
            ActivateBtn.Instantiate.UpdateActivationData(towerList);
        }
    }
}

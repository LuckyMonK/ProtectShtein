using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetSearch : MonoBehaviour
{
    [SerializeField] private Tower tower;

    private void OnTriggerEnter(Collider other)
    {
        Enemy target = other.GetComponent<Enemy>();
        if (target)
        {
            tower.targets.Add(target);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy target = other.GetComponent<Enemy>();
        if (target)
        {
            tower.targets.Remove(target);
        }
    }
}

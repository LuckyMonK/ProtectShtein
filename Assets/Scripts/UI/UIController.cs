using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instantiate;

    private void Awake()
    {
        if (!Instantiate)
        {
            Instantiate = this;
        }
    }

    
}

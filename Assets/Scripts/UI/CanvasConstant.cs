using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasConstant : MonoBehaviour
{
    [SerializeField] private Transform root;
    private Vector3 startRotation;

    private void Start()
    {
        root = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(root);
    }
}

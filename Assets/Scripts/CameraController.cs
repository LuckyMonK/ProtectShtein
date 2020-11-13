using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraState state;

    [SerializeField] private GameObject player;

    private Vector3 offset;

    public enum CameraState { 
        Follow,
        Free
    }

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (state is CameraState.Follow) {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime);
        }
    }
}

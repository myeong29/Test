using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    [Header("Set Dynically")]
    private GameObject player;

    [Header("Set in Inspecter")]
    public Vector3 lookOffset;
    public float cameraSpeed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector3 cameraPos = player.transform.position + lookOffset;
        //Vector3 lerpPos = Vector3.Lerp(transform.position, camerPos, cameraSpeed);
        transform.position = cameraPos;
        transform.LookAt(player.transform);
    }
}

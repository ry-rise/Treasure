﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// ビーム、右アーム
    /// </summary>
    [SerializeField]
    private GameObject beamArm = null;
    /// <summary>
    /// ライト、左アーム
    /// </summary>
    [SerializeField]
    private GameObject lightArm = null;
    [SerializeField]
    private GameManager gameManager = null;
    [SerializeField]
    private InputManager inputManager = null;
    [SerializeField]
    private Rigidbody rb = null;
    [SerializeField]
    private bool cameraTypeFlag = false;
    [SerializeField]
    private int angleVisionsNumber = 4;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //旋回
        if (cameraTypeFlag)
        {
            transform.Rotate(Vector3.up, inputManager.RC.AxisStick.x * gameManager.Parameter.TrunSpeed);
        }
        else
        {
            if (inputManager.LC.HandTrigger.GetDown)
            {
                transform.Rotate(Vector3.up, -360 / angleVisionsNumber);
            }
            if (inputManager.RC.HandTrigger.GetDown)
            {
                transform.Rotate(Vector3.up, 360 / angleVisionsNumber);
            }
        }
        //移動
        Vector3 moveAxis = new Vector3(inputManager.LC.AxisStick.x, 0, inputManager.LC.AxisStick.y) * gameManager.Parameter.MoveSpeed;
        rb.velocity = transform.rotation * moveAxis;
        //コントローラ（腕）の位置
        beamArm.transform.position = transform.rotation * inputManager.RC.Position + transform.position;
        beamArm.transform.localRotation = inputManager.RC.Rotation;
        lightArm.transform.position = transform.rotation * inputManager.LC.Position + transform.position;
        lightArm.transform.localRotation = inputManager.LC.Rotation;
    }
}

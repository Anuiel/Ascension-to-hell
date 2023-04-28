using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{   
    [SerializeField]
    private GameObject tagret;
    [Range(0, 1)]
    [SerializeField]
    private float scale;
    [Range(1, 10)]
    [SerializeField]
    private float zoomPower;
    
    private Camera cm;
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = tagret.GetComponent<PlayerInput>();
        cm = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 mousePos = playerInput.actions["ShootingTarget"].ReadValue<Vector2>();
        mousePos = cm.ScreenToWorldPoint(mousePos);

        Vector3 tagretPos = new Vector3(tagret.transform.position.x, tagret.transform.position.y, transform.position.z);
        Vector3 cursorPos = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        float true_scale = scale;

        if (playerInput.actions["RightClick"].IsPressed()) {
            true_scale /= zoomPower;
        }

        transform.position = (true_scale * tagretPos + (1 - true_scale) * cursorPos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // basic movement
        float verticalInput = playerInput.actions["Ver_mov"].ReadValue<float>();
        float horizontalInput = playerInput.actions["Hor_mov"].ReadValue<float>();
        Vector2 movementVector = SpeedScaler(speed, horizontalInput, verticalInput);
        Vector2 movement = Time.deltaTime * speed * movementVector;
        gameObject.transform.Translate(movement);
    }

    Vector2 SpeedScaler(float speed, float hor, float ver)
    {
        Vector2 res = new(speed * hor, speed * ver);
        if (hor * ver != 0)
            res /= Mathf.Sqrt(2);
        return res;
    }
}

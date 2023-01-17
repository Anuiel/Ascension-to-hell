using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;
    private PlayerInput playerInput;
    private PlayerBeing playerBeing;
    private List<GameObject> pickUpWeapon;

    private bool isDashing;
    private bool canDash;
    // [Range(0.1f, 3)]
    [SerializeField]
    float dashCooldown;
    [SerializeField]
    int dashAmount;
    private int dashCurrentAmount;
    // [Range(0.1f, 3)]
    [SerializeField]
    float dashTime;
    // [Range(5, 30)]
    [SerializeField]
    float dashPower;

    [SerializeField]
    Rigidbody2D rb;

    private Camera cm;

    // Start is called before the first frame update
    void Awake()
    {
        pickUpWeapon = new();
        playerInput = GetComponent<PlayerInput>();
        playerBeing = GetComponent<PlayerBeing>();
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
        canDash = true;
        dashCurrentAmount = dashAmount;
    }
    
    // Update is called once per frame
    void Update()
    {
        // dash
        if (playerInput.actions["Dash"].triggered && canDash) {
            print("Dash");
            StartCoroutine(Dash());
        }
        // weapon switch
        if (playerInput.actions["SwitchGun"].triggered)
        {
            playerBeing.WeaponSwitch();
        }
        // shooting
        Vector2 shootingPoint = playerInput.actions["ShootingTarget"].ReadValue<Vector2>();
        if (playerInput.actions["Shooting"].triggered)
        {
            playerBeing.ShootGun(shootingPoint);
        }

        Rotate(GetMousePosition(shootingPoint));

        // pick up weapon
        if (pickUpWeapon.Count != 0)
        {
            if (playerInput.actions["CollectGun"].triggered)
            {
                playerBeing.WeaponPick(pickUpWeapon[0].GetComponent<BasicWeapon>());
            }
        }
    }

    private Vector2 GetMousePosition(Vector2 point)
    {
        Vector2 MousePos = cm.ScreenToWorldPoint(point);
        return MousePos;
    }

    void Rotate(Vector2 point) {
        // rotate 
        Vector2 playerPos = transform.position;
        Vector2 direction = (point - playerPos).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    void FixedUpdate() {
        // basic movement
        if (isDashing) {
            return;
        }   
        float verticalInput = playerInput.actions["Ver_mov"].ReadValue<float>();
        float horizontalInput = playerInput.actions["Hor_mov"].ReadValue<float>();
        Vector2 movementVector = SpeedScaler(speed, horizontalInput, verticalInput);
        Vector2 movement = Time.deltaTime * speed * movementVector;
        rb.MovePosition(rb.position + movement);
        // gameObject.transform.Translate(movement);
    }

    private IEnumerator Dash() {

        float verticalInput = playerInput.actions["Ver_mov"].ReadValue<float>();
        float horizontalInput = playerInput.actions["Hor_mov"].ReadValue<float>();
        Vector2 movementVector = SpeedScaler(speed, horizontalInput, verticalInput);
        
        if ((verticalInput != 0 || horizontalInput != 0) && dashCurrentAmount > 0) {
            isDashing = true;
            rb.velocity = movementVector * dashPower;
            yield return new WaitForSeconds(dashTime);
            isDashing = false;
            StartCoroutine(ReloadDash());
        }
    }

    private IEnumerator ReloadDash() {
        dashCurrentAmount -= 1;
        yield return new WaitForSeconds(dashCooldown);
        dashCurrentAmount += 1;
    }

    Vector2 SpeedScaler(float speed, float hor, float ver)
    {
        Vector2 res = new(speed * hor, speed * ver);
        if (hor * ver != 0)
            res /= Mathf.Sqrt(2);
        return res;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Gun"))
        {
            pickUpWeapon.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            pickUpWeapon.Remove(collision.gameObject);
        }
    }
}

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

    private List<GameObject> buffs;
    
    private bool isDashing;
    public float dashEnergy; // public for Chargebar
    private float timeFromLastDash;
    [SerializeField] float dashCooldown;
    [SerializeField] public int dashAmount;
    [SerializeField] float dashTime;
    [SerializeField] float dashPower;
    [SerializeField] ChargeBar chargebar;

    [SerializeField]
    Rigidbody2D rb;

    private Camera cm;

    // Start is called before the first frame update
    void Awake()
    {
        pickUpWeapon = new();
        buffs = new();
        playerInput = GetComponent<PlayerInput>();
        playerBeing = GetComponent<PlayerBeing>();
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
        isDashing = false;
        dashEnergy = dashAmount;
        timeFromLastDash = dashTime;
    }
    
    // Update is called once per frame
    void Update()
    {
        dashEnergy = Mathf.Min(dashEnergy + Time.deltaTime, dashAmount);
        if (timeFromLastDash >= dashTime) {
            isDashing = false;
        } else {
            isDashing = true;
            timeFromLastDash += Time.deltaTime;
        }
        // dash
        if (playerInput.actions["Dash"].triggered && !isDashing) {
            Dash();
        }
        // weapon switch
        if (playerInput.actions["SwitchGun"].triggered)
        {
            playerBeing.WeaponSwitch();
        }
        // shooting
        Vector2 shootingPoint = playerInput.actions["ShootingTarget"].ReadValue<Vector2>();
        Rotate(GetMousePosition(shootingPoint));
        if (playerInput.actions["Shooting"].ReadValue<float>() > 0)
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
        if (buffs.Count != 0) {
            if (playerInput.actions["CollectGun"].triggered)
            {
                playerBeing.BuffPick(buffs[0].GetComponent<Buff>());
                Destroy(buffs[0]);
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
        if (chargebar) {chargebar.UpdateBar();}
        float verticalInput = playerInput.actions["Ver_mov"].ReadValue<float>();
        float horizontalInput = playerInput.actions["Hor_mov"].ReadValue<float>();
        Vector2 movementVector = SpeedScaler(speed, horizontalInput, verticalInput);
        Vector2 movement = speed * movementVector;
        rb.velocity = movement;
    }

    private void Dash() {

        float verticalInput = playerInput.actions["Ver_mov"].ReadValue<float>();
        float horizontalInput = playerInput.actions["Hor_mov"].ReadValue<float>();
        Vector2 movementVector = SpeedScaler(speed, horizontalInput, verticalInput);
        
        if ((verticalInput != 0 || horizontalInput != 0) && dashEnergy > 1) {
            isDashing = true;
            rb.AddForce(movementVector * dashPower, ForceMode2D.Impulse);
            dashEnergy -= 1;
            timeFromLastDash = 0;
        }
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
        if (collision.gameObject.CompareTag("Gun"))
        {
            pickUpWeapon.Add(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Buff")) {
            buffs.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            pickUpWeapon.Remove(collision.gameObject);
        } else if (collision.gameObject.CompareTag("Buff")) {
            buffs.Remove(collision.gameObject);
        }
    }
}

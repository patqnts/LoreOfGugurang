using MoreMountains.InventoryEngine;
using MoreMountains.Tools;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed = 5f;
    public CircleCollider2D circleCollider;
    public Vector2 movementInput;
    private Animator animator;
    private Rigidbody2D rb;
    public static PlayerController player;
    public bool controllerEnabled;
    private void Awake()
    {
        player = this;
    }
    private void Start()
    {
        joystick = FindObjectOfType<FixedJoystick>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (controllerEnabled)
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
        }
              
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 movement = new Vector2(movementInput.x, movementInput.y);
        movement.Normalize();

        rb.velocity = movement * moveSpeed;

        UpdateAnimatorParameters(movement);
    }

    private void UpdateAnimatorParameters(Vector2 movement)
    {
        animator.SetFloat("x", movement.x);
        animator.SetFloat("y", movement.y);
    }

    public void OnInteractionButtonClick()
    {
        StartCoroutine(EnableDisableCollider());
    }

    private IEnumerator EnableDisableCollider()
    {
        circleCollider.enabled = true;
        yield return new WaitForSeconds(1f);
        circleCollider.enabled = false;
    }

    public void MoveToSpawn(Transform spawnPoint)
    {
        this.gameObject.transform.position = spawnPoint.position;
    }

    public void SaveInventory()
    {
        MMEventManager.TriggerEvent(new MMGameEvent("Save"));
    }

    public void LoadInventory()
    {
        MMGameEvent.Trigger("Load");
    }

    void OnDisable()
    {
        rb.velocity = Vector2.zero;
    }

}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator playerAnimator;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    private bool movementDisabled;
    private int speed = OUTSIDE_SPEED;

    private const double ANIMATION_STOP_THRESHOLD = 0.01;
    private readonly Vector3 OUTSIDE_SIZE = new Vector3(0.7f, 0.7f, 1);
    private readonly Vector3 INSIDE_SIZE = Vector3.one;
    private const int INSIDE_SPEED = 100;
    private const int OUTSIDE_SPEED = 45;

    private GameObject stardewRoom;
    private GameObject outside;

    void Start() {
        playerAnimator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        InitializeRooms();
    }

    private void InitializeRooms() {
        stardewRoom = GameObject.Find("stardew_room");
        stardewRoom.SetActive(false);

        outside = GameObject.Find("outside");

        transform.localScale = OUTSIDE_SIZE;
    }

    void DisableMovement() {
        movementDisabled = true;
        rigidbody.velocity = Vector2.zero;
    }

    void EnableMovement() {
        movementDisabled = false;
    }

    void EnterStardew() {
        stardewRoom.SetActive(true);
        outside.SetActive(false);
        transform.localScale = INSIDE_SIZE;
        speed = INSIDE_SPEED;
        transform.position = GameObject.Find("stardew_spawn_point").transform.position;
    }

    void ExitStardew() {
        stardewRoom.SetActive(false);
        outside.SetActive(true);
        transform.localScale = OUTSIDE_SIZE;
        speed = OUTSIDE_SPEED;
        transform.position = GameObject.Find("outside_from_stardew_spawn_point").transform.position;

    }

    void Update() {
        if (movementDisabled) {
            playerAnimator.SetBool("isMoving", false);
            return;
        }

        if (rigidbody.velocity.sqrMagnitude < ANIMATION_STOP_THRESHOLD) {
            playerAnimator.SetBool("isMoving", false);
        } else {
            playerAnimator.SetBool("isMoving", true);
        }


        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            rigidbody.AddForce(Vector2.up * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            rigidbody.AddForce(Vector2.down * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            rigidbody.AddForce(Vector2.left * speed);
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            rigidbody.AddForce(Vector2.right * speed);
            spriteRenderer.flipX = false;
        }

        //if (Input.GetMouseButtonDown(0)) {
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //    if(mousePosition.x < transform.position.x) {
        //        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //    } else {
        //        gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //    }

        //    playerAnimator.SetBool("isMoving", true);

        //    tween = transform.DOMove(mousePosition, 2).OnComplete(() => {
        //        playerAnimator.SetBool("isMoving", false);
        //    });
        //}
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator playerAnimator;

    public bool movementDisabled;

    void Start() {
        playerAnimator = GetComponent<Animator>();
    }

    void DisableMovement() {
        movementDisabled = true;
    }

    void EnableMovement() {
        movementDisabled = false;
    }

    void Update() {
        if(movementDisabled) {
            return;
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(mousePosition.x < transform.position.x) {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            } else {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

            playerAnimator.SetBool("isMoving", true);

            transform.DOMove(mousePosition, 2).OnComplete(() => {
                playerAnimator.SetBool("isMoving", false);
            });
        }
    }
}

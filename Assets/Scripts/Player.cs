using UnityEngine;
using UnityEngine.InputSystem;

// a script for the player object
public class Player : MonoBehaviour {

    private PlayerControls controls;

    private int moveDir;    // 1, -1 or 0 depending on whether the player is moving up, down or not moving
    private float timeSinceLastMove = 0;

    // magic numbers
    [SerializeField] private float timeBetweenMoves;

    private void Awake() {
        controls = new PlayerControls();
        controls.main.MoveUp.performed += moveUp_performed;
        controls.main.MoveDown.performed += MoveDown_performed;
        controls.main.MoveDown.canceled += MoveDown_canceled;
        controls.main.MoveUp.canceled += MoveUp_canceled; ;
    }

    private void MoveUp_canceled(InputAction.CallbackContext obj) {
        if (moveDir == 1) {
            moveDir = 0;
        }
    }

    private void MoveDown_canceled(InputAction.CallbackContext obj) {
        if (moveDir == -1) {
            moveDir = 0;
        }
    }

    private void MoveDown_performed(InputAction.CallbackContext obj) {
        moveDir = -1;
    }

    private void moveUp_performed(InputAction.CallbackContext obj) {
        moveDir = 1;
    }

    private void OnEnable() {
        controls.main.Enable();
    }

    private void OnDisable() {
        controls.main.Disable();
    }

    void Start() {

    }


    void Update() {
        if (timeSinceLastMove > timeBetweenMoves) {
            timeSinceLastMove = 0;
            if (canMove()) {
                transform.position = transform.position + new Vector3(0, moveDir * GlobalConfig.distanceBetweenLines * 0.5f, 0);
            }
        }

        timeSinceLastMove += Time.deltaTime;
    }

    private bool canMove() {
        if (moveDir == 1 && transform.position.y >= GlobalConfig.distanceBetweenLines * 2) { return false; }
        if (moveDir == -1 && transform.position.y <= GlobalConfig.distanceBetweenLines * -2) { return false; }
        return true;
    }
}








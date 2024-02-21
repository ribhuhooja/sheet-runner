using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// a script for the player object
public class Player : MonoBehaviour {

    private PlayerControls controls;

    private int moveDir;    // 1, -1 or 0 depending on whether the player is moving up, down or not moving
    private float timeSinceLastMove = 0;
    private bool shiftPressed = false;

    public bool isPlayingNotes = true;

    // magic numbers
    [SerializeField] private float timeBetweenMoves;

    [SerializeField] private NotesManager notesManager;

    private void Awake() {
        controls = new PlayerControls();
        controls.main.MoveUp.performed += moveUp_performed;
        controls.main.MoveDown.performed += MoveDown_performed;
        controls.main.MoveDown.canceled += MoveDown_canceled;
        controls.main.MoveUp.canceled += MoveUp_canceled; ;

        controls.main.ShiftPressed.performed += ShiftPressed_performed;
        controls.main.ShiftPressed.canceled += ShiftPressed_canceled;

        controls.main.TogglePlay.performed += TogglePlay_performed;

        transform.position = new Vector3(GlobalConfig.playerX, transform.position.y, transform.position.z);
    }

    private void TogglePlay_performed(InputAction.CallbackContext obj) {
        isPlayingNotes = !isPlayingNotes;
        ToggleAlpha();
    }

    private void ShiftPressed_canceled(InputAction.CallbackContext obj) {
        shiftPressed = false;
        revertPosition();
    }

    private void ShiftPressed_performed(InputAction.CallbackContext obj) {
        shiftPressed = true;
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

    void Update() {
        if (timeSinceLastMove > timeBetweenMoves) {
            timeSinceLastMove = 0;
            if (CanMove()) {
                transform.position += new Vector3(0, moveDir * GlobalConfig.distanceBetweenLines * 0.5f, 0);
            }
        }

        timeSinceLastMove += Time.deltaTime;
    }

    private bool CanMove() {
        return !(OutOfBounds() && !shiftPressed) && !OutOfAbsoluteBounds();
    }

    private bool OutOfBounds() {
        if (moveDir == 1 && transform.position.y >= GlobalConfig.distanceBetweenLines * 2) { return true; }
        if (moveDir == -1 && transform.position.y <= GlobalConfig.distanceBetweenLines * -2) { return true; }
        return false;
    }

    private bool OutOfAbsoluteBounds() {
        if (moveDir == 1 && transform.position.y >= GlobalConfig.distanceBetweenLines * 4) { return true; }
        if (moveDir == -1 && transform.position.y <= GlobalConfig.distanceBetweenLines * -4) { return true; }
        return false;
    }

    private void revertPosition() {
        if (transform.position.y >= GlobalConfig.distanceBetweenLines * 2) {
            transform.position = new Vector3(transform.position.x, GlobalConfig.distanceBetweenLines * 2, transform.position.z);
        } else if (transform.position.y <= GlobalConfig.distanceBetweenLines * -2) {
            transform.position = new Vector3(transform.position.x, GlobalConfig.distanceBetweenLines * -2, transform.position.z);
        }
    }

    public int getNoteIndexFromYCoord() {
        // Player starts out on the B line, which is Y coord 0
        // if player's y-coord is y, then the index of the note to play
        // is (2*y/dist-between-lines) + index of B
        return (int)(2 * transform.position.y / GlobalConfig.distanceBetweenLines) + notesManager.noteIndices["B"];
    }

    // makes the player transparent when not playing notes
    private void ToggleAlpha() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr.color.a == 1) {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);
        } else {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
        }

    }
}








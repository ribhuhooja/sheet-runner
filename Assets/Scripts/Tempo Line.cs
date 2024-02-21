using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoLine : MonoBehaviour {
    private int bpm;
    private float speed;
    private bool notePlayed = false;

    private Player player;

    private NotesManager notesManager;


    private void Start() {
        player = FindObjectOfType<Player>();     // lazy, but works for now
    }

    void Update() {
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        // Janky unity collision detection not working
        if (!notePlayed && Math.Abs(transform.position.x - GlobalConfig.playerX) < 0.1) {
            notePlayed = true;
            if (player.isPlayingNotes) {
                notesManager.playNoteByIndex(player.getNoteIndexFromYCoord());
            }
        }

        if (transform.position.x < -GlobalConfig.screenWidth / 2 - 1) {
            Destroy(gameObject);
        }
    }


    public void SetSpeed(float timeBetweenTicks) {
        speed = GlobalConfig.barLength / timeBetweenTicks;

    }

    public void saveReferenceToNotesManager(NotesManager notesManager) {
        this.notesManager = notesManager;
    }

}

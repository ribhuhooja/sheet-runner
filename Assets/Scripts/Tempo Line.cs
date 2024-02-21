using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoLine : MonoBehaviour {
    private int bpm;
    private float speed;
    private bool notePlayed = false;

    private NotesManager notesManager;


    void Start() {

    }

    void Update() {
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        // Janky unity collision detection not working
        if (!notePlayed && System.Math.Abs(transform.position.x - GlobalConfig.playerX) < 0.1) {
            notesManager.playNote("C");
            notePlayed = true;
        }
    }


    public void SetSpeed(float timeBetweenTicks) {
        speed = GlobalConfig.barLength / timeBetweenTicks;

    }

    public void saveReferenceToNotesManager(NotesManager notesManager) {
        this.notesManager = notesManager;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoLinesManager : MonoBehaviour {

    [SerializeField] private int bpm;

    private AudioSource audioSource;
    [SerializeField] private TempoLine tempoLinePrefab;
    private Queue<TempoLine> tempoLines;

    [SerializeField] private NotesManager notesManager;

    private float secondsBetweenTicks;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        secondsBetweenTicks = 60 / bpm;
    }
    private void Start() {
        tempoLines = new Queue<TempoLine>();
        InvokeRepeating(nameof(MakeTempoLines), 0, secondsBetweenTicks);
        // InvokeRepeating(nameof(Tick), 0, secondsBetweenTicks);

    }

    private void Update() {

    }

    private void Tick() {
        audioSource.Play();
    }

    private void MakeTempoLines() {

        TempoLine line = Instantiate(tempoLinePrefab);

        line.SetSpeed(secondsBetweenTicks);
        line.saveReferenceToNotesManager(notesManager);
        line.transform.position = new Vector3(GlobalConfig.screenWidth / 2, 0, 0);
        tempoLines.Enqueue(line);


    }
}

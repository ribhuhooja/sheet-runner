using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour {

    public readonly Dictionary<string, int> noteIndices = new Dictionary<string, int>
        {
            {"C", 0},
            {"D", 1},
            {"E", 2},
            {"F", 3},
            {"G", 4},
            {"A", 5},
            {"B", 6},
            {"Cu", 7},
            {"Du", 8},
            {"Eu", 9},
            {"Fu", 10},
            {"Gu", 11 }
        };

    [SerializeField] public AudioClip[] notes;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void playNote(string noteString) {
        if (noteIndices.TryGetValue(noteString, out int index)) {
            audioSource.clip = notes[index];
            audioSource.Play();
        }
    }

    public void playNoteByIndex(int index) {
        if (index >= notes.Length || index < 0) {
            return;
        }
        audioSource.clip = notes[index];
        audioSource.Play();
    }
}

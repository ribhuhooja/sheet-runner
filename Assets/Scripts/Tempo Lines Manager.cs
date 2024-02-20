using UnityEngine;

public class TempoLinesManager : MonoBehaviour {

    [SerializeField] private int bpm;

    private AudioSource audioSource;

    private float secondsBetweenTicks;

    private float timeSinceLastTick = 0;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        secondsBetweenTicks = 60 / bpm;
    }
    void Start() {

    }

    void Update() {

        timeSinceLastTick += Time.deltaTime;

        if (timeSinceLastTick > secondsBetweenTicks) {
            timeSinceLastTick = 0;
            Tick();
        }

    }

    private void Tick() {
        audioSource.Play();
    }
}

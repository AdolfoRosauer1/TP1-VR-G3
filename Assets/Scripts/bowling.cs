

using UnityEngine;
using System.Collections.Generic; // Import needed for Dictionary
using System.Linq; // Import needed for LINQ operations like ToList()

public class bowling : MonoBehaviour
{
    // Scoreboard
    private Dictionary<Collider, float> colliders;
    private TextMesh scoreboard;
    private int[] scores;

    // Pins
    private GameObject[] pins;
    private Vector3[] pinsInitialPositions;
    private Vector3[] pinsInitialDirections;
    private Vector3[] pinsInitialAngles;
    private Vector3[] pinsInitialVelocities;
    private Vector3[] pinsInitialAngularVelocities;

    // Bola
    private GameObject bola;
    public float repeatTime = 7f;
    public float speed = 10f;
    public Vector3 winningPosition;

    private bool wasThrown = false;
    private int repetitions = 0;
    private bool gameOver = false;

    private Vector3 initialPosition;

    void Start()
    {
        colliders = new Dictionary<Collider, float>();
        scores = new int[5];
        pins = GameObject.FindGameObjectsWithTag("pins");
        bola = GameObject.FindGameObjectWithTag("bola");
        scoreboard = GameObject.FindGameObjectWithTag("scoreboard").GetComponent<TextMesh>();

        initialPosition = bola.transform.position;
        pinsInitialPositions = new Vector3[pins.Length];
        pinsInitialDirections = new Vector3[pins.Length];
        pinsInitialAngles = new Vector3[pins.Length];
        pinsInitialVelocities = new Vector3[pins.Length];
        pinsInitialAngularVelocities = new Vector3[pins.Length];
        for (int i = 0; i < pins.Length; i++){
            pinsInitialPositions[i] = pins[i].transform.position;
            pinsInitialDirections[i] = pins[i].transform.forward;
            pinsInitialAngles[i] = pins[i].transform.eulerAngles;
            pinsInitialVelocities[i] = pins[i].GetComponent<Rigidbody>().linearVelocity;
            pinsInitialAngularVelocities[i] = pins[i].GetComponent<Rigidbody>().angularVelocity;
        }
    }

    void Update()
    {
        if (gameOver) return;

        string scoreText = "";
        int totalScore = 0;
        for (int i = 0; i < scores.Length; i++) {
            int currentScore = 0;
            if (i < repetitions) {
                currentScore = scores[i];
                totalScore += currentScore;
                scoreText += currentScore;
            } else if (i == repetitions) {
                currentScore = 10 - colliders.Count;
                scoreText += currentScore;
            } else {
                scoreText += "-";
            }
            if (i < scores.Length - 1) {
                scoreText += " | ";
            }
        }
        scoreText = "Total: " + scoreText;
        
        scoreboard.text = scoreText;
        float time = Mathf.Repeat(Time.time, repeatTime);

        if (time < 2){
            if (wasThrown){
                scores[repetitions] = 10 - colliders.Count;
                repetitions++;

                if (repetitions >= 5) {
                    gameOver = true;
                    if (totalScore == 13) {
                        Camera.main.transform.position = winningPosition;
                    } else {
                        scoreboard.text = "Stuck forever";
                    }
                }
            }
            bola.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            bola.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            bola.transform.position = initialPosition;
            for (int i = 0; i < pins.Length; i++){
                pins[i].transform.position = pinsInitialPositions[i];
                pins[i].transform.forward = pinsInitialDirections[i];
                pins[i].transform.eulerAngles = pinsInitialAngles[i];
                pins[i].GetComponent<Rigidbody>().linearVelocity = pinsInitialVelocities[i];
                pins[i].GetComponent<Rigidbody>().angularVelocity = pinsInitialAngularVelocities[i];
            }
            wasThrown = false;
        }
        else{
            if (!wasThrown){
                Vector3 direction = Camera.main.transform.forward;
                bola.GetComponent<Rigidbody>().linearVelocity = direction * speed;
                wasThrown = true;
            }
            else{
                // do nothing
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        colliders[other] = Time.time;
    }

    void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}

using UnityEngine;
using System.Collections.Generic; // Import needed for Dictionary
using System.Linq; // Import needed for LINQ operations like ToList()

public class score : MonoBehaviour
{
    // Use Dictionary instead of HasMap
    private Dictionary<Collider, float> colliders;

    public TextMesh scoreboard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the dictionary
        colliders = new Dictionary<Collider, float>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the scoreboard
        scoreboard.text = colliders.Count.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        // Add or update the collider's timestamp directly
        colliders[other] = Time.time;
    }

    void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }
}

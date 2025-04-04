using UnityEngine;
using System.Collections.Generic; // Import needed for Dictionary
using System.Linq; // Import needed for LINQ operations like ToList()

public class score : MonoBehaviour
{
    private Dictionary<Collider, float> colliders;

    public TextMesh scoreboard;

    void Start()
    {
        colliders = new Dictionary<Collider, float>();
    }

    void Update()
    {
        scoreboard.text = (10 - colliders.Count).ToString();
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

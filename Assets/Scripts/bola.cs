using UnityEngine;

public class bola : MonoBehaviour
{
    public float speed = 10f;
    public float repeatTime = 5f;
    private Rigidbody rb;
    private GameObject[] pins;
    private Vector3[] pinInitialPositions;
    private Vector3[] pinInitialDirections;
    private Vector3[] pinInitialAngles;
    private Vector3[] pinInitialVelocities;
    private Vector3[] pinInitialAngularVelocities;


    private Vector3 initialPosition;
    private bool wasThrown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;

        pins = GameObject.FindGameObjectsWithTag("pins");

        // Initialize pins
        pinInitialPositions = new Vector3[pins.Length];
        pinInitialDirections = new Vector3[pins.Length];
        pinInitialAngles = new Vector3[pins.Length];
        pinInitialVelocities = new Vector3[pins.Length];
        pinInitialAngularVelocities = new Vector3[pins.Length];
        for (int i = 0; i < pins.Length; i++){
            pinInitialPositions[i] = pins[i].transform.position;
            pinInitialDirections[i] = pins[i].transform.forward;
            pinInitialAngles[i] = pins[i].transform.eulerAngles;
            pinInitialVelocities[i] = pins[i].GetComponent<Rigidbody>().linearVelocity;
            pinInitialAngularVelocities[i] = pins[i].GetComponent<Rigidbody>().angularVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time =  Mathf.Repeat(Time.time, repeatTime);

        if (time < 2){
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = initialPosition;
            for (int i = 0; i < pins.Length; i++){
                pins[i].transform.position = pinInitialPositions[i];
                pins[i].transform.forward = pinInitialDirections[i];
                pins[i].transform.eulerAngles = pinInitialAngles[i];
                pins[i].GetComponent<Rigidbody>().linearVelocity = pinInitialVelocities[i];
                pins[i].GetComponent<Rigidbody>().angularVelocity = pinInitialAngularVelocities[i];
            }
            wasThrown = false;
        }
        else{
            if (!wasThrown){
                Vector3 direction = Camera.main.transform.forward;
                rb.linearVelocity = direction * speed;
                wasThrown = true;
            }
            else{
                // do nothing
            }
        }
    }
}

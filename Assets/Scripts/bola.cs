using UnityEngine;

public class bola : MonoBehaviour
{
    public float speed = 10f;
    public float repeatTime = 5f;
    private Rigidbody rb;

    private Vector3 initialPosition;
    private bool wasThrown = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float time =  Mathf.Repeat(Time.time, repeatTime);

        if (time < 2){
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = initialPosition;
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

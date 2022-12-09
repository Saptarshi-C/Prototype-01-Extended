using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float rpm;
    [SerializeField]
    private float horsePower = 0;

    private float turnSpeed = 25f;
    private float horizontalInput;
    private float forwardInput;

    private Rigidbody playerRb;

    [SerializeField]
    private GameObject centerOfMass;

    [SerializeField]
    private TextMeshProUGUI speedometerText;

    [SerializeField]
    private TextMeshProUGUI rpmText;

    [SerializeField]
    private List<WheelCollider> allWheels;
    private int wheelsOnGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make vehicle move

        // Get Input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        if (IsGrounded())
        {
            // Forward movement
            //transform.Translate(Vector3.forward*Time.deltaTime*speed*forwardInput);

            playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput);

            // Side to side turning
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 3.6f);
            rpm = (speed % 30) * 40;

            speedometerText.text = "Speed: " + speed.ToString() + "km/h";
            rpmText.text = "RPM: " + rpm.ToString();
        }
    }

    bool IsGrounded()
    {
        wheelsOnGround = 0;

        foreach(WheelCollider wheel in allWheels)
        {
            if(wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if(wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

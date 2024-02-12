using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public TextMeshProUGUI goldCount;
    public GameObject winText;
    public float speed = 0;
    public float jumpSpeed = 0;
    private int gold;
    bool canJump = true;
    public Vector3 startPosition = new Vector3(0,3,0);


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gold = 0;
        SetCountText();
        winText.SetActive(false);
    }

    void SetCountText()
    {
     goldCount.text = "Gold:" + gold.ToString();
     if(gold == 11) 
     {
        winText.SetActive(true);
     }
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp")) 
        {
          other.gameObject.SetActive(false);
          gold++;
          SetCountText();
        }
    }

    void Update() 
    {
     if(transform.position.y < -40f) 
     {
      transform.position = startPosition;
      rb.velocity = Vector3.zero;
      rb.angularVelocity = Vector3.zero;
      winText.SetActive(false);
      goldCount.text = "Gold: 0";
      gold = 0;
     }   
     if(canJump && Input.GetKey(KeyCode.Space))
     {
        Vector3 atas = new Vector3(0,20,0);
        rb.AddForce(atas * jumpSpeed);
        canJump = false;
     }
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}

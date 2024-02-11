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
    private int gold;

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
}

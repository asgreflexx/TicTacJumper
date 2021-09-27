using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private int score = 0;
    [SerializeField]
    public Text scoreText;
    [SerializeField]
    public Text finishText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";
        finishText.text = "";
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    // FixedUpdate is called once every Physics update
    private void FixedUpdate()
    {
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length == 1)
        {
            return;
        }

        if(jumpKeyWasPressed)
        {
            rigidBodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

       rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("On Trigger Enter");
        Debug.Log(other.tag);
        if(other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            score++;
            scoreText.text = "Score: " + score;

        }

        if (other.gameObject.tag == "Finish")
        {
            StartCoroutine(FinishRoutine());
        }
    }

    IEnumerator FinishRoutine()
    {
        finishText.text = "You have won!";
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

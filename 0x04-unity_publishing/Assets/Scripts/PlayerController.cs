using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 1100f;
    public Rigidbody player;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Image winOrLose;
    public Text winOrLoseText;

    void Update ()
    {
        // if player dies
        if (health == 0)
        {
            winOrLoseText.text = "Game over!";
            ShowWinOrLoseColor(Color.red, Color.white);
            StartCoroutine(LoadScene(3));
        }

        // If player wants menu
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    void FixedUpdate()
    {
        // Press up
        if (Input.GetKey("w"))
        {
            player.AddForce(0, 0, speed * Time.deltaTime);
        }
        // Press down
        if (Input.GetKey("s"))
        {
            player.AddForce(0, 0, -speed * Time.deltaTime);
        }
        // Press left
        if (Input.GetKey("a"))
        {
            player.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        // Press right
        if (Input.GetKey("d"))
        {
            player.AddForce(speed * Time.deltaTime, 0, 0);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        // If the player touches a coin
        if (other.tag == "Pickup")
        {
            score++;
            Destroy(other.gameObject);
            SetScoreText();
        }

        // If player touches a trap
        if (other.tag == "Trap")
        {
            health--;
            SetHealthText();
        }

        // If player wins!
        if (other.tag == "Goal")
        {
            winOrLoseText.text = "You Win!";
            ShowWinOrLoseColor(Color.green, Color.black);
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score " + score;
    }

    void SetHealthText()
    {
        healthText.text = "Health " + health;
    }

    // Show and change wiOrLose game object colors
    void ShowWinOrLoseColor(Color imgColor, Color textColor)
    {
        winOrLose.color = imgColor;
        winOrLoseText.color = textColor;
        winOrLose.gameObject.SetActive(true);
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // To load the actual scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject finishPanel;
    public TextMeshProUGUI finalScoreText;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        UpdateScoreUI();
        finishPanel.SetActive(false);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal * 5f, rb.linearVelocity.y, vertical * 5f);
        rb.linearVelocity = movement;

        if (horizontal != 0 || vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(horizontal, 0, vertical));
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void DecreaseScore(int amount)
    {
        score -= amount;
        if (score < 0) score = 0;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Vehicle"))
        {
            DecreaseScore(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            ShowFinishScreen();
        }
    }

    void ShowFinishScreen()
    {
        finishPanel.SetActive(true);
        finalScoreText.text = "Your Score: " + score;
        Time.timeScale = 0f;
    }
}

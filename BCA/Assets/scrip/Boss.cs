using UnityEngine;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    private bool bossDiee = false;
    public AudioClip bossTake;
    public AudioClip bossDie;
    private AudioSource audioSource;
    public GameObject gameWinPanel;
    public float bossHealth = 100f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealth <= 0f && bossDiee != true)
        {
            bossDiee = true;
            audioSource.PlayOneShot(bossDie);
            gameWinPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D other2D)
    {
        if (other2D.gameObject.CompareTag("Bullet"))
        {
            
            audioSource.PlayOneShot(bossTake);
             bossHealth -= 10f;
            Destroy(other2D.gameObject); // ลบลูกกระสุน
        }
    }

    

}

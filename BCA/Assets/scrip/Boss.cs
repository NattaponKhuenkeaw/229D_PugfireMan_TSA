using UnityEngine;

public class Boss : MonoBehaviour
{
    public float bossHealth = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        if (bossHealth <= 0f)
        {
            Time.timeScale = 0f; // หยุดเวลา
            Destroy(this.gameObject);
            Time.timeScale = 0f; // หยุดเวลา
        }
        
    }

    void OnCollisionEnter2D(Collision2D other2D)
    {
        if (other2D.gameObject.CompareTag("Bullet"))
        {
             bossHealth -= 5f;
            Destroy(other2D.gameObject); // ลบลูกกระสุน
        }
    }
}

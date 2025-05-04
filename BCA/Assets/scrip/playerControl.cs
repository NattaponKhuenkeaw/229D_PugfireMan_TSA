using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject GameOverPanel;
    public Image[] hearts;
    public int maxHealth = 3;
    private int currentHealth;

    public Transform shootPoint;
    public GameObject target;
    public Rigidbody2D bulletPrefab;

    public float speed = 5f;
    public float jumpForce = 150f;

    private bool isJumping = false;
    private float moveInput;
    private Rigidbody2D rb2d;

    void Awake()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // รับอินพุตแนวนอนแบบไม่มีการลื่น (ค่าจะเป็น -1, 0, 1)
        moveInput = Input.GetAxisRaw("Horizontal");

        // การกระโดด
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2d.AddForce(Vector2.up * jumpForce);
            isJumping = true;
        }

        // การยิง
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                target.transform.position = hit.point;

                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);

                Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                firedBullet.linearVelocity = projectileVelocity;
            }
            
        }

        // หมุนตัวตามทิศ
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (transform.position.y < -10)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(moveInput) > 0.01f)
        {
            // เพิ่มแรงเฉพาะเมื่อกดซ้าย-ขวา
            rb2d.AddForce(new Vector2(moveInput * speed, 0), ForceMode2D.Force);
        }
        else
        {
            // ถ้าไม่กด → ลดแรงเฉื่อยลง (ไม่ให้ลื่น)
            Vector2 v = rb2d.linearVelocity;
            v.x *= 0.9f; // ลองปรับเป็น 0.8f หรือ 0.7f ถ้ายังลื่นอยู่
            rb2d.linearVelocity = v;
        }
    }


    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        return new Vector2(velocityX, velocityY);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentHealth--;

        if (currentHealth >= 0 && currentHealth < hearts.Length)
        {
            hearts[currentHealth].enabled = false;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameOverPanel.SetActive(true);
        Destroy(gameObject);
        Time.timeScale = 0f; // หยุดเวลา
    }
}

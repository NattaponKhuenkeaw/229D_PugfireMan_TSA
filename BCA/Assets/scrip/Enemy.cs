using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public float speed = 2f;
    private Transform player;

    

    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            // เคลื่อนที่เข้าหา Player
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                speed * Time.deltaTime
            );
        }
    }

    // ต้องใช้ชื่อเมธอดนี้ให้ถูกต้องตามระบบ 2D
    void OnCollisionEnter2D(Collision2D other2D)
    {
        if (other2D.gameObject.CompareTag("Bullet"))
        {
            
            Destroy(other2D.gameObject); // ลบลูกกระสุน
            Destroy(gameObject);         // ลบศัตรู
        }
        else if (other2D.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);         // ลบศัตรูเมื่อชนผู้เล่น
        }
    }
}
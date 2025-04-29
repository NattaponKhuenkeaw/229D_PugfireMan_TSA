using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerControl : MonoBehaviour
{
    
    public Transform shootPoint;
    
    public GameObject target;
   
    public Rigidbody2D bulletPrefab;
    
    public float yRange  ;

    public float speed;
    
    private float horizontalInput;

    private InputAction moveAction;
    
    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
 
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
 
        return new Vector2(velocityX, velocityY);
    }//
    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        
    }// Start

    // Update 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // แปลงตำแหน่งเมาส์ในจอ เป็นตำแหน่งในโลก 2D
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);
 
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
 
            if (hit.collider != null)
            {
                // ย้าย Target ไปที่ตำแหน่งคลิก
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
 
                // คำนวณความเร็วสำหรับการยิง
                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);
 
                // สร้างกระสุนใหม่
                Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
 
                // ใส่ความเร็วให้กระสุน
                firedBullet.linearVelocity = projectileVelocity;
 
            }//hit.collider
 
        }//GetMouseButtonDown
        
        horizontalInput = moveAction.ReadValue<Vector2>().y;
        transform.Translate(horizontalInput * speed * Time.deltaTime * Vector3.up);
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
    }// Update
}

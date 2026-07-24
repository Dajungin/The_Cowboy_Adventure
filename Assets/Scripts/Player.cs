using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    //플레이어 한테 필요한거
    //이동, 공격, 목숨, 적 , 화면에서 벗어나지 않기

    [Header("Player")]
    public float speed = 5f;
    public int hp = 3; //어떻게 표현할 것인가...

    private Rigidbody2D rb;
    private Vector2 move;
    private Camera cam;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    [Header("Attack")]
    public GameObject bulletPrefab;
    public GameObject firePos;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Start()
    {
        Vector3 min = cam.ViewportToWorldPoint(Vector3.zero);
        Vector3 max = cam.ViewportToWorldPoint(Vector3.one);

        float halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        minX = min.x + halfWidth;
        maxX = max.x - halfWidth;
        minY = min.y + halfHeight;
        maxY = max.y - halfHeight;
    }

    void Update()
    {
        MoveInput();
        LookMouse();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }

    }
    //총알 
    void Shoot()
    {
        Instantiate(
            bulletPrefab,
            firePos.transform.position,
            firePos.transform.rotation
        );
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move.normalized * speed;

        ClampPlayer();
    }

    //이동
    void MoveInput()
    {
        move = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            move.y = 1;

        if (Keyboard.current.sKey.isPressed)
            move.y = -1;

        if (Keyboard.current.aKey.isPressed)
            move.x = -1;

        if (Keyboard.current.dKey.isPressed)
            move.x = 1;
    }

    //화면 밖으로 나가지 않기 
    void ClampPlayer()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
    //마우스 있는 곳 보기
    void LookMouse()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();

        mousePos.z = -cam.transform.position.z;

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        Vector2 dir = worldPos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp--;

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
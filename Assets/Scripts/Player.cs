using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class Player : MonoBehaviour
{
    [Header("Player")]
    public float speed = 5f;
    public int hp = 3;


    private Rigidbody2D rb;
    private Vector2 move;
    private Camera cam;


    private float minX;
    private float maxX;
    private float minY;
    private float maxY;



    [Header("Item")]
    public int bulletCount = 1;

    public float fireDelay = 0.3f;

    private float defaultSpeed;
    private float defaultFireDelay;


    public bool shotgun;
    public bool machineGun;
    public bool sheriff;
    public bool wagonWheel;
    public bool tombStone;



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


        float halfWidth =
            GetComponent<SpriteRenderer>().bounds.extents.x;

        float halfHeight =
            GetComponent<SpriteRenderer>().bounds.extents.y;


        minX = min.x + halfWidth;
        maxX = max.x - halfWidth;

        minY = min.y + halfHeight;
        maxY = max.y - halfHeight;


        defaultSpeed = speed;
        defaultFireDelay = fireDelay;
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



    // =====================
    // °ø°Ý
    // =====================

    void Shoot()
    {

        // ¸¶Â÷ ¹ÙÄû
        if (wagonWheel)
        {
            for (int i = 0; i < 8; i++)
            {
                FireBullet(i * 45);
            }

            return;
        }



        // »êÅºÃÑ
        if (shotgun || sheriff)
        {
            FireBullet(-20);
            FireBullet(0);
            FireBullet(20);

            return;
        }



        // ±âº» °ø°Ý
        FireBullet(0);
    }



    void FireBullet(float angle)
    {
        Quaternion rot =
        Quaternion.Euler(
            0,
            0,
            firePos.transform.eulerAngles.z + angle
        );


        Instantiate(
            bulletPrefab,
            firePos.transform.position,
            rot
        );
    }



    // =====================
    // ÀÌµ¿
    // =====================


    void FixedUpdate()
    {
        rb.linearVelocity =
            move.normalized * speed;


        ClampPlayer();
    }



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



    void ClampPlayer()
    {
        Vector3 pos = transform.position;


        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);


        transform.position = pos;
    }



    void LookMouse()
    {
        Vector3 mousePos =
            Mouse.current.position.ReadValue();


        mousePos.z =
            -cam.transform.position.z;


        Vector3 worldPos =
            cam.ScreenToWorldPoint(mousePos);


        Vector2 dir =
            worldPos - transform.position;


        float angle =
            Mathf.Atan2(dir.y, dir.x)
            * Mathf.Rad2Deg;


        transform.rotation =
            Quaternion.Euler(0, 0, angle);
    }



    // =====================
    // Ãæµ¹
    // =====================


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {

            // ¹¦ºñ È¿°ú
            if (tombStone)
            {
                Destroy(collision.gameObject);
                return;
            }



            hp--;


            Destroy(collision.gameObject);



            if (hp <= 0)
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameOver();
                }


                Destroy(gameObject);
            }
        }
    }





    // =====================
    // ¾ÆÀÌÅÛ È¿°ú
    // =====================


    // »ý¸í Áõ°¡
    public void LifeUp()
    {
        hp++;

        Debug.Log("»ý¸í Áõ°¡");
    }




    // Ä¿ÇÇ
    public void StartCoffee()
    {
        StartCoroutine(Coffee());
    }


    IEnumerator Coffee()
    {
        speed *= 1.5f;


        yield return new WaitForSeconds(16);


        speed = defaultSpeed;
    }





    // ±â°üÃÑ
    public void StartMachineGun()
    {
        StartCoroutine(MachineGun());
    }


    IEnumerator MachineGun()
    {
        machineGun = true;


        fireDelay = 0.08f;


        yield return new WaitForSeconds(12);


        fireDelay = defaultFireDelay;


        machineGun = false;
    }





    // ÇÙÆøÅº
    public void UseNuclearBomb()
    {
        Enemy[] enemies =
        FindObjectsByType<Enemy>
        (FindObjectsSortMode.None);


        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }


        Debug.Log("ÇÙÆøÅº!");
    }





    // »êÅºÃÑ
    public void StartShotGun()
    {
        StartCoroutine(ShotGun());
    }


    IEnumerator ShotGun()
    {
        shotgun = true;


        yield return new WaitForSeconds(12);


        shotgun = false;
    }





    // ¿¬¸·Åº
    public void UseSmokeBomb()
    {
        Enemy[] enemies =
        FindObjectsByType<Enemy>
        (FindObjectsSortMode.None);


        foreach (Enemy enemy in enemies)
        {
            enemy.StopEnemy(2);
        }
    }





    // º¸¾È°ü ¹èÁö
    public void StartSheriffBadge()
    {
        StartCoroutine(SheriffBadge());
    }



    IEnumerator SheriffBadge()
    {
        sheriff = true;


        speed *= 1.3f;

        fireDelay = 0.1f;



        yield return new WaitForSeconds(24);



        sheriff = false;


        speed = defaultSpeed;

        fireDelay = defaultFireDelay;
    }





    // ¹¦ºñ
    public void StartTombStone()
    {
        StartCoroutine(TombStone());
    }



    IEnumerator TombStone()
    {
        tombStone = true;


        speed *= 1.3f;



        yield return new WaitForSeconds(8);



        tombStone = false;


        speed = defaultSpeed;
    }





    // ¸¶Â÷ ¹ÙÄû
    public void StartWagonWheel()
    {
        StartCoroutine(WagonWheel());
    }



    IEnumerator WagonWheel()
    {
        wagonWheel = true;


        yield return new WaitForSeconds(12);


        wagonWheel = false;
    }

}
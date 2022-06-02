using UnityEngine;


public static class Vector2Extensions
{
    public static float GetAngle(this Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}

public class Player : MonoBehaviour
{

    public float ControllersOffTime = 10;
    private float offTimer;


    private Vector2 MovementVector;
    private Rigidbody2D rb;
    public bool IsOffed;

    public float Speed;

    private Animator anim;
    private SpriteRenderer sprite;

    public Sprite[] Sprites;

    public float Surrounds;

    private float offTime;
    private float Count;
    private bool isSlowed;

    void Start()
    {
        IsOffed = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GameObject.Find("standcharacterF").GetComponent<Animator>();
        sprite = GameObject.Find("standcharacterF").GetComponent<SpriteRenderer>();
        rb.mass = 10;
    }

    void FixedUpdate()
    {
        if (!IsOffed)
        {
            var w = Input.GetKey(KeyCode.W) ? 1 : 0;
            var s = Input.GetKey(KeyCode.S) ? -1 : 0;
            var a = Input.GetKey(KeyCode.A) ? -1 : 0;
            var d = Input.GetKey(KeyCode.D) ? 1 : 0;

            MovementVector = new Vector2(a + d, w + s).normalized;

            rb.velocity = MovementVector * Speed;
            rb.angularDrag = 10;

            SetSprite();
            SetAnimationParameters();
        }

        else if (offTime > 0f)
        {
            offTime -= Time.deltaTime;
        }
        else IsOffed = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Portal")
        {
            transform.position = new Vector3(-140f, 4f, -1f);
        }
    }

    private void SetSprite()
    {
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            sprite.sprite = Sprites[0];
            if (rb.velocity.x >= 0) sprite.flipX = false;
            else sprite.flipX = true;
        }

        else if (Mathf.Abs(rb.velocity.y) > 0)
        {
            if (rb.velocity.y >= 0) sprite.sprite = Sprites[1];
            else sprite.sprite = Sprites[2];
        }
    }

    private void SetAnimationParameters()
    {
        anim.SetFloat("SpeedX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("SpeedY", Mathf.Abs(rb.velocity.y));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Surrounds);
    }
    public void OffController()
    {
        IsOffed = true;
        offTime = 20 * Time.deltaTime;
    }

    public void Froze(float count)
    {
        if (!isSlowed)
        {
            Count = count;
            Speed = Speed / count;
        }
        isSlowed = true;
    }

    public void AntiFroze()
    {
        if (isSlowed)
        {
            Speed *= Count;
            Count = 1;
        }
        isSlowed = false;
    }

    public void SetTimer()
    {
        offTimer = ControllersOffTime;
    }
}
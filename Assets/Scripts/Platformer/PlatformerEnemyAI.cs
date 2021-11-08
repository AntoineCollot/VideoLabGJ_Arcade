using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerEnemyAI : MonoBehaviour
{
    public float moveForce = 5;
    public Direction moveDirection = Direction.Left;
    public float moveIntervalMin = 2f;
    public float moveIntervalMax = 4f;
    float moveInterval;
    new Rigidbody2D rigidbody;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        moveInterval = Random.Range(moveIntervalMin, moveIntervalMax);

        PlatformerManager.Instance.onMiniGameStart.AddListener(OnMiniGameStart);
        if (PlatformerManager.Instance.gameIsPlaying)
            OnMiniGameStart();

        UpdateFacingDirection();
    }

    void OnMiniGameStart()
    {
        StartCoroutine(MoveLoop());
    }

    IEnumerator MoveLoop()
    {
        yield return new WaitForSeconds(moveInterval);

        while(PlatformerManager.Instance.gameIsPlaying)
        {
            Move();
            yield return new WaitForSeconds(moveInterval);
        }
    }

    void Move()
    {
        anim.SetTrigger("Move");

        switch (moveDirection)
        {
            case Direction.Right:
                rigidbody.AddForce(Vector2.right * moveForce,ForceMode2D.Impulse);
                break;
            case Direction.Left:
                rigidbody.AddForce(Vector2.left * moveForce,ForceMode2D.Impulse);
                break;
            default:
                break;
        }
    }

    public void SetFacingDirection(Direction dir)
    {
        moveDirection = dir;
        UpdateFacingDirection();
    }

    void UpdateFacingDirection()
    {
        switch (moveDirection)
        {
            case Direction.Right:
                transform.localScale = new Vector3(-1, 1, 1);
                break;
            case Direction.Left:
                transform.localScale = Vector3.one;
                break;
            default:
                break;
        }
    }

    void Kill()
    {
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        rigidbody.isKinematic = true;
        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.collider.CompareTag("Player"))
            return;

        Vector3 toPlayer = collision.collider.transform.position - transform.position;
        if(Vector3.Angle(toPlayer, Vector3.up)<45)
        {
            Kill();
            collision.collider.GetComponent<PlatformerCharacterController>()?.ForceJump();

            AudioManager.PlaySound(SFX.PlatformerEnemyKill);

        }
        else
        {
            //Gameover
            AudioManager.PlaySound(SFX.Hurt);

            PlatformerManager.Instance.MiniGameOver();
        }
    }
}

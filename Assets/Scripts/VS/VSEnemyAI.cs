using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VSEnemyAI : MonoBehaviour
{
    public class AttackEvent : UnityEvent<Direction> { }
    public AttackEvent onAttack = new AttackEvent();
    public float attackDelay = 0.5f;
    public float attackInterval = 0.5f;
    public float pauseTime = 2;
    bool isAttacking = false;

    public int hp = 10;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        VSManager.Instance.onMiniGameStart.AddListener(OnMiniGameStart);
        VSManager.Instance.onMiniGameOver.AddListener(OnMiniGameOver);
        VSManager.Instance.onMiniGameWon.AddListener(OnMiniGameWon);
    }

    private void OnMiniGameWon()
    {
        StopAllCoroutines();
    }

    private void OnMiniGameStart()
    {
        hp = 10;

        StartCoroutine(AttackLoop());
    }

    void OnMiniGameOver()
    {
        hp = 10;
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetHit()
    {
        if (isAttacking)
            return false;

        AudioManager.PlaySound(SFX.VSHit);
        AudioManager.PlaySound(SFX.VSEnemyHurt);

        anim.SetTrigger("Hit");
        hp--;

        if (hp <= 0)
            VSManager.Instance.MiniGameWin();

        return true;
    }

    IEnumerator Attack(Direction direction)
    {
        AudioManager.PlaySound(SFX.VSEnemyAttack);
        isAttacking = true;

        switch (direction)
        {
            case Direction.Top:
                anim.SetTrigger("AttackUp");
                break;
            case Direction.Bottom:
                anim.SetTrigger("AttackDown");
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(attackDelay);

        onAttack.Invoke(direction);
        isAttacking = false;
    }

    IEnumerator AttackLoop()
    {
        while (VSManager.Instance.gameIsPlaying)
        {
            int attackCount = Random.Range(3, 10);

            for (int i = 0; i < attackCount; i++)
            {
                if (Random.Range(0f, 1f) > 0.5f)
                    yield return StartCoroutine(Attack(Direction.Top));
                else
                    yield return StartCoroutine(Attack(Direction.Bottom));

                yield return new WaitForSeconds(attackInterval);
            }
            yield return new WaitForSeconds(pauseTime);
        }
    }
}

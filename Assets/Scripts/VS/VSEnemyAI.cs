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

    public int maxHp = 10;
    public int currentHp = 10;

    public int[] attackSequenceCount;
    int sequenceCount;

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
        currentHp = maxHp;

        StartCoroutine(AttackLoop());
    }

    void OnMiniGameOver()
    {
        currentHp = maxHp;

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
        currentHp--;

        if (currentHp <= 0)
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
        yield return new WaitForSeconds(5);

        while (VSManager.Instance.gameIsPlaying)
        {
            int attackCount = attackSequenceCount[Mathf.Min(sequenceCount, attackSequenceCount.Length - 1)];

            for (int i = 0; i < attackCount; i++)
            {
                if (Random.Range(0f, 1f) > 0.5f)
                    yield return StartCoroutine(Attack(Direction.Top));
                else
                    yield return StartCoroutine(Attack(Direction.Bottom));

                yield return new WaitForSeconds(attackInterval);
            }
            sequenceCount++;
            yield return new WaitForSeconds(pauseTime);
        }
    }
}

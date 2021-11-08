using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSPlayerControls : MonoBehaviour
{
    public float attackDelay = 0.3f;
    public float attackTotalTime = 1;
    bool isAttacking = false;
    Direction blockDirection;

    bool startLock;
    public int hp = 3;

    Animator anim;

    public VSEnemyAI enemy;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        enemy.onAttack.AddListener(GetHit);

        VSManager.Instance.onMiniGameStart.AddListener(OnMiniGameStart);
    }

    private void OnMiniGameStart()
    {
        hp = 3;
        anim.SetBool("HasWon", false);
        startLock = true;
        Invoke("ClearStartLock", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!VSManager.Instance.gameIsPlaying || isAttacking || startLock)
            return;

        if (VSManager.Instance.gameWon)
        {
            anim.SetBool("HasWon", true);
        }

        anim.SetFloat("BlockDirection", 0);
        blockDirection = Direction.None;
        if (Input.GetButton("Action"))
        {
            StartCoroutine(AttackAnim());
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") < -0.1f)
            {
                //Block up
                anim.SetFloat("BlockDirection", 1);
                blockDirection = Direction.Top;
            }
            else if (Input.GetAxisRaw("Vertical") < -0.1f)
            {
                //Block down
                anim.SetFloat("BlockDirection", -1);
                blockDirection = Direction.Bottom;
            }
        }
    }

    public void GetHit(Direction direction)
    {
        StopAllCoroutines();
        isAttacking = false;
        if (blockDirection == direction)
        {
            //Blocked
            AudioManager.PlaySound(SFX.VSBlock);
            return;
        }

        anim.SetTrigger("Hit");
        AudioManager.PlaySound(SFX.VSHit);
        AudioManager.PlaySound(SFX.Hurt);
        hp--;
        if(hp<=0)
        {
            VSManager.Instance.MiniGameOver();
        }
    }

    IEnumerator AttackAnim()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        AudioManager.PlaySound(SFX.VSPlayerAttack);

        yield return new WaitForSeconds(attackDelay);

        enemy.GetHit();

        yield return new WaitForSeconds(attackTotalTime - attackDelay);

        isAttacking = false;
    }

    void ClearStartLock()
    {
        startLock = false;
    }
}

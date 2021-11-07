using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterEnemyAI : MonoBehaviour
{
    Transform player;
    public int hp = 2;

    [Header("Sight")]
    public float sightAngle = 40;
    public LayerMask obstacleLayers = int.MaxValue;
    bool seePlayer = false;

    [Header("Navigation")]
    NavMeshAgent agent;

    [Header("Animations")]
    Animator anim;

    [Header("Fire")]
    public float fireInterval = 2;
    public float fireDelay = 0.75f;
    public ShooterProjectile projectilePrefab;
    public Transform gunOrigin;
    public float projectileSpeed;
    bool isFiring = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("ShooterPlayer").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        ShooterManager.Instance.onMiniGameStart.AddListener(OnMiniGameStart);
        if (ShooterManager.Instance.gameIsPlaying)
            OnMiniGameStart();
        agent.isStopped = true;
    }

    //// Update is called once per frame
    void Update()
    {
        if (isFiring)
            agent.isStopped = true;
        UpdateAnimations();
    }

    void OnMiniGameStart()
    {
        seePlayer = false;
        StartCoroutine(RaycastPlayerLoop());
        StartCoroutine(FireLoop());
    }

    IEnumerator RaycastPlayerLoop()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.3f));
        Ray ray = new Ray();
        Vector3 toPlayer;

        while(ShooterManager.Instance.gameIsPlaying)
        {
            toPlayer = player.position - transform.position;

            //Check if the player is in the line of sight
            if (Vector3.Angle(transform.forward, toPlayer) < sightAngle)
            {
                ray.origin = transform.position;
                ray.direction = toPlayer.normalized;

                //If we don't hit an obstacle on the way to the player
                if (!Physics.Raycast(ray, toPlayer.magnitude-0.25f, obstacleLayers))
                {
                    seePlayer = true;

                    agent.isStopped = false;
                    agent.SetDestination(player.position);
                }
                else
                {
                    seePlayer = false;
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator FireLoop()
    {
        yield return new WaitForSeconds(Random.Range(0f, fireInterval));
        while (ShooterManager.Instance.gameIsPlaying)
        {
            if(seePlayer)
            {
                isFiring = true;
                anim.SetTrigger("Fire");
                yield return new WaitForSeconds(fireDelay);
                ShooterProjectile projectile = Instantiate(projectilePrefab, gunOrigin.position, Quaternion.identity, null);
                projectile.Fire(gameObject, (player.position - transform.position).normalized, projectileSpeed);
                isFiring = false;
                if (seePlayer)
                    agent.isStopped = false;
            }

            yield return new WaitForSeconds(fireInterval);
        }
    }

    void UpdateAnimations()
    {
        if (agent.isStopped)
            anim.SetFloat("MoveSpeed", 0);
        else
            anim.SetFloat("MoveSpeed", 1);

        anim.SetFloat("PlayerDirection", Vector3.Dot((player.position - transform.position).normalized, transform.forward));
    }

    public void GunDamage()
    {
        //If dead, stops
        if (hp <= 0)
            return;
        hp--;
        if (hp <= 0)
        {
            anim.SetBool("IsDead", true);
            GetComponent<Collider>().enabled = false;
            agent.isStopped = true;
            Destroy(gameObject, 1);
        }
    }
}

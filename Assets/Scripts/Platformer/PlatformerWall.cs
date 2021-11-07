using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerWall : MonoBehaviour
{
    public Direction autoSetDirection = Direction.Left;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlatformerEnemyAI enemy = collision.collider.GetComponent<PlatformerEnemyAI>();
        enemy?.SetFacingDirection(autoSetDirection);
    }
}

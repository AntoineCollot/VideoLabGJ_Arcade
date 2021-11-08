using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VSUIHealthBar : MonoBehaviour
{
    public VSPlayerControls player;
    public VSEnemyAI enemy;

    public Image filling;

    // Update is called once per frame
    void Update()
    {
        if(player!=null)
        {
            filling.fillAmount = (float)player.hp / 3f;
        }
        else
        {
            filling.fillAmount = (float)enemy.currentHp / (float)enemy.maxHp;
        }
    }
}

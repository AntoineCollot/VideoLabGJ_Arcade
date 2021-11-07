using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
    Hurt,
    VSEnemyHurt,
    VSBlock,
    VSHit,
    VSPlayerAttack,
    VSEnemyAttack,
    BarrelExplosion,
    ShooterFire,
    ShooterEnemyHurt,
    ShooterWalkStep,
}

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct Clip
    {
        public SFX name;
        public AudioClip clip;
        public float volume;
    }

    public Clip[] clips;
    Dictionary<SFX, Clip> clipDic;

    public AudioSource sfxSource;
    public static AudioManager Instance;

    private void Awake()
    {
        clipDic = new Dictionary<SFX, Clip>();
        
        foreach (Clip clip in clips)
        {
            clipDic.Add(clip.name, clip);
        }

        Instance = this;
    }

    public static void PlaySound(SFX name)
    {
        if(!Instance.clipDic.ContainsKey(name))
        {
            Debug.Log("Couldn't find SFX " + name);
            return;
        }
        Clip clip = Instance.clipDic[name];
        Instance.sfxSource.PlayOneShot(clip.clip, clip.volume);
    }
}

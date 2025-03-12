using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource myAudio;
    public AudioClip SoundBullet;
    public AudioClip soundDie;

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    void Start()
    {
        myAudio = GetComponent<AudioSource>(); //AudioSource 컴포넌트 가져오기
    }

    public void PlayBulletSound()
    {
        myAudio.PlayOneShot(SoundBullet);
    }

    public void SoundDie()
    {
        myAudio.PlayOneShot(soundDie); //몬스터 죽음사운드`
    }
}

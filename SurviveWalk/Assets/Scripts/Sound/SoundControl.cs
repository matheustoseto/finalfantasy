using System.Collections.Generic;
using UnityEngine;

public enum TypeSound { PlayerAttack, EnemyAttack}

[RequireComponent(typeof(AudioListener))]
public class SoundControl : MonoBehaviour {
    [System.Serializable]
    private class AudioStruct {
        public string name;
        public TypeSound typeSound;
        public AudioClip audioClip;
    }

    private static SoundControl instance = null;
    private AudioSource ambient;
    private AudioSource background;
    private AudioSource[] effects;
    private AudioSource audioSourceCurrent;

    [SerializeField] private List<AudioStruct> listAudios;

    private const int firstOfTheList = 0;

    public static SoundControl GetInstance()
    {
        return instance;

    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        ambient = transform.Find("Ambient").GetComponent<AudioSource>();
        background = transform.Find("Background").GetComponent<AudioSource>();
        effects = transform.Find("Effects").GetComponentsInChildren<AudioSource>();

    }

    // Use this for initialization
    void Start () {
        #region ListAudios
        for (int i = 0; i < listAudios.Count; i++)
        {
            listAudios[i].name = listAudios[i].typeSound.ToString();
        }
        #endregion


    }

    // Update is called once per frame
    void Update () {
		
	}



    public void ExecuteEffect(TypeSound audioEffect)
    {

        audioSourceCurrent = EffectsFree();
        audioSourceCurrent.clip = listAudios.Find(audio => audio.name == audioEffect.ToString()).audioClip;
        audioSourceCurrent.Play();
    }

    private AudioSource EffectsFree()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            if (!effects[i].isPlaying)
            {
                return effects[i];
            }
        }

        effects[firstOfTheList].Stop();
        return effects[firstOfTheList];
    }
}

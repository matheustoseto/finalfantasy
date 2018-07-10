using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeSound {None, PlayerAttack, EnemyAttack, ArmaFinal, Cidade, Coleta, ColetaMetais, Desolacao, Erro, Floresta, ForjaItens, RisadaBoss , ColetaArvore , ColetaDefault }

[RequireComponent(typeof(AudioListener))]
public class SoundControl : MonoBehaviour {
    [System.Serializable]
    private class AudioStruct {
        public string name;
        public bool isLoop;
        public TypeSound typeSound;
        public AudioClip audioClip;
    }


    

    private static SoundControl instance = null;
    private AudioSource ambient;
    private AudioSource background;
    private AudioSource[] effects;
    private AudioSource audioSourceCurrent;


    [Header("Audio Settings:")]
    [SerializeField] private List<AudioStruct> listAudios;

    private const int firstOfTheList = 0;
    private TypeSound actualAmbientSound = TypeSound.None;


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

        ExecuteAmbient(TypeSound.Desolacao,1f);

    }

    public AudioSource ExecuteEffect(TypeSound audioEffect)
    {
        AudioStruct clip = listAudios.Find(audio => audio.name == audioEffect.ToString()); 
        audioSourceCurrent = EffectsFree();
        audioSourceCurrent.clip = clip.audioClip;
        audioSourceCurrent.loop = clip.isLoop;
        audioSourceCurrent.Play();

        return audioSourceCurrent;
    }

    public void ExecuteStop(AudioSource audio)
    {
        if (audio != null)
        {
            audio.Stop();
        }
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

    public void ExecuteAmbient(TypeSound audioAmbient, float fadeoutVolumeSpeed)
    {
        if(actualAmbientSound == TypeSound.None)
        {
            if (audioAmbient != TypeSound.None)
            {
                actualAmbientSound = audioAmbient;
                ambient.clip = listAudios.Find(audio => audio.name == audioAmbient.ToString()).audioClip;
                ambient.Play();
            }
            return;
        }

        if (actualAmbientSound == audioAmbient && ambient.isPlaying)
            return;

        if(audioAmbient == TypeSound.None)
        {
            actualAmbientSound = TypeSound.None;
            ambient.Stop();
            ambient.clip = null;

            ExecuteEffect(TypeSound.None);
            return;
        }

        actualAmbientSound = audioAmbient;
        StartCoroutine(Fadeout(audioAmbient, fadeoutVolumeSpeed));
    }

    IEnumerator Fadeout(TypeSound audioAmbient, float fadeoutVolumeSpeed)
    {
        float vol = ambient.volume;
        while (ambient.volume > 0.01f)
        {
            ambient.volume -= fadeoutVolumeSpeed;
            yield return new WaitForSeconds(fadeoutVolumeSpeed);
        }

        ambient.clip = listAudios.Find(audio => audio.name == audioAmbient.ToString()).audioClip;
        ambient.Play();
        ambient.volume = vol;
    }

}

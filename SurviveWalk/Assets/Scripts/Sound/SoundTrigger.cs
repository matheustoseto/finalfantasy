using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {

    private SoundControl soundControl = null;

    [SerializeField] private TypeSound typeSound = TypeSound.Cidade;
    [SerializeField] private float fadeoutVolumeSpeed = 0.01f;


	// Use this for initialization
	void Start () {
        soundControl = SoundControl.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            soundControl.ExecuteAmbient(typeSound,fadeoutVolumeSpeed);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

    public Text coordenadas;

    void Update () {
		if(PlayerManager.Instance.player != null)
			coordenadas.text = "X: " + Mathf.RoundToInt(PlayerManager.Instance.player.transform.position.x) + " Y: " + Mathf.RoundToInt(PlayerManager.Instance.player.transform.position.z);
    }
}

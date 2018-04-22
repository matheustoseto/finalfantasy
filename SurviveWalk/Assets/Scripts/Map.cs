using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {

    public GameObject player;
    public Text coordenadas;

	void Update () {
        coordenadas.text = "X: " + Mathf.RoundToInt(player.transform.position.x) + " Y: " + Mathf.RoundToInt(player.transform.position.z);
    }
}

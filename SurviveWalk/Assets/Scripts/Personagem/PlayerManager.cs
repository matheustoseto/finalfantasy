using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public GameObject player;

    private static PlayerManager instance = null;
    public static PlayerManager Instance { get { return instance; } }

    private void Start()
    {
        instance = this;
        player = this.gameObject;
    }

    void Awake()
    {
        instance = this;
        player = this.gameObject;
    }
}

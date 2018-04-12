using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour {

    [SerializeField] private CharacterAttribute hp = new CharacterAttribute();
    [SerializeField] private CharacterAttribute spaceSuit = new CharacterAttribute();
    private CharacterAttribute[] attribs = new CharacterAttribute[2];

    public CharacterAttribute HP { get { return hp; } }
    public CharacterAttribute SpaceSuit { get { return spaceSuit; } }
    // Use this for initialization
    void Start () {
        attribs[0] = hp;
        attribs[1] = spaceSuit;
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < attribs.Length; i++)
            if(attribs[i] != null)
                attribs[i].Update();
	}
}

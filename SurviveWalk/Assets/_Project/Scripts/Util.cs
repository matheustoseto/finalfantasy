using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeSeach { tag, name}

public class Util {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public static Transform SearchChildren(Transform transformtObj, string strCompare, TypeSeach typeSearch )
    {
        Transform[] children = transformtObj.GetComponentsInChildren<Transform>();

        Transform gobj = null;


        for (int i = 0; i < children.Length; i++)
        {
            // Tags
            if (Compare(children[i], strCompare, typeSearch))
            {
                gobj = children[i];
                break;
            }
        }

        return gobj;
    }

    private static bool Compare(Transform transformtObj, string strCompare, TypeSeach typeSearch)
    {
        switch (typeSearch)
        {
            case TypeSeach.tag:  return transformtObj.tag == strCompare;
            case TypeSeach.name: return transformtObj.name == strCompare;
            default:
                return false;
        }
    }


    public static Transform SearchFatherOfAll(Transform children)
    {
        Transform father = children.parent;
        Transform fatherOfAll = null;
        while (father != null)
        {
            Transform fatherCandidate = father.GetComponent<Transform>();
            if (fatherCandidate != null)
            {
                fatherOfAll = fatherCandidate;
                break;
            }
            father = father.parent;
        }
        return fatherOfAll;
    }

}

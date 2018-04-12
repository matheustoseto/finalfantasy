using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private List<Transform> distancePoints = new List<Transform>();

    public Transform GetTransform()
    {
        return transform;
    }

    public List<Transform> ListDistancePoints()
    {
        return distancePoints;
    }
}

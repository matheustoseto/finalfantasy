using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private List<Transform> distancePoints = new List<Transform>();

    private IcarusController player;

    public IcarusController Player { get { return player; } }

    void Awake()
    {
        player = GetComponent<IcarusController>();
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public List<Transform> ListDistancePoints()
    {
        return distancePoints;
    }
}

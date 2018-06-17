using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : PlayerManager
{
    [SerializeField] private List<Transform> distancePoints = new List<Transform>();

    private IcarusController icarusPlayer;
    public IcarusController IcarusPlayer { get { return icarusPlayer; } }

    void Awake()
    {
        icarusPlayer = GetComponent<IcarusController>();
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

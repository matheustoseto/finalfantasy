using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseItem : MonoBehaviour {

    public int Id { get; set; }
    public int IdLevel { get; set; }
    public List<Combination> Combination = new List<Combination>();
}

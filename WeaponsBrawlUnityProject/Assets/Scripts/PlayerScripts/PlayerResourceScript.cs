using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceScript : MonoBehaviour {
    public int resources;

    public void addResouces(int amount)
    {
        resources += amount;
    }
}

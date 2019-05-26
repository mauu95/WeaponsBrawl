using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourceScript : MonoBehaviour {
    public int resources=100000;

    public void addResouces(int amount)
    {
        resources += amount;
    }

    public bool UseResource(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            return true;
        }
        return false;
    }
}

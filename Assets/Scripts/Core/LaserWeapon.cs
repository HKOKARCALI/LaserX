using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LaserWeapon :IComparable<LaserWeapon>
{
    // Start is called before the first frame update
    public string name;
    public float damage;
    public float range;
    public float battery;
    public float cooling;

    public LaserWeapon(string name, float damage, float range, float battery, float cooling)
    {
        name = name;
        damage = damage;
        range = range;
        battery = battery;
        cooling = cooling;
    }

    public int CompareTo(LaserWeapon other)
    {
        if (other == null)
        {
            return 1;
        }
        //Return the difference in power.
        return (int)damage - (int)other.damage;
    }
}

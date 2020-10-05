using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Powered : MonoBehaviour
{
    public int requiredPower = 1;
    public bool powered = false;

    List<GameObject> powerSources = new List<GameObject>();

    public void AddPower(GameObject obj)
    {
        if (!this.powerSources.Contains(obj))
        {
            this.powerSources.Add(obj);
        }

        this.CheckPower();
    }

    public void RemovePower(GameObject obj)
    {
        if (this.powerSources.Contains(obj))
        {
            this.powerSources.Remove(obj);
        }

        this.CheckPower();
    }

    void CheckPower()
    {
        if (this.powerSources.Count >= this.requiredPower && !powered)
        {
            this.powered = true;
            this.OnPowered();
        }

        if (this.powerSources.Count < this.requiredPower && powered)
        {
            this.powered = false;
            this.OnUnpowered();
        }
    }

    public virtual void OnPowered()
    {

    }

    public virtual void OnUnpowered()
    {

    }
}

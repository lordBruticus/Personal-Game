using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public List<int> livingTargetPositions = new List<int>();
    public List<int> livingTargetsTypes = new List<int>();

    public int waves = 0;
    public int kills = 0;
}

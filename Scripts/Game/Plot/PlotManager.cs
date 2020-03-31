using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public struct PlotYield
{
    public PlotType PlotType;
    public int Harvest;
}

public class PlotManager : MonoBehaviour
{
    //1
    public static PlotManager Instance;

    //2
    public GameObject plantsPrefab;

    //3
    public List<PlotYield> PlotYields = new List<PlotYield>();
    //4
    void Awake()
    {
        Instance = this;
    }
    //5
    public void CreateNewPlot(GameObject slotToFill, PlotType plotType)
    {
        switch (plotType)
        {
            case PlotType.Plant:
                Instantiate(plantsPrefab, slotToFill.transform.position, Quaternion.identity);
                slotToFill.gameObject.SetActive(false);
                break;
        }
    }
    //6
    public int GetPlotYield(PlotType plotType)
    {
        return (from plotYield in PlotYields where plotYield.PlotType == plotType select plotYield.Harvest).FirstOrDefault();
    }
}

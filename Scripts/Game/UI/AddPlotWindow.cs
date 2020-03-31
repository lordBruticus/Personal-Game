using System;
using UnityEngine;
using System.Collections;

public class AddPlotWindow : MonoBehaviour
{
    //1
    public GameObject plotSlotToAddPlotTo;
    //2
    public void AddPlot(string plotTypeAsString)
    {
        //3
        PlotType type = (PlotType)Enum.Parse(typeof(PlotType), plotTypeAsString, true);
        //4
        if (PlotManager.Instance.GetPlotYield(type) <= GameManager.Instance.money)
        {
            //5
            GameManager.Instance.money -= PlotManager.Instance.GetPlotYield(type);
            //6
            PlotManager.Instance.CreateNewPlot(plotSlotToAddPlotTo, type);
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CodeComplianceChecker
{

    class CheckAllStairs
    {
        

        public void CheckMyStairs(Document doc)
        {
            Stairs stair = null;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<ElementId> stairsIds = collector.WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Stairs).ToElementIds();
            List<double> stairWidthList = new List<double>();


            foreach (var stairId in stairsIds)
            {
                if (Stairs.IsByComponent(doc, stairId) == true)
                {
                    stair = doc.GetElement(stairId) as Stairs;

                    //Autodesk.Revit.DB.Architecture.Stairs stair ;
                    StairsRun sRun = doc.GetElement(stair.GetStairsRuns().First()) as StairsRun;
                    double StairWidth = sRun.ActualRunWidth;
                    double StairWidthmm = UnitUtils.ConvertFromInternalUnits(StairWidth, DisplayUnitType.DUT_MILLIMETERS);
                    stairWidthList.Add(StairWidthmm);

                   // string info = "*\nNumber of stories: " + stair.Height;
                   // TaskDialog.Show("Revit", info);
                }
                
            }

            StreamWriter file = new StreamWriter("Stairs.txt", true);
            foreach (var item in stairWidthList)
            {
                string stairwidthinmm = "Width of stair is {0} mm" + item;
                file.WriteLine(item);
            }
            file.Close();

        }
        

        public void GetStairInfo(Document document)
        {
            Stairs stairs = null;

            FilteredElementCollector collector = new FilteredElementCollector(document);
            ICollection<ElementId> stairsIds = collector.WhereElementIsNotElementType().OfCategory(BuiltInCategory.OST_Stairs).ToElementIds();
            foreach (ElementId stairId in stairsIds)
            {
                if (Stairs.IsByComponent(document, stairId) == true)
                {
                    stairs = document.GetElement(stairId) as Stairs;

                    // Format the information
                    String info = "\nNumber of stories:  " + stairs.NumberOfStories;
                    info += "\nHeight of stairs:  " + stairs.Height;
                    info += "\nNumber of treads:  " + stairs.ActualTreadsNumber;
                    info += "\nTread depth:  " + stairs.ActualTreadDepth;

                    // Show the information to the user.
                    TaskDialog.Show("Revit", info);
                }
            }

           // return stairs;
        }
    }
}

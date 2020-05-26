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
    [Transaction(TransactionMode.Manual)]
    class CheckStairs
    {
        public void CheckModelStairs(Document doc, UIDocument uidoc)
        {
            Stairs stair = null;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<ElementId> stairsIds = collector.WhereElementIsElementType().OfCategory(BuiltInCategory.OST_Stairs).ToElementIds();

            foreach (var stairId in stairsIds)
            {
  
                stair = doc.GetElement(stairId) as Stairs;



                //Autodesk.Revit.DB.Architecture.Stairs stair ;
                StairsRun sRun = doc.GetElement(stair.GetStairsRuns().First()) as Autodesk.Revit.DB.Architecture.StairsRun;
                double StairWidth = sRun.ActualRunWidth;
                /*
                var stairrCollector = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance));
                stairrCollector.OfCategory(BuiltInCategory.OST_Stairs);
                IList<Element> stairList = stairrCollector.ToElements();

                List<Double> passedStairs = new List<Double>();
                List<Double> failedStairs = new List<Double>();
                List<ElementId> passedStairsId = new List<ElementId>();
                List<ElementId> failedStairsId = new List<ElementId>();

                foreach (Element mystair in stairList)
                {
                    //Instance param
                    Parameter parameter = mystair.get_Parameter(BuiltInParameter.STAIRS_RUN_ACTUAL_RUN_WIDTH);
                    Parameter stairInstparam = parameter;
                    //string InstStorage = doorInstparam.StorageType.ToString();
                    //Parameter parameter = door.LookupParameter("width");

                    double stairInstWidth = (stairInstparam.HasValue) ? stairInstparam.AsDouble() : 0;
                    //double doorWidthParam = parameter.AsDouble();

                    //type param
                    ElementId stairTypeId = mystair.GetTypeId();
                    ElementType stairType = (ElementType)doc.GetElement(stairTypeId);
                    Parameter stairTypeParam = stairType.get_Parameter(BuiltInParameter.FAMILY_WIDTH_PARAM);

                    //string typeStorage = doorInstparam.StorageType.ToString();

                    double stairTypeWidth = (stairTypeParam.HasValue) ? stairTypeParam.AsDouble() : 0;


                    double stairInstWidthmm = UnitUtils.ConvertFromInternalUnits(stairInstWidth, DisplayUnitType.DUT_MILLIMETERS);
                    double stairTypeWidthmm = UnitUtils.ConvertFromInternalUnits(stairTypeWidth, DisplayUnitType.DUT_MILLIMETERS);

                    double ttldoorWidth = new double();
                    if (stairInstWidthmm == 0)
                    {
                        ttldoorWidth = stairTypeWidthmm;
                    }
                    else
                    {
                        ttldoorWidth = stairInstWidthmm;
                    }

                    if (ttldoorWidth <= 800)
                    {
                        ElementId failstairId = mystair.GetTypeId();
                        failedStairsId.Add(failstairId);
                        failedStairs.Add(ttldoorWidth);
                    }
                    else
                    {
                        ElementId passstairId = mystair.GetTypeId();
                        passedStairsId.Add(passstairId);
                        passedStairs.Add(ttldoorWidth);
                    }
                }

                 }
                 */
                //#region Write to text file
                StreamWriter File = new StreamWriter("FailedStairs.txt");
                // foreach (Double item in failedStairs)
                //{

                File.WriteLine(StairWidth);
                //}
                File.Close();
                /*
                            StreamWriter file = new StreamWriter("PassedStairs.txt");
                            foreach (Double item in passedStairs)
                            {
                                file.WriteLine(item);
                            }
                            file.Close();
                            #endregion


                    /*
                            Stairs stairs = null;

                                FilteredElementCollector collector = new FilteredElementCollector(doc);
                                ICollection<ElementId> stairsIds = collector.WhereElementIsElementType().OfCategory(BuiltInCategory.OST_Stairs).ToElementIds();
                                foreach (ElementId stairId in stairsIds)
                                {
                                    if (Stairs.IsByComponent(doc, stairId) == true)
                                    {
                                        stairs = doc.GetElement(stairId) as Stairs;

                                        // Format the information
                                        String info = "\nNumber of stories:  " + stairs.NumberOfStories;
                                        info += "\nHeight of stairs:  " + stairs.Height;
                                        info += "\nNumber of treads:  " + stairs.ActualTreadsNumber;
                                        info += "\nTread depth:  " + stairs.ActualTreadDepth;
                                        info += "\nStair width:  " + stairs;

                                    // Show the information to the user.
                                    TaskDialog.Show("Revit", info);
                                    }
                                }
                                */

                // return stairs;

            }
        }
    }
}
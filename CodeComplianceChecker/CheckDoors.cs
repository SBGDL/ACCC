using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeComplianceChecker
{
    public class CheckDoors
    {
        public void CheckModelDoors(Document doc, Application app)
        {

            #region Set Collector and Filter
            /*
            // set collector
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            // set filter
            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);
            */
            #endregion

            var doorCollector = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance));
            // FilteredElementCollector a = new FilteredElementCollector(doc).OfClass(typeof(SpatialElement));

            doorCollector.OfCategory(BuiltInCategory.OST_Doors);
            IList<Element> doorList = doorCollector.ToElements();


            #region Set Filter on Collector
            // set filter on the collector
            //IList<Element> typedoors = collector.WherePasses(filter).WhereElementIsElementType().ToElements();
            //IList<Element> instdoors = collector.WherePasses(filter).WhereElementIsNotElementType().ToElements();
            #endregion

            #region Get Doors and Check their Width

            List<Double> passedDoors = new List<Double>();
            List<Double> failedDoors = new List<Double>();
            List<ElementId> failedDoorId = new List<ElementId>();
            List<ElementId> passedDoorId = new List<ElementId>();

            foreach (Element door in doorList)
            {
                //Instance param
                Parameter doorInstparam = door.get_Parameter(BuiltInParameter.DOOR_WIDTH);
                //string InstStorage = doorInstparam.StorageType.ToString();
                //Parameter parameter = door.LookupParameter("width");

                double doorInstWidth = (doorInstparam.HasValue) ? doorInstparam.AsDouble() : 0;
                //double doorWidthParam = parameter.AsDouble();

                //type param
                ElementId doorTypeId = door.GetTypeId();
                ElementType doorType = (ElementType)doc.GetElement(doorTypeId);
                Parameter doorTypeParam = doorType.get_Parameter(BuiltInParameter.FAMILY_WIDTH_PARAM);

                //string typeStorage = doorInstparam.StorageType.ToString();

                double doorTypeWidth = (doorTypeParam.HasValue) ? doorTypeParam.AsDouble() : 0;


                double doorInstWidthmm = UnitUtils.ConvertFromInternalUnits(doorInstWidth, DisplayUnitType.DUT_MILLIMETERS);
                double doorTypeWidthmm = UnitUtils.ConvertFromInternalUnits(doorTypeWidth, DisplayUnitType.DUT_MILLIMETERS);

                double ttldoorWidth = new double();
                if (doorInstWidthmm == 0)
                {
                    ttldoorWidth = doorTypeWidthmm;
                }
                else
                {
                    ttldoorWidth = doorInstWidthmm;
                }

                if (ttldoorWidth <= 800)
                {
                    ElementId faildoorId = door.GetTypeId();
                    failedDoorId.Add(faildoorId);
                    failedDoors.Add(ttldoorWidth);
                }
                else
                {
                    ElementId passdoorId = door.GetTypeId();
                    passedDoorId.Add(passdoorId);
                    passedDoors.Add(ttldoorWidth);
                }
            }

            #endregion

            #region Write to text file
            //if ((!File.Exists("FailedDoors.txt")))
            //{
            StreamWriter File = new StreamWriter("FailedDoors.txt");
            for (int i = 0; i < failedDoors.Count; i++)
            {
                string fdtxt = "The Door Width is " + failedDoors[i];
                File.WriteLine(fdtxt);
                string fdIdtxt = "The Door Id is " + failedDoorId[i];
                File.WriteLine(fdIdtxt);
                File.WriteLine("++++++++++++++++++++++++++++++");
            }

            File.Close();

            StreamWriter file = new StreamWriter("PassedDoors.txt");
            for (int i = 0; i < passedDoors.Count; i++)
            {
                string pdtxt = "The Door Width is " + passedDoors[i];
                File.WriteLine(pdtxt);
                string pdIdtxt = "The Door Id is " + passedDoorId[i];
                File.WriteLine(pdIdtxt);
                File.WriteLine("++++++++++++++++++++++++++++++");
            }

            file.Close();

            //}
            #endregion


            TaskDialog.Show("Door Width", "The failed Doors are saved in a text file");



        }

    }

}

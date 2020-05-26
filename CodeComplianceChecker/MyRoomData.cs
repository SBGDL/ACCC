using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;

namespace CodeComplianceChecker
{
    class MyRoomData
    {

        public void ListRoomData(Document doc)
        {
            FilteredElementCollector a = new FilteredElementCollector(doc).OfClass(typeof(SpatialElement));
            /// <summary>
            /// List some properties of a given room to the 
            /// Visual Studio debug output window.
            /// </summary>
            /// 
            List<string> roomName = new List<string>();
            List<string> roomNumber = new List<string>();
            List<double> roomArea = new List<double>();

            foreach (SpatialElement e in a)
            {
                Room room = e as Room;

                SpatialElementBoundaryOptions opt = new SpatialElementBoundaryOptions();

                string nr = room.Number;
                roomNumber.Add(nr);
                string name = room.Name;
                roomName.Add(name);
                double area = room.Area;
                roomArea.Add(area);

                Location loc = room.Location;
                LocationPoint lp = loc as LocationPoint;
                XYZ p = (null == lp) ? XYZ.Zero : lp.Point;

                BoundingBoxXYZ bb = room.get_BoundingBox(null);

                IList<IList<BoundarySegment>> boundary
                  = room.GetBoundarySegments(opt);

                int nLoops = boundary.Count;

                int nFirstLoopSegments = 0 < nLoops
                  ? boundary[0].Count
                  : 0;

                /*
                Debug.Print(string.Format(
                  "Room nr. '{0}' named '{1}' at {2} with "
                  + "bounding box {3} and area {4} sqf has "
                  + "{5} loop{6} and {7} segment{8} in first "
                  + "loop.",
                  nr, name, Util.PointString(p),
                  BoundingBoxString2(bb), area, nLoops,
                  Util.PluralSuffix(nLoops), nFirstLoopSegments,
                  Util.PluralSuffix(nFirstLoopSegments)));
                 */


                #region Write to text file
                StreamWriter File = new StreamWriter("RoomProp.txt");
                for (int i = 0; i < roomName.Count; i++)
                {
                    File.WriteLine(roomName[i]);
                    File.WriteLine(roomNumber[i]);
                    File.WriteLine(roomArea[i]);
                    File.WriteLine("+++++++++++++++++++++++++++++++++++++++++++");
                }
                File.Close();
                #endregion


            }
            TaskDialog.Show("Room Data", "All room data are saved in a text file");

        }
    }
}

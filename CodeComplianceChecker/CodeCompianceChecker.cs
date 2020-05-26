using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;



namespace CodeComplianceChecker
{
    [Transaction(TransactionMode.Manual)]
    class mycommands : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            UIApplication uiapp = commandData.Application;
            Application app = uiapp.Application;
            DefinitionFile defFiel = app.OpenSharedParameterFile();

            /*
            #region Add Instance Parameter to Revit
            CreateSharedParam scsp = new CreateSharedParam();
            scsp.CreateSampleSharedParameters(doc, app);
            #endregion
            */
            /*
            #region Check Doors
            CheckDoors chckdoor = new CheckDoors();
            chckdoor.CheckModelDoors(doc, app);
            #endregion
           
            /*
            #region Access Room Data
            MyRoomData roomData = new MyRoomData();
            roomData.ListRoomData(doc);
            #endregion
            */

            #region Check Stairs
            CheckAllStairs chckstair = new CheckAllStairs();
            // CheckStairs chckstair = new CheckStairs();
            chckstair.CheckMyStairs(doc);
            //chckstair.GetStairInfo(doc);
            #endregion
            
            /*
            #region Add Instance Parameter 
            AddInstanceParameter addinstparam = new AddInstanceParameter();
            addinstparam.SetNewParameterToInstanceWall(uiapp, defFiel);
            #endregion
            */
            /*
            #region Add Shared Parameter
            SampleCreateSharedParameter shrdparam = new SampleCreateSharedParameter();
            shrdparam.CreateSampleSharedParameters(doc, app);
            #endregion
            */
            return Result.Succeeded;


        }
    }
}

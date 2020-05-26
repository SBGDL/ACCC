using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CodeComplianceChecker
{
    class AddInstanceParameter
    {
        public bool SetNewParameterToInstanceWall(UIApplication app, DefinitionFile myDefinitionFile)
        {
            if (myDefinitionFile == null) throw new Exception("No SharedParameter File!");
            // create a new group in the shared parameters file
            DefinitionGroups myGroups = myDefinitionFile.Groups;
            DefinitionGroup myGroup = myGroups.Create("MyParameters1");

            // create an instance definition in definition group MyParameters
            ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions("Instance_ProductDate", ParameterType.Text);
            // Don't let the user modify the value, only the API
            option.UserModifiable = false;
            // Set tooltip
            option.Description = "Wall product date";
            Definition myDefinition_ProductDate = myGroup.Definitions.Create(option);

            // create a category set and insert category of wall to it
            CategorySet myCategories = app.Application.Create.NewCategorySet();
            // use BuiltInCategory to get category of wall
            Category myCategory = Category.GetCategory(app.ActiveUIDocument.Document, BuiltInCategory.OST_Walls);


            myCategories.Insert(myCategory);

            //Create an instance of InstanceBinding
            InstanceBinding instanceBinding = app.Application.Create.NewInstanceBinding(myCategories);

            // Get the BingdingMap of current document.
            BindingMap bindingMap = app.ActiveUIDocument.Document.ParameterBindings;

            // Bind the definitions to the document
            bool instanceBindOK = bindingMap.Insert(myDefinition_ProductDate,
                                            instanceBinding, BuiltInParameterGroup.PG_TEXT);
            return instanceBindOK;
        }
    }
}

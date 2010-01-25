using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;
using System.IO;

namespace CR_TestDrivenDevelopmentTools
{
    public partial class TestDrivenDevelopmentTools : StandardPlugIn
    {
        private DeclareClassInSpecificProject _declareClassInProject;
        private MoveTypeToFileInSpecificProject _moveTypeToFile;

        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();
        }

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            _moveTypeToFile = new MoveTypeToFileInSpecificProject(new ProjectServices(), new NamespaceServices());
            _declareClassInProject = new DeclareClassInSpecificProject(new ProjectServices());
        }

        private void DeclareClassInSpecificProject_Apply(object sender, ApplyContentEventArgs ea)
        {
            string selectedProjectName = ea.SelectedSubMenuItem.Name;

            CodeRush.UndoStack.BeginUpdate("Declare Class in Project");
            CodeRush.Markers.Drop();

            _declareClassInProject.Apply(ea, selectedProjectName);

        }

        private void DeclareClassInSpecificProject_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = _declareClassInProject.IsAvailable(ea);

            if (ea.Available == false)
                return;

            ea.MenuCaption = "Declare Class In Project...";

            _declareClassInProject.AddProjectsToSubmenu(ea);
        }

        private void MoveTypeToFileInSpecificProjectProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            string selectedProjectName = ea.SelectedSubMenuItem.Name;

            CodeRush.UndoStack.BeginUpdate("Move Type To File in Project");
            CodeRush.Markers.Drop();

            string filename = _moveTypeToFile.Apply(ea, selectedProjectName);

            CodeRush.File.Activate(filename);
            CodeRush.Documents.Format();
        }

        private void MoveTypeToFileInSpecificProjectProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = _moveTypeToFile.IsAvailable(ea);

            if (ea.Available == false)
                return;

            ea.MenuCaption = "Move Type To File In Project...";

            _moveTypeToFile.AddProjectsToSubmenu(ea);
        }

    }
}
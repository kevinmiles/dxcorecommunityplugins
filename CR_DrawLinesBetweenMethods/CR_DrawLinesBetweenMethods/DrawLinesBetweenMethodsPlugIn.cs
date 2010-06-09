using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;
using DevExpress.DXCore.Adornments;

namespace CR_DrawLinesBetweenMethods
{
    /// <summary>
    /// Summary description for DrawLinesBetweenMethodsPlugIn.
    /// </summary>
    public class DrawLinesBetweenMethodsPlugIn : StandardPlugIn
    {
        #region Component Designer generated code
        public DrawLinesBetweenMethodsPlugIn()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DrawLinesBetweenMethodsPlugIn
            // 
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        // CodeRush-generated code
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            EventNexus.DecorateLanguageElement += EventNexus_DecorateLanguageElement;

            base.InitializePlugIn();
        }
        #endregion

        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            EventNexus.DecorateLanguageElement -= EventNexus_DecorateLanguageElement;

            base.FinalizePlugIn();
        }
        #endregion

        void EventNexus_DecorateLanguageElement(object sender, DecorateLanguageElementEventArgs args)
        {
            //Debug.WriteLine("EventNexus_DecorateLanguageElement");

            var settings = DrawLinesBetweenMethodsSettings.Current;

            if (!settings.Enabled)
                return;

            //CodeRush.Documents.ActiveTextDocument.ge
            if ((args.LanguageElement is Class) && settings.EnableOnClass
                || (args.LanguageElement is Property) && settings.EnableOnProperty
                || (args.LanguageElement is Method) && settings.EnableOnMethod
                || (args.LanguageElement is Enumeration) && settings.EnableOnEnum)
            {
                LanguageElement langElement = args.LanguageElement;
                //Debug.WriteLine("langElement: " + langElement);

                // TODO: Skip up over Comment, AttributeSection, XmlDocComment

                //Debug.WriteLine(" > AddBackgroundAdornment ...");
                var adornment = new DrawLinesBetweenMethodsDocumentAdornment(langElement.Range);
                args.AddBackgroundAdornment(adornment);
            }
        }

    }
}
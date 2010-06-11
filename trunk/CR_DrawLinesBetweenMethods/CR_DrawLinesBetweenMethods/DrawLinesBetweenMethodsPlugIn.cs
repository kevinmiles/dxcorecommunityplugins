using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;
using DevExpress.DXCore.Adornments;
using System.Linq;
using System.Collections.Generic;

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

            LanguageElement langElement = args.LanguageElement;

            //CodeRush.Documents.ActiveTextDocument.ge
            if ((langElement is Class) && settings.EnableOnClass
                || (langElement is Property) && settings.EnableOnProperty
                || (langElement is Method) && settings.EnableOnMethod
                || (langElement is Enumeration) && settings.EnableOnEnum)
            {
                //Debug.WriteLine("langElement: " + langElement);

                // Skip up over Comment, AttributeSection, XmlDocComment
                var commentsAndStuff = previousSiblings(langElement)
                    .TakeWhile(sibling => sibling is Comment || sibling is XmlDocComment || sibling is AttributeSection);

                langElement = commentsAndStuff.LastOrDefault() ?? langElement;

                //Debug.WriteLine(" > AddBackgroundAdornment ...");
                var adornment = new HorizontalLineDocAdornment(langElement.Range);
                args.AddBackgroundAdornment(adornment);
            }
        }

        IEnumerable<LanguageElement> previousSiblings(LanguageElement languageElement)
        {
            var sibling = languageElement.PreviousSibling;
            while (sibling != null)
            {
                yield return sibling;
                sibling = sibling.PreviousSibling;
            }
        }

    }
}
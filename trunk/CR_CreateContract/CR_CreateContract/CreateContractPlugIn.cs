//-----------------------------------------------------------------------
// <copyright file="CreateContractPlugin.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace CR_CreateContract
{
  using System.Linq;
  using DevExpress.CodeRush.Core;
  using DevExpress.CodeRush.PlugInCore;
  using DevExpress.CodeRush.StructuralParser;

  /// <summary>
  /// Generates a Code Contracts class for an interface and applies attributes
  /// to link the contract with the interface.
  /// </summary>
  public partial class CreateContractPlugIn : StandardPlugIn
  {
    /// <summary>
    /// string literal: ContractClass
    /// </summary>
    private const string ContractClassAttributeName = "ContractClass";

    /// <summary>
    /// An object which provides non-null verified access to CodeRush services.
    /// </summary>
    private CodeRushProxy codeRushProxy;

    /// <summary>
    /// The path to the file containing the interface.
    /// </summary>
    private string interfaceFilePath;

    /// <summary>
    /// Cached reference to the active interface.
    /// </summary>
    private Interface activeInterface;

    /// <summary>
    /// The name of the interface.
    /// </summary>
    private string interfaceName;

    /// <summary>
    /// The SourceFile language element containing the active interface.
    /// </summary>
    private SourceFile interfaceSourceFile;

    #region InitializePlugIn

    /// <summary>
    /// Initializes the plug in.
    /// </summary>
    public override void InitializePlugIn()
    {
      base.InitializePlugIn();

      // TODO: Add your initialization code here.
    }

    #endregion

    #region FinalizePlugIn

    /// <summary>
    /// Finalizes the plug in.
    /// </summary>
    public override void FinalizePlugIn()
    {
      // TODO: Add your own finalization code here.
      base.FinalizePlugIn();
    }

    #endregion

    /// <summary>
    /// Handles the Apply event of the cpCreateContract control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="ea">The <see cref="DevExpress.CodeRush.Core.ApplyContentEventArgs"/> instance containing the event data.</param>
    private void Apply(object sender, ApplyContentEventArgs ea)
    {
      // Must have cached instance of interface and interface declaration.
      if (this.activeInterface == null)
      {
        return;
      }

      if (ea.TextDocument == null)
      {
        return;
      }

      if (string.IsNullOrEmpty(this.interfaceFilePath))
      {
        return;
      }

      if (this.interfaceSourceFile == null)
      {
        return;
      }

      if (this.codeRushProxy == null)
      {
        return;
      }

      if (this.cpCreateContract == null)
      {
        return;
      }

      if (string.IsNullOrEmpty(this.interfaceName))
      {
        return;
      }

      var interfaceUpdater = new InterfaceUpdater(
        ea.TextDocument,
        this.activeInterface, 
        this.interfaceSourceFile,
        this.interfaceFilePath,
        this.interfaceName);
      var contractClassBuilder = new ContractClassBuilder(interfaceUpdater, this.codeRushProxy);
      using (this.codeRushProxy.TextBuffers.NewMultiFileCompoundAction(this.cpCreateContract.ActionHintText))
      {
        this.codeRushProxy.Markers.Drop(MarkerStyle.Standard);
        interfaceUpdater.UpdateInterface();
        contractClassBuilder.CreateContractClassFile();
      }
    }

    /// <summary>
    /// Handles the CheckAvailability event of the cpCreateContract control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="ea">The <see cref="DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs"/> instance containing the event data.</param>
    private void CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      // Must be in an interface.
      this.activeInterface = ea.ClassInterfaceOrStruct as Interface;
      if (this.activeInterface == null || ea.Caret.Line != this.activeInterface.Range.Start.Line)
      {
        return;
      }

      // This object will provide assertions that statically provided services are not null.  
      // If required services are null, an exception is thrown here by design.
      this.codeRushProxy = new CodeRushProxy();
      string filePath = this.codeRushProxy.Source.ActiveSourceFile.FilePath;
      if (string.IsNullOrEmpty(filePath))
      {
        return;
      }

      this.interfaceFilePath = filePath;

      SourceFile fileNode = this.activeInterface.FileNode;
      if (fileNode == null)
      {
        return;
      }

      this.interfaceSourceFile = fileNode;

      string interfaceName = this.activeInterface.Name;
      if (string.IsNullOrEmpty(interfaceName))
      {
        return;
      }
      
      this.interfaceName = interfaceName;

      // Must not already have the ContractClassAttribute
      ea.Available = this.activeInterface.AttributeSections == null ||
        !(from AttributeSection section in this.activeInterface.AttributeSections
          from DevExpress.CodeRush.StructuralParser.Attribute attribute in section.AttributeCollection
          where attribute.Name == ContractClassAttributeName
          select attribute).Any();
    }
  }
}
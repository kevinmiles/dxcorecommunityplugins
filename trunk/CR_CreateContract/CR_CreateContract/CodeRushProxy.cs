//-----------------------------------------------------------------------
// <copyright file="CodeRushProxy.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace CR_CreateContract
{
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;
  using DevExpress.CodeRush.Core;
  using DevExpress.DXCore.TextBuffers;

  /// <summary>
  /// Provides a wrapper around <see cref="DevExpress.CodeRush.Core.CodeRush"/> which ensures 
  /// certain properties and services are not null using checks and contracts.
  /// </summary>
  public class CodeRushProxy : ProxyBase
  {
    /// <summary>
    /// Document Services
    /// </summary>
    private readonly DocumentServices documents;

    /// <summary>
    /// File Services
    /// </summary>
    private readonly FileServices file;

    /// <summary>
    /// Language Services.
    /// </summary>
    private readonly LanguageServices language;

    /// <summary>
    /// Marker Services.
    /// </summary>
    private readonly MarkerServices markers;

    /// <summary>
    /// Solution Services
    /// </summary>
    private readonly SolutionServices solution;

    /// <summary>
    /// Source Model Services.
    /// </summary>
    private readonly SourceModelProxy source;

    /// <summary>
    /// Text buffer service 
    /// </summary>
    private readonly ITextBufferService textBuffers;

    /// <summary>
    /// The undo stack services.
    /// </summary>
    private readonly UndoServices undoStack;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeRushProxy"/> class.
    /// </summary>
    public CodeRushProxy()
    {
      this.solution = GetNonNull(CodeRush.Solution, "CodeRush.Solution.");
      this.documents = GetNonNull(CodeRush.Documents, "CodeRush.Documents");
      this.file = GetNonNull(CodeRush.File, "CodeRush.File");
      this.textBuffers = GetNonNull(CodeRush.TextBuffers, "CodeRush.TextBuffers");
      this.language = GetNonNull(CodeRush.Language, "CodeRush.Language");
      this.markers = GetNonNull(CodeRush.Markers, "CodeRush.Markers");
      var source = GetNonNull(CodeRush.Source, "CodeRush.Source");
      this.source = new SourceModelProxy(source);
      this.undoStack = GetNonNull(CodeRush.UndoStack, "CodeRush.UndoStack");
    }

    /// <summary>
    /// Gets the document services.
    /// </summary>
    public DocumentServices Documents
    {
      get
      {
        Contract.Ensures(Contract.Result<DocumentServices>() != null, "Result will not be null.");
        return this.documents;
      }
    }

    /// <summary>
    /// Gets the file services.
    /// </summary>
    public FileServices File
    {
      get
      {
        Contract.Ensures(Contract.Result<FileServices>() != null, "Result will not be null.");
        return this.file;
      }
    }

    /// <summary>
    /// Gets the language services.
    /// </summary>
    public LanguageServices Language
    {
      get
      {
        Contract.Ensures(Contract.Result<LanguageServices>() != null, "Result will not be null.");
        return this.language;
      }
    }

    /// <summary>
    /// Gets the marker services.
    /// </summary>
    public MarkerServices Markers
    {
      get
      {
        Contract.Ensures(Contract.Result<MarkerServices>() != null, "Result will not be null.");
        return this.markers;
      }
    }

    /// <summary>
    /// Gets the solution services.
    /// </summary>
    public SolutionServices Solution
    {
      get
      {
        Contract.Ensures(Contract.Result<SolutionServices>() != null, "Result will not be null.");
        return this.solution;
      }
    }

    /// <summary>
    /// Gets the source model services.
    /// </summary>
    public SourceModelProxy Source
    {
      get
      {
        Contract.Ensures(Contract.Result<SourceModelProxy>() != null, "Result will not be null.");
        return this.source;
      }
    }

    /// <summary>
    /// Gets the text buffer service.
    /// </summary>
    public ITextBufferService TextBuffers
    {
      get
      {
        Contract.Ensures(Contract.Result<ITextBufferService>() != null, "Result will not be null.");
        return this.textBuffers;
      }
    }

    /// <summary>
    /// Gets the undo stack services.
    /// </summary>
    public UndoServices UndoStack
    {
      get
      {
        Contract.Ensures(Contract.Result<UndoServices>() != null, "Result will not be null.");
        return this.undoStack;
      }
    }

    /// <summary>
    /// Container for invariant contracts.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by CodeContracts runtime.")]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "False positive.")]
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.undoStack != null, "this.undoStack must not be null.");
      Contract.Invariant(this.source != null, "this.source must not be null.");
      Contract.Invariant(this.markers != null, "this.markers must not be null.");
      Contract.Invariant(this.language != null, "this.language must not be null.");
      Contract.Invariant(this.textBuffers != null, "this.textBuffers must not be null.");
      Contract.Invariant(this.solution != null, "this.solution must not be null.");
      Contract.Invariant(this.documents != null, "this.documents must not be null.");
      Contract.Invariant(this.file != null, "this.file must not be null.");
    }
  }
}
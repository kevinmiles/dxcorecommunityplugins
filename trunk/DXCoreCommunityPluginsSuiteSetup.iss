; #################################################################################################
; Self-extracting InnoSetup script for DXCore Community Plugins Suite.
; Compile with InnoSetup: http://www.jrsoftware.org/isinfo.php
; Compilation can be automated with:
;    "%ProgramFiles%\Inno Setup 5\ISCC.exe" /Q "DXCoreCommunityPluginsSuiteSetup.iss"
; #################################################################################################


[Setup]
; NOTE: The value of AppId uniquely identifies this application so that an install of a later
; version doesn't create a new Uninstall entry in Add/Remove programs.
AppId=DXCoreCommunityPluginsSuite
AppName=DXCore Community Plugins Suite
; ### Update Version! ###
AppVerName=DXCore Community Plugins Suite 1.0.0.144
AppPublisherURL=http://code.google.com/p/dxcorecommunityplugins/
AppSupportURL=http://code.google.com/p/dxcorecommunityplugins/
AppUpdatesURL=http://code.google.com/p/dxcorecommunityplugins/
CreateAppDir=no
AllowNoIcons=yes
SourceDir=.\bin
OutputDir=..\
OutputBaseFilename=DXCoreCommunityPluginsSuiteSetup

[Types]
Name: none; Description: "(none)"
Name: custom; Description: "Custom installation"; Flags: iscustom
Name: full; Description: "Full installation"

; #################################################################################################
; This creates the tree on the "Select Components" page
; #################################################################################################
[Components]
Name: refactor; Description: "Refactorings"; Types: full
;Name: refactor\Resolve; Description: "Refactor: Resolve"; Types: full
Name: refactor\CreateStubForHandler; Description: "Refactor: Create Stub For Handler"; Types: full
Name: refactor\FormatAssignments; Description: "Refactor: Format Assignments"; Types: full
Name: refactor\Generalize; Description: "Refactor: Generalize"; Types: full

Name: paint; Description: "Code Painting"; Types: full
Name: paint\CR_BlockPainterPlus; Description: "Block Painter Plus"; Types: full
Name: paint\CR_DrawLinesBetweenMethods; Description: "Draw Lines Between Methods"; Types: full
Name: paint\HighlightCurrentLineInEditor; Description: "Highlight Current Line In Editor"; Types: full
Name: paint\HighlightNonDisposedLocals; Description: "Highlight Non Disposed Locals"; Types: full

Name: format; Description: "Code Formatting"; Types: full
Name: format\CR_Initials; Description: "Initials"; Types: full
Name: format\CR_JoinLines; Description: "Join Lines"; Types: full
Name: format\CR_Paste; Description: "Paste"; Types: full
Name: format\CR_SortLines; Description: "Sort Lines"; Types: full
;Name: format\CollapseXMLComment; Description: "Collapse XML Comment"; Types: full

; ##### TODO decide on categories for following? ####
Name: CR_EnhancedForEach; Description: "Enhanced For Each"; Types: full
Name: CR_EventHandlerCheckTC; Description: "Event Handler Check TC"; Types: full
Name: CR_ImplementBaseConstructors; Description: "Implement Base Constructors"; Types: full
Name: CR_JumpToImplementation; Description: "Jump To Implementation"; Types: full
Name: CR_ShowFile; Description: "Show File"; Types: full
Name: CR_mdMarkerExtensions; Description: "Marker Extensions"; Types: full
Name: CR_MsdnBclHelp; Description: "MSDN BCL Help"; Types: full
Name: DX_PickFromListStringProvider; Description: "Pick From List String Provider"; Types: full
Name: RedGreen; Description: "RedGreen"; Types: full

[Files]
;Source: "CollapseXMLComment.dll"; DestDir: "{code:DxPluginsDir}"; Components: format\CollapseXMLComment
Source: "CR_DrawLinesBetweenMethods.dll"; DestDir: "{code:DxPluginsDir}"; Components: paint\CR_DrawLinesBetweenMethods
Source: "CR_EnhancedForEach.dll"; DestDir: "{code:DxPluginsDir}"; Components: CR_EnhancedForEach
Source: "CR_EventHandlerCheckTC.dll"; DestDir: "{code:DxPluginsDir}"; Components: CR_EventHandlerCheckTC
Source: "CR_ImplementBaseConstructors.dll"; DestDir: "{code:DxPluginsDir}"; Components: CR_ImplementBaseConstructors
Source: "CR_Initials.dll"; DestDir: "{code:DxPluginsDir}"; Components: format\CR_Initials
Source: "CR_JoinLines.dll"; DestDir: "{code:DxPluginsDir}"; Components: format\CR_JoinLines
Source: "CR_JumpToImplementation.dll"; DestDir: "{code:DxPluginsDir}"; Components: CR_JumpToImplementation
Source: "CR_mdMarkerExtensions.dll"; DestDir: "{code:DxPluginsDir}"; Components: CR_mdMarkerExtensions
Source: "CR_MsdnBclHelp.dll"; DestDir: "{code:DxPluginsDir}"; Components: CR_MsdnBclHelp
Source: "CR_Paste.dll"; DestDir: "{code:DxPluginsDir}"; Components: format\CR_Paste
Source: "CR_ShowFile.dll"; DestDir: "{code:DxPluginsDir}"; Components: CR_ShowFile
Source: "CR_SortLines.dll"; DestDir: "{code:DxPluginsDir}"; Components: format\CR_SortLines
Source: "DX_PickFromListStringProvider.dll"; DestDir: "{code:DxPluginsDir}"; Components: DX_PickFromListStringProvider
Source: "HighlightCurrentLineInEditor.dll"; DestDir: "{code:DxPluginsDir}"; Components: paint\HighlightCurrentLineInEditor
Source: "HighlightNonDisposedLocals.dll"; DestDir: "{code:DxPluginsDir}"; Components: paint\HighlightNonDisposedLocals
;Source: "Refactor_Resolve.dll"; DestDir: "{code:DxPluginsDir}"; Components: refactor\Resolve
Source: "Refactor_CreateStubForHandler.dll"; DestDir: "{code:DxPluginsDir}"; Components: refactor\CreateStubForHandler
Source: "Refactor_FormatAssignments.dll"; DestDir: "{code:DxPluginsDir}"; Components: refactor\FormatAssignments
Source: "Refactor_Generalize.dll"; DestDir: "{code:DxPluginsDir}"; Components: refactor\Generalize
Source: "CR_BlockPainterPlus.dll"; DestDir: "{code:DxPluginsDir}"; Components: paint\CR_BlockPainterPlus
; RedGreen
Source: "RedGreen.dll"; DestDir: "{code:DxPluginsDir}"; Components: RedGreen
Source: "Gallio.dll"; DestDir: "{code:DxPluginsDir}"; Components: RedGreen
Source: "xunit.dll"; DestDir: "{code:DxPluginsDir}"; Components: RedGreen
Source: "xunit.runner.utility.dll"; DestDir: "{code:DxPluginsDir}"; Components: RedGreen

; ####  I guess this is actually a shared DLL for a couple of the above: update "Components" as appropriate ####
Source: "DX_ContextContrib.dll"; DestDir: "{code:DxPluginsDir}"; Components: paint\CR_BlockPainterPlus

[Code]
// This just saves a bit of copy & paste in the [Files] section
function DxPluginsDir(Param: String): String;
begin
  Result := ExpandConstant('{reg:HKLM\SOFTWARE\Developer Express\CodeRush for VS\3.0,PluginsFolder}');
end;


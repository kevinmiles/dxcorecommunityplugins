Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

Public Class PluginSettings
    Public Shared Function Load(ByVal Storage As DecoupledStorage) As XElement
        Dim Result = PluginSettings.GetDefaultSettings()
    End Function
    Public Shared Function GetDefaultSettings() As Object
        Return <Settings>
                   <Framework Name="NUnit" Folder="%Program Files%\NUnit\2.5.2\"/>
               </Settings>
    End Function
    Public Shared Function GetDefaultTemplates() As XElement
        Return <Templates>
                   <Framework Name="NUnit">
                       <Template Name="NUnitClassStub" DisplayName="Generate Test Class">
                           <Expansion>
                               <![CDATA[«AddAssemblyReference(NUnit.Framework.dll)»<TestFixture> _
Public Class «?Get(TestClassName)»
    «Target»
End Class
]]>
                           </Expansion>
                       </Template>
                       <Template Name="NUnitMethodStub" DisplayName="Generate Test Method">
                           <Expansion>
                               <![CDATA[«AddNamespace(NUnit.Framework)»«AddAssemblyReference(D:\Development\AuraStuff\3rd Party\NUnit\2.2.8.0\NUnit.Framework.dll)»<Test> _
Public Sub «?Get(TestMethodName)»()
    «Marker»
End Sub
]]>
                           </Expansion>
                       </Template>
                   </Framework>
               </Templates>

    End Function

End Class
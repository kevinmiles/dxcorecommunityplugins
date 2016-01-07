[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_GenerateTest/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Requires DXCore 10.2 since build 1576 #####
# Introduction #

Generates a new stub method, creating a class and project around it as needed.

# Details #

  * Generates a Test Project
    * References your **SourceProject**
    * References '**Nunit.Framework.dll**'
  * Generates a Test Class
    * Marks it with a **TestFixture** Attribute
  * Generates an empty Test Method
    * Marks it with a **Test** Attribute

So if you have


---

```
Public Class SomeClass
    Public Sub SomeMethod()
    End Sub 
End Class
```

---

... in a Project called "SomeProject"

Then activation will cause the generation of an additional project within the solution called "SomeProject\_Tests".

This project will contain a class...

---

```
Imports Nunit.Framework
<TestFixture> _
Public Class SomeClass_Tests
    <Test> _
    Public Sub Test_Test()
    End Sub 
End Class
```

---



### See also ###
[CR\_CreateTestMethod](CR_CreateTestMethod.md)
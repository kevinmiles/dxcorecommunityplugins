[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_CreateTestMethod/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Requires DXCore 10.2 since build 1533 #####
# Introduction #

There are a number of things introduced by this plugin which are intended to make Test Drivent Development easier.  These include:
  * Action (Create Test) which will convert the comment that the cursor is currnetly on to a test method
  * Smart Paste Provider which will do the same for multiple comments copied to the clipboard
  * Two Context Providers which allow you to create templates/shortcuts that apply specifically to tests
    * Inside Test Class
    * Inside Test Method
  * The "Move To Setup" Code Provider
  * Code Issue which indicates Test Methods which do not have Assertions

Please note that there is a very big possibility that some of these functions will only work with NUnit based tests.  MbUnit is similar, so that may work as well.  I would not count on MSTest or xUnit.Net working.

# Installation #

Installation instructions can be found on the InstallInstructions page.

# Setup #
Bind the "Create Test" command to a key. I chose to bind to 'CTRL-ALT-T'.  When creating the shortcut it is also advisable to use the context providers to specify that the shortcut is available "Inside Test Class" but not "Inside Test Method" (You can find both of these contexts under Editor/Code)

# Usage #

  * Create Test Method
    * Activate from within your test class with your cursor on a comment line
  * Smart Paste Provider
    * Copy multiple comment lines to the clipboard.  From within the class body, paste the clipboard contents.
So if you have


---

```
    // Can create a new class to do the thing with the stuff
```

---

... in your test

Then activation will cause the comment to be converted to the following test method stub

---

```
    [Test]
    public void Can_create_a_new_class_to_do_the_thing_with_the_stuff()
    {
        Assert.Fail("Not Implemented");
    }
```

---


  * Move to Setup
    * Currently this only works on cases where you are initializing or assigning values to class level variables, and a SetUp method already exists.
      * Invoke the Code menu, select "Move To Setup"
      * Your statement should be moved to the end of your setup method

So if you have

---

```
    [TestFixture]
    public class MyTestClass
    {
        private MyClass _testingClass;
        
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void TestSomething()
        {
            _testingClass = new MyClass();
        }
    }
```

---

...with the cursor on the first line in TestSomething
Invoking the "Move To Setup" provider produces the following

---

```
    [TestFixture]
    public class MyTestClass
    {
        private MyClass _testingClass;
        
        [SetUp]
        public void SetUp()
        {
            _testingClass = new MyClass();
        }

        [Test]
        public void TestSomething()
        {
        
        }
    }
```

---

  * Code Issue
    * This should detect Test Methods based on the presence of a "Test", or "TestMethod" attribute, and then check for either an "Assert" statement or an "ExpectedException" attribute.  If neither are found, a Code Smell issue is added.

# Why did I build this #

I do TDD daily, so this is the beginning of an effort to ease some of the pain points I encounter when working TDD.  As with most things, these are all things that I personally thought would be useful.  Feel free to suggest additional functionality if there is something you would like that is missing from this group of utilities.

# Credits #

Original Author: [Casey Kramer](CaseyKramer.md)
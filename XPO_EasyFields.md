[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/XPO_EasyFields/)           [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)

&lt;wiki:gadget url="http://www.alfware.com.au/facebook-like.xml" width="500" height="80" /&gt;



# Introduction #

XPO Simplified Criteria Syntax allows you to obtain your persistent objects properties via Object Orientated method. This is plugin is meant to replicate Oliver Sturms XPOFieldSync, and hopefully in time improve from the excellent idea of Olivers.

Instead of:

`Dim MyCustomersStartingWithB as XPCollection(Of Customer)(New BinaryOperator("CompanyTradingName", "B%", Like)) `

After running XPO_EasyFields you can now do:_

`Dim MyCustomersStartingWithB as XPCollection(Of Customer)(New BinaryOperator(Customer.Fields.CompanyTradingName, "B%", Like))`

This not only easier to identify fields and relations via Intellisense, but is Strongly Typed, meaning if you wanted to Refactor your object and change the CompanyTradingName property to say TradingName, you will end up with Compile Errors highlighting where you have used that field, unlike the string representation which is perfectly valid code until it executes at Runtime and collapes in a heap ;)

There are some differences between XPO\_EasyFields and XPOFieldSync

  * XPOFieldSync would create a region with a static name and replace this region everytime. This was an issue if you would “organise” your class to have your nested classes at different positions to your variable and property declarations, XPOFieldSync would see that there wasn’t a region create a new one and duplicate the class. XPO_EasyFields replaces the 3 sections (FieldsClass, Fields Property,_Fields Variable) all in place so it doesn’t matter if you have organised your code, it will update anyways ;)
  * XPOFieldSync offered an automatic way to update the class, basically each time you changed a property XPOFieldSync would update the definition, XPO_EasyFields doesn’t include this functionality as I found the automatic syncing slowed down the IDE too much for me with constant text being replaced.
  * XPOFieldSync still has an issue (as of March 2010) with XPCollections (that was introduced by a new version of DXCore in 2009) whereby it will skip them when generating the shadowed FieldsClass. XPO_EasyFields seems to capture all fields within your class and being in the Open Source community should mean any issues identified can be fixed by any number of people.

# Configuration #

![http://www.alfware.com.au/XPO_EasyFields_Options.png](http://www.alfware.com.au/XPO_EasyFields_Options.png)

There are currently 6 options (as per 1205)
## Update from Class Name Only ##
By default in the Refactor! menu that appears when you press your Refactor! key you can have the Command to perform the Update XPO FieldsClass anywhere within your persistent class, if you only want it available while the caret is within the Class Name you can tick this option.

## Perform Update on Save ##
This is pretty self-explanitory, when the document is saved, the Update XPO FieldsClass will be performed on any Persistent classes within the document

## Don't replace Property/Variable ##
By default XPO\_EasyFields will replace all code (ie. Fields property, _Fields variable and FieldsClass nested class) if you wish to make a slight change to your Fields property or_Fields variable (such as reordering the keywords of the property to comply with StyleCop) you can enable this option and if the Property or Variable are found it won't replace them.

## Include Non-Persistent Properties ##
You can specifically ask XPO\_EasyFields to include members that are marked Non-Persistent, this can be useful if you are using the Field references for other purposes such as databinding whereby you still need access to the field regardless if it is in the database or not.

## Include Inherited Members ##
By default everything remains the same, however this allows you to have EasyFields generate the FieldsClass using the PersistentBase.FieldsClass as it's ancestor instead of it's inherited class. This might be handy if you want to have complete seperation of your objects. (Such as if you have CreatedBy, CreatedOn in a BusinessBaseObject which MySaleDataObject inherits from, you can either have CreatedBy exposed in the Fields property through MySaleDataObject, or have to explicitly use the BusinessBaseObject to obtain that fields.

## Place a comment and custom format ##
This allow you to have your generated FieldsClass tagged when generated, allow you to easily identify when or possibly even who generated the class.

# Usage #

Place the caret on the Class name of your persistent object, bring up the Refactor menu and select Update XPO FieldsClass



# History #
  * Build 1905
    * (partially fixed) if property referenced external type (from referenced library), EasyFields wouldn't sync, EasyFields now won't crash however it also can't see if that referenced type derives from PersistentBase or is a Structure so therefore it will only ever generate a standard OperandProperty at the moment, going to hit the CR guys up with how to properly handle this scenario.
  * Build 1829
    * (fixed) incompatibility
  * Build 1439
    * (beta) Added support for Constant generation for use in Attributes
    * (fixed) hopefully fixed Structures not generating correctly
    * (added) can customise what the private variable name will be generated from the Options page, this helps meet any StyleCop/StyleNinja rules you may have.
  * Build 1205
    * New option to Sync Non-Persistent properties (default: False)
    * New option to include Inherited Members (default: True)
    * New option to customise/disable Comment (default: True)
    * Changed options screen to include new options
    * Fixed issue with "Shadowed" Fields property
    * Fixed issue with Structures not syncing
    * Started implementing CollectionFieldsClass (still in progress)
  * Build 1076
    * Removed the "Nothing to do" feedback, got annoying when using the Update on Save feature
  * Build 1053
    * Implemented feature request by Robert to Update on Document Save (look under DevExpress > Options > Editor > XPO > EasyFields)
    * Made replacing Fields Property and _Fields variable optional (to assist with StyleCop non-compliant ordering of Keywords on property, can change it once and XPO\_EasyFields won't replace it)
    * Fixed formatting issue in C#, now the code should format correctly
  * Build 1024
    * Fixed C# code having ReadOnly in front of properties
  * Build 1007
    * Ability to perform the update anywhere within the class
    * Shortcut included, you can now assign a shortcut to XPO Update FieldsClass
  * Build 997
    * Corrected some minor typos
  * Build 991
    * Initial Release_

# Future Plans #

  * Shortcut key to allow easy execution
  * Possibly make it you have a XPCollection to return a "custom" class that would give options of AggregateOperands such as Count, Sum, Max, Min etc.

# Credits #

Author: [Michael Proctor (AussieALF)](AussieALF.md) with help from mentor RoryBecker ;)
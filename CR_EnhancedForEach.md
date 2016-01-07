# Introduction #
This plugins finds all Enumerable variables within method or class scope, shows a list in a popup so you can pick one and create a for each loop for the selected variable.

Please also have a look at http://community.devexpress.com/forums/t/62991.aspx?PageIndex=1


# Installation #

Installation instructions can be found on the InstallInstructions page.

# Usage #

Create a new template 'efe' and use this text for template expansion:

For C#:

«?ForEachInit(method)»foreach(«Field("«?ForEachVarType»",type)» «Field("«?ForEachVarItemName»",var)» in «Caret»«Field("«?ForEachVarName»",collection)»)«BlockAnchor»
{
> «Marker»«Target»
}

For VB:

«?ForEachInit(method)»For Each  «Field("«?ForEachVarItemName»",var)» As «Field("«?ForEachVarType»",type)» In «Caret»«Field("«?ForEachVarName»",collection)»«BlockAnchor»
> «Marker»«Target»
Next

Create a second template 'efec' with the same text for template expansion but replace ForEachInit(method) with ForEachInit(class).

The difference is the second template finds all variables at Class level, the first at method level.

To test it, create a method with one or more Eumerable variables and type efe + space. A popup will show a list of all Enumerable variables. Pick one and a new for each loop will be generated.

PS In C# You can replace «Field("«?ForEachVarType»",type)» with var

# Options #

TODO

# Credits #

Author: Koen Hoefkens
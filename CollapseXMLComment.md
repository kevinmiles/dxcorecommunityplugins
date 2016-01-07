# Introduction #

This plug-in is implemented as a Refactoring - hit the Refactor button or click on the refactoring ellipse when inside of an XML comment and it will collapse it down to one line, ex.

```
<summary>
Sample
</summary>
```

Will become:

```
<summary>Sample</summary>
```

If the comment spans multiple lines it collapses it in two steps:

```
<summary>
This
Is
Three Lines.
</summary>
```

**First Collapse
```
<summary>This
Is
Three Lines</summary>
```**

**Second Collapse**

```
<summary>This Is Three Lines</summary>
```

You can view a more detailed explanation on my blog at:

http://www.rcs-solutions.com/blog/2008/08/08/CollapsingXMLCommentTags.aspx

# Installation #

Installation instructions can be found on the InstallInstructions page.

# Usage #

TODO

# Options #

TODO

# Credits #

Author: Paul Mrozowski
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_MarkerExtensions/bin/CR_MarkerExtensions.zip)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

#### Updated to work with DXCore/CodeRush 10.1+ (and hoping it still works with 9.3) ####
Caveat: The plug-in's Options page has the ability to test the animation of the marker's locator beacon. Unfortunately, the latest version of DXCore removed the API I used to show the animation. I have disabled the ability to test the animation for now.

## Introduction ##

**CR\_MarkerExtensions** is a CodeRush plug-in that supports an alternate set of navigation commands for CodeRush "Markers". The out-of-the-box Marker navigation scheme works like a popcorn-trail of lightweight bookmarks in your code. It is "stack based" so the last marker dropped will be the first marker collected (LIFO). The alternate set of navigation commands supported by this plug-in work in "document" order (from top to bottom). There are a couple of other commands in this plug-in that you may find useful so check it out.

The latest version of this plug-in was built with Visual Studio 2008 and CodeRush 9.3.4.

## Details ##

The following commands are available with this plug-in:

  * MarkerFirst - Navigate to the "first" (top-left) marker in the current document but do not collect it.
  * MarkerPrev, MarkerNext - Navigate to the "previous"/"next" marker in the current document but do not collect it.
  * MarkerLast - Navigate to the "last" (bottom-right) marker in the current document but do not collect it.
  * MarkerCollectFirst - Navigate to the "first" (top-left) marker in the current document and collect it.
  * MarkerCollectPrev, MarkerCollectNext - Navigate to the "previous"/"next" marker in the current document and collect it.
  * MarkerCollectLast - Navigate to the "last" (bottom-right) marker in the current document and collect it.
  * MarkerStackTop, MarkerStackBottom - Navigate to the marker on the top/bottom of the marker stack but do not collect it.
  * MarkerCollectAtCaret - Collects the marker under the caret. This is useful when you want to remove an arbitrary marker from the marker stack.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_MarkerExtensions/images/CR_MarkerExtensions.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_MarkerExtensions/images/CR_MarkerExtensions.png)

## Credits ##

Original Author: [Mark DuCharme](http://code.google.com/u/mark.ducharme)
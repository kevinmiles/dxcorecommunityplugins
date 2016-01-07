[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/Refactor_ConvertToXPOProperty/)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Requires DXCore 10.2 since build 1542 #####
# Introduction #

This plugin allow to convert auto property of any type into XPO property. It can be often used in scenario when classed which were previously serialized are now stored using XPO. It also supports delayed properties so you can easily convert from a auto property to delayed property and also from XPO property to an delayed property. Currently the plugin was designed and tested to work under C#.

## Sample ##

This plugins allows convert this:

```
public class Bar : XPObject
{
    public string Message { get; set; }
}
```

Into:

```
public class Bar : XPObject
{
    private string _Message;
    public string Message
    {
        get
        {
            return _Message;
        }
        set
        {
            SetPropertyValue("Message", ref _Message, value);
        }
    }
}
```

The updated version also supports delayed properties so you can convert this:

```
public class Bar : XPObject
{
    private string _Message;
    public string Message
    {
        get
        {
            return _Message;
        }
        set
        {
            SetPropertyValue("Message", ref _Message, value);
        }
    }
}
```

or this

```
public class Bar : XPObject
{
    public string Message { get; set; }
}
```

Into that:

```
public class Bar : XPObject
{
    [Delayed]
    public string Message
    {
        get
        {
            return GetDelayedPropertyValue("Message");
        }
        set
        {
            SetDelayedPropertyValue("Message", value);
        }
    }
}
```

# Usage #

![http://www.virgotech.pl/wp-content/uploads/2010/08/ConvertToXPoProp_01.png](http://www.virgotech.pl/wp-content/uploads/2010/08/ConvertToXPoProp_01.png)

![http://www.virgotech.pl/wp-content/uploads/2010/08/ConvertToXPoProp_02.png](http://www.virgotech.pl/wp-content/uploads/2010/08/ConvertToXPoProp_02.png)

![http://www.virgotech.pl/wp-content/uploads/2010/08/ConvertToXPoProp_03.png](http://www.virgotech.pl/wp-content/uploads/2010/08/ConvertToXPoProp_03.png)

# History #

  * 15.08.2010 - First version released to public.
  * 26.08.2010 - I've added support for delayed properties.

# Credits #

Krzysztof Blacha
You can contact me at krzysztof.blacha@gmail.com
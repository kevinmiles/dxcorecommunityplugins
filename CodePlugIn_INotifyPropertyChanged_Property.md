[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CodePlugIn_INotifyPropertyChanged_Property/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Build using DXCore 11.1 #####
### Introduction ###
This code expansion plugin allows you to expand or convert a property declaration so that it also fires the PropertyChanged event of the INotifyPropertyChanged interface.

  * It can convert auto-implemented or normal properties to include a call to fire the PropertyChanged event.
  * It will implement the INPC interface if it is not already implemented on the containing type.

This plugin contains two CodeProviders:

  * The first will simply fire the INPC.PropertyChanged event directly in the property set block after expansion.
  * The second assumes you have base class support for firing PropertyChanged through an OnPropertyChanged method that takes a lambda expression.

For the lambda expression base class support, you can use the Prism 4 NotificationObject base class: http://msdn.microsoft.com/en-us/library/microsoft.practices.prism.viewmodel.notificationobject(v=pandp.39).aspx.
### Usage ###
Place the cursor anywhere in the property name, invoke refactoring (CTRL-~ default). Select either "**Convert to INPC Property**" or "**Convert to INPC Property Base Class Call**" from the Code extension operations context menu.

### Details ###
For initial code like this:

```
public class Customer
{
    public int CustomerId { get; set; }
}
```

If you select "Convert to INPC Property", you will get:
```
public class Customer : INotifyPropertyChanged
{
    int _CustomerId;
    public int CustomerId
    {
        get { return _CustomerId; }
        set
        {
            if (value != _CustomerId)
            {
                _CustomerId = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CustomerId"));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged = delegate { };
}
```

Note the expansion of the property with a backing store, the call to PropertyChanged event from the set block, the addition of the INotifyPropertyChanged interface to the class, and the declaration of the PropertyChanged to satisfy the interface. The code uses an anonymous method to initialize the event so that there is always an empty subscriber in the list, removing the need for null checking before firing the event.

If instead you first add a base class that implements INPC and has an RaisePropertyChanged virtual method, such as the Prism NotificationObject class that supports a lambda expression pointer to the property:

```
public class Customer : NotificationObject
{
    public int CustomerId { get; set; }
}
```

Then you invoke refactoring on the property and select "Convert to INPC Property Base Class Call", the result will be:

```
public class Customer : NotificationObject
{
    int _CustomerId;
    public int CustomerId
    {
        get { return _CustomerId; }
        set
        {
            if (value != _CustomerId)
            {
                _CustomerId = value;
                RaisePropertyChanged(() => CustomerId);
            }
        }
    }
}
```

### Credits ###

Author: [Brian Noyes](http://briannoyes.net/)
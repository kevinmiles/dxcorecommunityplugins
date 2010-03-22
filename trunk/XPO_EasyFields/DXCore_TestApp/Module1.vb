Module Module1

    Sub Main()
        Console.WriteLine(PersistentObject1.Fields.PersistentReferenceProperty.PropertyName)
        Console.WriteLine(PersistentObject1.Fields.PersistentReferenceProperty.PersistentProperty.PropertyName)
        Console.WriteLine(PersistentObject1.Fields.PersistentReferenceProperty.PersistentReferenceProperty.PropertyName)
    End Sub

End Module

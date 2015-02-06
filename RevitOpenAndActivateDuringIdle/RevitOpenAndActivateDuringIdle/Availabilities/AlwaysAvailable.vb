Imports RUI = Autodesk.Revit.UI
Imports RDB = Autodesk.Revit.DB

Namespace Availabilities
    Public Class AlwaysAvailable
        Implements RUI.IExternalCommandAvailability

        Public Function IsCommandAvailable(applicationData As RUI.UIApplication, selectedCategories As RDB.CategorySet) As Boolean Implements RUI.IExternalCommandAvailability.IsCommandAvailable
            Return True
        End Function
    End Class
End Namespace


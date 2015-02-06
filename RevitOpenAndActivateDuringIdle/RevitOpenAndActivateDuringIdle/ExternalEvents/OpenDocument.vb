Imports RUI = Autodesk.Revit.UI

Namespace ExternalEvents
    Public Class OpenDocumentExternalEventHandler
        Implements RUI.IExternalEventHandler

        Public Sub Execute(app As RUI.UIApplication) Implements RUI.IExternalEventHandler.Execute
            Dim sf = New UserInterface.SelectFile()
            If Not sf.ShowDialog() Then Return
            Try
                app.OpenAndActivateDocument(sf.FilePath.Text)
            Catch ex As Exception
                System.Diagnostics.Debugger.Break()
            End Try
        End Sub

        Public Function GetName() As String Implements RUI.IExternalEventHandler.GetName
            ' not sure where this will appear or if need to be unique
            Return "External Evenet handler Name"
        End Function

    End Class
End Namespace

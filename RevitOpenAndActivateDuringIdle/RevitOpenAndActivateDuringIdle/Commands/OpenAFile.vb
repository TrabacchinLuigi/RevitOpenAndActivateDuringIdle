Imports RUI = Autodesk.Revit.UI
Imports RDB = Autodesk.Revit.DB

Namespace Commands

    <Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)>
    <Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)>
    <Autodesk.Revit.Attributes.Journaling(Autodesk.Revit.Attributes.JournalingMode.NoCommandData)>
    Public Class OpenAFile
        Implements Autodesk.Revit.UI.IExternalCommand

        Public Function Execute(commandData As RUI.ExternalCommandData, ByRef message As String, elements As RDB.ElementSet) As RUI.Result Implements RUI.IExternalCommand.Execute
            Dim task = New System.Threading.Tasks.Task(
                Sub()
                    Dim sf = New UserInterface.SelectFile()
                    If Not sf.ShowDialog() Then Return
                    Try
                        commandData.Application.OpenAndActivateDocument(sf.FilePath.Text)
                    Catch ex As Exception
                        System.Diagnostics.Debugger.Break()
                        Dim document As RDB.Document = Nothing
                        Dim DocumentOpenedHandler As New EventHandler(Of RDB.Events.DocumentOpenedEventArgs)(
                            Sub(sender As Autodesk.Revit.ApplicationServices.Application, e As RDB.Events.DocumentOpenedEventArgs)
                                If Not String.Equals(e.Document.PathName, sf.FilePath.Text, StringComparison.OrdinalIgnoreCase) Then Return
                                RemoveHandler sender.DocumentOpened, DocumentOpenedHandler
                                document = e.Document
                            End Sub
                        )
                        AddHandler ExternalApplication.Singleton.UIControlledApplication.ControlledApplication.DocumentOpened, DocumentOpenedHandler
                        Process.Start(sf.FilePath.Text)
                    End Try
                End Sub
            )
            ExternalApplication.Singleton.ThingsToDoOnIdling.Enqueue(task)
            Return Autodesk.Revit.UI.Result.Succeeded
        End Function

    End Class
End Namespace


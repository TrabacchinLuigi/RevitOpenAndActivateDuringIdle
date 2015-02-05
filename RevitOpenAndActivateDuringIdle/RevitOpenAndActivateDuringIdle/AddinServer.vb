Imports RUI = Autodesk.Revit.UI

Public Class AddinServer
    Implements RUI.IExternalApplication

    Private ThingsToDoOnIdlingField As New Collections.Concurrent.ConcurrentQueue(Of Threading.Tasks.Task)

    Public Function OnStartup(application As RUI.UIControlledApplication) As RUI.Result Implements RUI.IExternalApplication.OnStartup
        AddHandler application.Idling, AddressOf HandleIdling
        Return Autodesk.Revit.UI.Result.Succeeded
    End Function

    Public Function OnShutdown(application As RUI.UIControlledApplication) As RUI.Result Implements RUI.IExternalApplication.OnShutdown
        ' should wait for empting the queue ?
        ' should remove the handler who fill the queue ?
        ' should skip what is left in the queue ?
        ' ...
        Return Autodesk.Revit.UI.Result.Succeeded
    End Function

    Private Sub HandleIdling(sender As Object, e As RUI.Events.IdlingEventArgs)
        ' sender type ?
        Dim task As System.Threading.Tasks.Task = Nothing
        If ThingsToDoOnIdlingField.TryDequeue(task) Then task.RunSynchronously()
    End Sub

End Class

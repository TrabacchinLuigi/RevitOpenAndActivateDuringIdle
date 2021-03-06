﻿Imports RUI = Autodesk.Revit.UI

Public Class ExternalApplication
    Implements RUI.IExternalApplication

    Private Shared SingletonField As ExternalApplication
    Public Shared Property Singleton As ExternalApplication
        Get
            Return SingletonField
        End Get
        Private Set(value As ExternalApplication)
            SingletonField = value
        End Set
    End Property

    Private OpenDocumentExternalEventField As RUI.ExternalEvent
    Public ReadOnly Property RunTaskEvent As RUI.ExternalEvent
        Get
            Return OpenDocumentExternalEventField
        End Get
    End Property

    Public Sub New()
        Singleton = Me
    End Sub

    Public Function OnStartup(application As RUI.UIControlledApplication) As RUI.Result Implements RUI.IExternalApplication.OnStartup
        OpenDocumentExternalEventField = RUI.ExternalEvent.Create(New ExternalEvents.OpenDocumentExternalEventHandler())

        ' create some user interface
        application.CreateRibbonTab("Tests")
        Dim ribbonPanel As RUI.RibbonPanel = application.CreateRibbonPanel("Tests", "OpenAndActivate")

        Dim type = GetType(Commands.OpenAFile)

        Dim pushButtonDataTest As New RUI.PushButtonData(
            type.FullName,
            My.Resources.Open_a_file,
            type.Assembly.Location,
            type.FullName
        )

        Dim pushBtnApply As RUI.PushButton = ribbonPanel.AddItem(pushButtonDataTest)
        pushBtnApply.AvailabilityClassName = GetType(Availabilities.AlwaysAvailable).FullName

        pushBtnApply.ToolTip = "This will try to prompt for opening a file during idle"
        ' do i need those ?
        pushBtnApply.LargeImage = Nothing ' New BitmapImage(New Uri(My.Application.Info.DirectoryPath & "\whateverLarge.png", UriKind.Absolute))
        pushBtnApply.Image = Nothing ' New BitmapImage(New Uri(My.Application.Info.DirectoryPath & "\whaeverSmall.png", UriKind.Absolute))

        Return Autodesk.Revit.UI.Result.Succeeded
    End Function

    Public Function OnShutdown(application As RUI.UIControlledApplication) As RUI.Result Implements RUI.IExternalApplication.OnShutdown
        ' should wait for empting the queue ?
        ' should remove the handler who fill the queue ?
        ' should skip what is left in the queue ?
        ' ...
        SingletonField = Nothing
        Return Autodesk.Revit.UI.Result.Succeeded
    End Function

End Class

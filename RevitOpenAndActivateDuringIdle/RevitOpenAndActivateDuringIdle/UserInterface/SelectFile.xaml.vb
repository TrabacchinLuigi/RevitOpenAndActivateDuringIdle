Namespace UserInterface
    Public Class SelectFile

        Private Sub Button_Click(sender As Object, e As Windows.RoutedEventArgs)
            Dim a = New System.Windows.Forms.OpenFileDialog()
            a.ShowDialog()
            Me.FilePath.Text = a.FileName
        End Sub

        Private Sub OK_Button_Click(sender As Object, e As Windows.RoutedEventArgs)
            Me.DialogResult = True
        End Sub
    End Class
End Namespace


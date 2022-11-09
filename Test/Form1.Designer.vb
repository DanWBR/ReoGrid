<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ReoGridEditor1 = New unvell.ReoGrid.Editor.ReoGridEditor()
        Me.SuspendLayout()
        '
        'ReoGridEditor1
        '
        Me.ReoGridEditor1.CurrentFilePath = Nothing
        Me.ReoGridEditor1.CurrentSelectionRange = CType(resources.GetObject("ReoGridEditor1.CurrentSelectionRange"), unvell.ReoGrid.RangePosition)
        Me.ReoGridEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReoGridEditor1.Location = New System.Drawing.Point(0, 0)
        Me.ReoGridEditor1.Margin = New System.Windows.Forms.Padding(6, 6, 6, 6)
        Me.ReoGridEditor1.Name = "ReoGridEditor1"
        Me.ReoGridEditor1.NewDocumentOnLoad = True
        Me.ReoGridEditor1.Size = New System.Drawing.Size(1350, 782)
        Me.ReoGridEditor1.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1350, 782)
        Me.Controls.Add(Me.ReoGridEditor1)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ReoGridEditor1 As unvell.ReoGrid.Editor.ReoGridEditor
End Class

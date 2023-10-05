<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigLineIn
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConfigLineIn))
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ButtonOpen = New System.Windows.Forms.Button()
        Me.btSaveConfigLineIn = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbIDDevice = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(12, 173)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(255, 21)
        Me.ComboBox1.TabIndex = 0
        '
        'ButtonOpen
        '
        Me.ButtonOpen.Location = New System.Drawing.Point(6, 20)
        Me.ButtonOpen.Name = "ButtonOpen"
        Me.ButtonOpen.Size = New System.Drawing.Size(95, 23)
        Me.ButtonOpen.TabIndex = 1
        Me.ButtonOpen.Text = "Enable Line In"
        Me.ButtonOpen.UseVisualStyleBackColor = True
        '
        'btSaveConfigLineIn
        '
        Me.btSaveConfigLineIn.Location = New System.Drawing.Point(15, 337)
        Me.btSaveConfigLineIn.Name = "btSaveConfigLineIn"
        Me.btSaveConfigLineIn.Size = New System.Drawing.Size(252, 23)
        Me.btSaveConfigLineIn.TabIndex = 4
        Me.btSaveConfigLineIn.Text = "Save Config"
        Me.btSaveConfigLineIn.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button3)
        Me.GroupBox1.Controls.Add(Me.ButtonOpen)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 199)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(255, 54)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Test"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(154, 20)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(95, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Disable Line In"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 157)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(129, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Select Line In to Enable:"
        '
        'lbIDDevice
        '
        Me.lbIDDevice.AutoSize = True
        Me.lbIDDevice.Location = New System.Drawing.Point(12, 56)
        Me.lbIDDevice.Name = "lbIDDevice"
        Me.lbIDDevice.Size = New System.Drawing.Size(0, 13)
        Me.lbIDDevice.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(15, 261)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(252, 46)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Adittionally, Mute/Unmute program if it is active (for example, typing: 'salamand" &
    "ra' or 'radit')"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(15, 311)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(252, 22)
        Me.TextBox1.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(255, 126)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = resources.GetString("Label3.Text")
        '
        'ConfigLineIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(288, 383)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lbIDDevice)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btSaveConfigLineIn)
        Me.Controls.Add(Me.ComboBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ConfigLineIn"
        Me.ShowIcon = False
        Me.Text = "ConfigLineIn"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ButtonOpen As Button
    Friend WithEvents btSaveConfigLineIn As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lbIDDevice As Label
    Friend WithEvents Button3 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label3 As Label
End Class

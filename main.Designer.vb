<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.DataGridViewEvents = New System.Windows.Forms.DataGridView()
        Me.lbclock = New System.Windows.Forms.Label()
        Me.MainClock = New System.Windows.Forms.Timer(Me.components)
        Me.btSaveGuideline = New System.Windows.Forms.Button()
        Me.DataGridViewLog = New System.Windows.Forms.DataGridView()
        Me.WaitClock = New System.Windows.Forms.Timer(Me.components)
        Me.lbLog = New System.Windows.Forms.Label()
        Me.btAddConnection = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbHorarias = New System.Windows.Forms.Label()
        Me.lbdate = New System.Windows.Forms.Label()
        Me.lbTimeWorking = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.EntradaDeAudioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControlPanelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lbbgloading = New System.Windows.Forms.Label()
        Me.TimerConnection = New System.Windows.Forms.Timer(Me.components)
        Me.MainClockVisual = New System.Windows.Forms.Timer(Me.components)
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DataGridViewEvents, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridViewEvents
        '
        Me.DataGridViewEvents.AllowDrop = True
        Me.DataGridViewEvents.AllowUserToAddRows = False
        Me.DataGridViewEvents.AllowUserToDeleteRows = False
        Me.DataGridViewEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewEvents.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridViewEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewEvents.GridColor = System.Drawing.SystemColors.ControlLight
        Me.DataGridViewEvents.Location = New System.Drawing.Point(16, 208)
        Me.DataGridViewEvents.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DataGridViewEvents.Name = "DataGridViewEvents"
        Me.DataGridViewEvents.ReadOnly = True
        Me.DataGridViewEvents.RowHeadersVisible = False
        Me.DataGridViewEvents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewEvents.Size = New System.Drawing.Size(512, 212)
        Me.DataGridViewEvents.TabIndex = 4
        '
        'lbclock
        '
        Me.lbclock.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.lbclock.Font = New System.Drawing.Font("Segoe UI", 23.75!, System.Drawing.FontStyle.Bold)
        Me.lbclock.ForeColor = System.Drawing.Color.Black
        Me.lbclock.Location = New System.Drawing.Point(19, 40)
        Me.lbclock.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbclock.Name = "lbclock"
        Me.lbclock.Size = New System.Drawing.Size(194, 55)
        Me.lbclock.TabIndex = 5
        Me.lbclock.Text = "Hour"
        Me.lbclock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MainClock
        '
        Me.MainClock.Interval = 900
        '
        'btSaveGuideline
        '
        Me.btSaveGuideline.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btSaveGuideline.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btSaveGuideline.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btSaveGuideline.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btSaveGuideline.Location = New System.Drawing.Point(381, 170)
        Me.btSaveGuideline.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btSaveGuideline.Name = "btSaveGuideline"
        Me.btSaveGuideline.Size = New System.Drawing.Size(147, 29)
        Me.btSaveGuideline.TabIndex = 17
        Me.btSaveGuideline.Text = "Save Events"
        Me.btSaveGuideline.UseVisualStyleBackColor = False
        '
        'DataGridViewLog
        '
        Me.DataGridViewLog.AllowDrop = True
        Me.DataGridViewLog.AllowUserToAddRows = False
        Me.DataGridViewLog.AllowUserToDeleteRows = False
        Me.DataGridViewLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewLog.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridViewLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewLog.Location = New System.Drawing.Point(16, 449)
        Me.DataGridViewLog.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.DataGridViewLog.Name = "DataGridViewLog"
        Me.DataGridViewLog.ReadOnly = True
        Me.DataGridViewLog.RowHeadersVisible = False
        Me.DataGridViewLog.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewLog.Size = New System.Drawing.Size(512, 121)
        Me.DataGridViewLog.TabIndex = 28
        '
        'lbLog
        '
        Me.lbLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLog.Location = New System.Drawing.Point(13, 156)
        Me.lbLog.Name = "lbLog"
        Me.lbLog.Size = New System.Drawing.Size(361, 26)
        Me.lbLog.TabIndex = 32
        Me.lbLog.Text = "LineInScheduler"
        '
        'btAddConnection
        '
        Me.btAddConnection.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btAddConnection.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.btAddConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btAddConnection.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btAddConnection.Location = New System.Drawing.Point(16, 106)
        Me.btAddConnection.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.btAddConnection.Name = "btAddConnection"
        Me.btAddConnection.Size = New System.Drawing.Size(198, 29)
        Me.btAddConnection.TabIndex = 45
        Me.btAddConnection.Text = "New scheduled opening"
        Me.btAddConnection.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Button6.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button6.Location = New System.Drawing.Point(223, 106)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(149, 29)
        Me.Button6.TabIndex = 46
        Me.Button6.Text = "Enable Now"
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Button7.FlatAppearance.BorderColor = System.Drawing.Color.LightGray
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Button7.Location = New System.Drawing.Point(380, 107)
        Me.Button7.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(148, 29)
        Me.Button7.TabIndex = 47
        Me.Button7.Text = "Disable Now"
        Me.Button7.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 429)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Log"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 188)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Next Events"
        '
        'lbHorarias
        '
        Me.lbHorarias.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbHorarias.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.lbHorarias.Font = New System.Drawing.Font("Segoe UI", 21.75!, System.Drawing.FontStyle.Bold)
        Me.lbHorarias.ForeColor = System.Drawing.Color.Navy
        Me.lbHorarias.Location = New System.Drawing.Point(16, -62)
        Me.lbHorarias.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbHorarias.Name = "lbHorarias"
        Me.lbHorarias.Size = New System.Drawing.Size(194, 55)
        Me.lbHorarias.TabIndex = 6
        Me.lbHorarias.Text = "HORARIAS"
        Me.lbHorarias.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbHorarias.Visible = False
        '
        'lbdate
        '
        Me.lbdate.BackColor = System.Drawing.Color.FromArgb(CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.lbdate.Font = New System.Drawing.Font("Segoe UI", 10.75!, System.Drawing.FontStyle.Bold)
        Me.lbdate.ForeColor = System.Drawing.Color.Black
        Me.lbdate.Location = New System.Drawing.Point(223, 40)
        Me.lbdate.Margin = New System.Windows.Forms.Padding(0)
        Me.lbdate.Name = "lbdate"
        Me.lbdate.Size = New System.Drawing.Size(307, 55)
        Me.lbdate.TabIndex = 6
        Me.lbdate.Text = "lbdate"
        Me.lbdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbTimeWorking
        '
        Me.lbTimeWorking.Location = New System.Drawing.Point(235, 744)
        Me.lbTimeWorking.Name = "lbTimeWorking"
        Me.lbTimeWorking.Size = New System.Drawing.Size(294, 22)
        Me.lbTimeWorking.TabIndex = 40
        Me.lbTimeWorking.Text = "Tiempo Funcionando"
        Me.lbTimeWorking.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EntradaDeAudioToolStripMenuItem, Me.ControlPanelToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(539, 24)
        Me.MenuStrip1.TabIndex = 24
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'EntradaDeAudioToolStripMenuItem
        '
        Me.EntradaDeAudioToolStripMenuItem.Name = "EntradaDeAudioToolStripMenuItem"
        Me.EntradaDeAudioToolStripMenuItem.Size = New System.Drawing.Size(131, 20)
        Me.EntradaDeAudioToolStripMenuItem.Text = "Line In Configuration"
        '
        'ControlPanelToolStripMenuItem
        '
        Me.ControlPanelToolStripMenuItem.Name = "ControlPanelToolStripMenuItem"
        Me.ControlPanelToolStripMenuItem.Size = New System.Drawing.Size(91, 20)
        Me.ControlPanelToolStripMenuItem.Text = "Control Panel"
        '
        'lbbgloading
        '
        Me.lbbgloading.AutoSize = True
        Me.lbbgloading.Location = New System.Drawing.Point(17, 749)
        Me.lbbgloading.Name = "lbbgloading"
        Me.lbbgloading.Size = New System.Drawing.Size(89, 13)
        Me.lbbgloading.TabIndex = 43
        Me.lbbgloading.Text = "LineInScheduler"
        '
        'TimerConnection
        '
        '
        'MainClockVisual
        '
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(112, 20)
        Me.AboutToolStripMenuItem.Text = "About the project"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(539, 585)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.lbHorarias)
        Me.Controls.Add(Me.btAddConnection)
        Me.Controls.Add(Me.lbbgloading)
        Me.Controls.Add(Me.lbTimeWorking)
        Me.Controls.Add(Me.lbdate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridViewLog)
        Me.Controls.Add(Me.lbLog)
        Me.Controls.Add(Me.btSaveGuideline)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.DataGridViewEvents)
        Me.Controls.Add(Me.lbclock)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.Black
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LineInScheduler"
        CType(Me.DataGridViewEvents, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridViewEvents As DataGridView
    Friend WithEvents lbclock As Label
    Friend WithEvents MainClock As Timer
    Friend WithEvents btSaveGuideline As Button
    Friend WithEvents DataGridViewLog As DataGridView
    Friend WithEvents WaitClock As Timer
    Friend WithEvents lbLog As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lbdate As Label
    Friend WithEvents lbTimeWorking As Label
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents lbHorarias As Label
    Friend WithEvents lbbgloading As Label
    Friend WithEvents btAddConnection As Button
    Friend WithEvents TimerConnection As Timer
    Friend WithEvents EntradaDeAudioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MainClockVisual As Timer
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents ControlPanelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
End Class

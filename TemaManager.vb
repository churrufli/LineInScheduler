Imports System.Drawing

Public Class TemaManager
    Public Shared Sub AplicarTemaClaro(formulario As Form)
        ' Configurar colores para el tema claro
        Dim colorFondoFormulario As Color = Color.White
        Dim colorFuenteFormulario As Color = Color.Black
        Dim colorFondoControles As Color = Color.LightGray
        Dim colorFondoBotones As Color = Color.White
        Dim colorFondoDataGridView As Color = Color.White
        Dim colorFuenteDataGridView As Color = Color.Black
        Dim colorFondoLinea1 As Color = Color.FromArgb(240, 240, 240) ' Blanco pálido
        Dim colorFondoLinea2 As Color = Color.White

        ' Aplicar colores al formulario y sus controles
        formulario.BackColor = colorFondoFormulario
        formulario.ForeColor = colorFuenteFormulario
        AplicarColoresControles(formulario.Controls, colorFondoControles)
        AplicarColoresBotones(formulario.Controls, colorFondoBotones, True)
        AplicarColoresDataGridViews(formulario.Controls, colorFondoDataGridView, colorFuenteDataGridView, colorFondoLinea1, colorFondoLinea2)
    End Sub

    Public Shared Sub AplicarTemaOscuro(formulario As Form)
        ' Configurar colores para el tema oscuro
        Dim colorFondoFormulario As Color = Color.FromArgb(18, 18, 18)
        Dim colorFuenteFormulario As Color = Color.White
        Dim colorFondoControles As Color = Color.FromArgb(60, 60, 60)
        Dim colorFondoBotones As Color = Color.Gray
        Dim colorFondoDataGridView As Color = Color.FromArgb(45, 45, 45)
        Dim colorFuenteDataGridView As Color = Color.White
        Dim colorFondoLinea1 As Color = Color.FromArgb(50, 50, 50) ' Negro pálido
        Dim colorFondoLinea2 As Color = Color.Black

        ' Aplicar colores al formulario y sus controles
        formulario.BackColor = colorFondoFormulario
        formulario.ForeColor = colorFuenteFormulario
        AplicarColoresControles(formulario.Controls, colorFondoControles)
        AplicarColoresBotones(formulario.Controls, colorFondoBotones, False)
        AplicarColoresDataGridViews(formulario.Controls, colorFondoDataGridView, colorFuenteDataGridView, colorFondoLinea1, colorFondoLinea2)
    End Sub

    Private Shared Sub AplicarColoresControles(controles As Control.ControlCollection, colorFondo As Color)
        For Each control As Control In controles
            control.BackColor = colorFondo
            AplicarColoresControles(control.Controls, colorFondo)
        Next
    End Sub

    Private Shared Sub AplicarColoresBotones(controles As Control.ControlCollection, colorFondo As Color, temaClaro As Boolean)
        For Each control As Control In controles
            If TypeOf control Is Button Then
                Dim boton As Button = DirectCast(control, Button)
                boton.BackColor = colorFondo

                If temaClaro Then
                    boton.FlatAppearance.BorderColor = Color.Green ' Borde verde en tema claro
                Else
                    boton.FlatAppearance.BorderColor = Color.Gray ' Borde gris en tema oscuro
                End If

                boton.FlatAppearance.BorderSize = 1
                boton.FlatAppearance.MouseDownBackColor = boton.BackColor
                boton.FlatAppearance.MouseOverBackColor = boton.BackColor
            End If
            AplicarColoresBotones(control.Controls, colorFondo, temaClaro)
        Next
    End Sub

    Private Shared Sub AplicarColoresDataGridViews(controles As Control.ControlCollection, colorFondo As Color, colorFuente As Color, colorFondoLinea1 As Color, colorFondoLinea2 As Color)
        For Each control As Control In controles
            If TypeOf control Is DataGridView Then
                Dim dataGridView As DataGridView = DirectCast(control, DataGridView)
                dataGridView.BackgroundColor = colorFondo
                dataGridView.ForeColor = colorFuente
                dataGridView.DefaultCellStyle.BackColor = colorFondo
                dataGridView.DefaultCellStyle.ForeColor = colorFuente
                AddHandler dataGridView.RowPrePaint, Sub(sender As Object, e As DataGridViewRowPrePaintEventArgs)
                                                         If e.RowIndex Mod 2 = 0 Then
                                                             dataGridView.Rows(e.RowIndex).DefaultCellStyle.BackColor = colorFondoLinea1
                                                         Else
                                                             dataGridView.Rows(e.RowIndex).DefaultCellStyle.BackColor = colorFondoLinea2
                                                         End If
                                                     End Sub
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = colorFondoLinea1
                dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = colorFuente
            End If
            AplicarColoresDataGridViews(control.Controls, colorFondo, colorFuente, colorFondoLinea1, colorFondoLinea2)
        Next
    End Sub
End Class

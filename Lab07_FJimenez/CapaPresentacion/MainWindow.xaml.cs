using System;
using System.Windows;
using System.Windows.Controls;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class MainWindow : Window
    {
        private NPersona negocio = new NPersona();

        public MainWindow()
        {
            InitializeComponent();
            Listar();
        }

        private void Listar()
        {
            try
            {
                dgPersonas.ItemsSource = negocio.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al listar: " + ex.Message);
            }
        }

        private Persona ObtenerPersonaDelFormulario()
        {
            Persona persona = new Persona();
            persona.IdPersona = txtId.Text == "" ? 0 : Convert.ToInt32(txtId.Text);
            persona.Nombres = txtNombres.Text.Trim();
            persona.Apellidos = txtApellidos.Text.Trim();
            persona.Dni = txtDni.Text.Trim();
            return persona;
        }

        private void BtnInsertar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                negocio.Insertar(ObtenerPersonaDelFormulario());
                MessageBox.Show("Persona insertada correctamente.");
                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                negocio.Actualizar(ObtenerPersonaDelFormulario());
                MessageBox.Show("Persona actualizada correctamente.");
                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Selecciona una persona de la lista para eliminar.");
                return;
            }

            MessageBoxResult respuesta = MessageBox.Show(
                "¿Seguro que deseas eliminar a " + txtNombres.Text + " " + txtApellidos.Text + "?",
                "Confirmar", MessageBoxButton.YesNo);

            if (respuesta != MessageBoxResult.Yes)
            {
                return;
            }

            try
            {
                negocio.Eliminar(Convert.ToInt32(txtId.Text));
                MessageBox.Show("Persona eliminada correctamente.");
                Limpiar();
                Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgPersonas.ItemsSource = negocio.Buscar(txtBuscar.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }

        private void BtnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            txtBuscar.Text = "";
            Listar();
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            txtId.Text = "";
            txtNombres.Text = "";
            txtApellidos.Text = "";
            txtDni.Text = "";
            dgPersonas.SelectedItem = null;
        }

        private void DgPersonas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Persona persona = dgPersonas.SelectedItem as Persona;
            if (persona == null)
            {
                return;
            }

            txtId.Text = persona.IdPersona.ToString();
            txtNombres.Text = persona.Nombres;
            txtApellidos.Text = persona.Apellidos;
            txtDni.Text = persona.Dni;
        }
    }
}

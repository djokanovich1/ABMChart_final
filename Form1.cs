using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BE;

namespace ParcialCardacci
{
    public partial class Form1 : Form
    {
        private BLL.Persona BLLpersona = new BLL.Persona();
        private BE.Persona PERSONA_SELECCIONADA;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Enlazar();
        }

        void Enlazar()
        {
            var personas = BLLpersona.ListarTodos();
            dataGridView1.DataSource = personas;
            ActualizarHistograma(personas);
        }

        private void ActualizarHistograma(List<Persona> personas)
        {
            // Labels del eje x
            var x = new[] { "1-10", "11-20", "21-30", "31-40", "41-50", "51-60", "61-70", "71-80", "81-90", "91-100" };
            
            // Edades de todas las personas
            var edades = personas.Select(p => (DateTime.Now - p.FechaNac).Days / 365);

            // Valores en el eje y: calculo para cada rango de edades, cuántas personas hay en el rango correspondiente.
            var y = new List<int>();

            for (int offset = 0; offset < 10; offset++)
            {
                var cant = edades.Where(e =>
                    e > 10 * offset &&
                    e <= 10 * (offset + 1))
                        .Count();

                y.Add(cant);
            }

            // Grafico en el control Chart.
            chart1.Series.Clear();
            var serie = chart1.Series.Add(@"Edades");

            for (int i = 0; i < x.Length; i++)
            {
                serie.Points.AddXY(x[i], y[i]);
            }
        }

        // Alta
        private void Button1_Click(object sender, EventArgs e)
        {
            // formato DD/MM/AAAA
            var regex = new Regex(@"\d{1,2}/\d{1,2}/\d{4}");
            
            if(!string.IsNullOrWhiteSpace(textBox1.Text) &&
               !string.IsNullOrWhiteSpace(textBox2.Text) &&
               regex.IsMatch(txtFechaNac.Text)
               )
            {
                BE.Persona persona = new BE.Persona
                {
                    Nombre = textBox1.Text,
                    Apellido = textBox2.Text,
                    FechaNac = DateTime.Parse(txtFechaNac.Text)
                };

                if(BLLpersona.Alta(persona))
                {
                    MessageBox.Show("Todo ok");
                    Enlazar();
                }
            }
            else
            {
                MessageBox.Show("Verifique los datos ingresados.");
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.Rows.Count > 0)
            {
                PERSONA_SELECCIONADA = (BE.Persona)dataGridView1.Rows[e.RowIndex].DataBoundItem;
            }
        }

        // Baja
        private void Button2_Click(object sender, EventArgs e)
        {
            if(PERSONA_SELECCIONADA != null)
            {
                if(BLLpersona.Baja(PERSONA_SELECCIONADA) == true)
                {
                    MessageBox.Show("Baja ok");
                    Enlazar();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar a una persona.");
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Seleccione solo una persona.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                var cells = dataGridView1.SelectedRows[0].Cells;
                BE.Persona persona = new BE.Persona
                {
                    ID = (int)cells[0].Value,
                    Nombre = textBox1.Text,
                    Apellido = textBox2.Text,
                    FechaNac = dateTimePicker1.Value,
                };

                if (BLLpersona.Modificar(persona))
                {
                    MessageBox.Show("Todo ok");
                    Enlazar();
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar datos a modificar.");
            }
        }

        private void btnSortAsc_Click(object sender, EventArgs e)
        {
            var personas = dataGridView1.Rows
                                .Cast<DataGridViewRow>()
                                .Select(r => new Persona
                                    {
                                        ID = (int)r.Cells[0].Value,
                                        Nombre = (string)r.Cells[1].Value,
                                        Apellido = (string)r.Cells[2].Value,
                                        FechaNac = (DateTime)r.Cells[3].Value,
                                })
                                .OrderBy(p => p.FechaNac)
                                .ToList();
                
            dataGridView1.DataSource = personas;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var personas = BLLpersona.ListarTodos();
            dataGridView1.DataSource = personas;
            ActualizarHistograma(personas);
            MessageBox.Show("Grafico Actualizado");

        }

        private void btnSortDesc_Click(object sender, EventArgs e)
        {
            var personas = dataGridView1.Rows
                               .Cast<DataGridViewRow>()
                               .Select(r => new Persona
                               {
                                   ID = (int)r.Cells[0].Value,
                                   Nombre = (string)r.Cells[1].Value,
                                   Apellido = (string)r.Cells[2].Value,
                                   FechaNac = (DateTime)r.Cells[3].Value,
                               })
                               .OrderByDescending(p => p.FechaNac)
                               .ToList();

            dataGridView1.DataSource = personas;
        }
    }
}

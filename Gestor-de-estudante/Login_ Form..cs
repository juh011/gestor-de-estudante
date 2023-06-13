using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_de_estudante
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Coloca a imagem user.png na picture box da janela
            pictureBox1.Image = Image.FromFile("../../imagem/user.png");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MEU_BD bancoDeDados = new MEU_BD();

            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            DataTable tabela = new DataTable();
            MySqlCommand comando = new MySqlCommand("SELECT * FROM `usuarios` WHERE 1`username` @usn AND `senha` = @psw", bancoDeDados.getConexao);

            comando.Parameters.Add("@usn", MySqlDbType.VarChar).Value = txtUsuario.Text;
            comando.Parameters.Add("@psw", MySqlDbType.VarChar).Value = txtSenha.Text;

            adaptador.SelectCommand = comando;

            adaptador.Fill(tabela);

            if (tabela.Rows.Count > 0)
            {
                MessageBox.Show("YES");

            }
            else
            {
                MessageBox.Show("NO");
            
            }



        }
    }
}

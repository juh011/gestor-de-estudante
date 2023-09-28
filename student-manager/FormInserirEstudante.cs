using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace student_manager
{
    public partial class FormInserirEstudante : Form
    {
        public FormInserirEstudante()
        {
            InitializeComponent();
        }

        private void buttonEnviarFoto_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog inserirFoto = new OpenFileDialog();
            
            inserirFoto.Filter = "Selecionar Imagem(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif;";
            if (inserirFoto.ShowDialog() == DialogResult.OK)
            {
                pictureBoxFoto.Image = Image.FromFile(inserirFoto.FileName);
            }
        }

        private void buttonCadastrar_Click(object sender, EventArgs e)
        {
           
            Estudante novoEstudante = new Estudante();
            string nomeDoEstudante = textBoxNome.Text;
            string sobrenomeDoEstudante = textBoxSobrenome.Text;
            DateTime dataDeNascimento = dateTimePickerNascimento.Value;
            string telefoneDoEstudante = textBoxTelefone.Text;
            string enderecoDoEstudante = textBoxEndereco.Text;
            string generoDoEstudante = "Feminino";

            if (radioButtonMasculino.Checked)
            {
                generoDoEstudante = "Masculino";
            }

            MemoryStream fotoDoEstudante = new MemoryStream();

            int anoDeNascimento = dateTimePickerNascimento.Value.Year;
            int anoAtual = DateTime.Now.Year;
            int idadeAtual = anoAtual - anoDeNascimento;

            if (idadeAtual < 10 || idadeAtual > 100)
            {
                MessageBox.Show("O estudante precisa ter entre 10 e 100 anos",
                    "Data de Nascimento Inválida", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else if (verificarDados())
            {
                pictureBoxFoto.Image.Save(fotoDoEstudante, pictureBoxFoto.Image.RawFormat);
                if (novoEstudante.inserirEstudante(nomeDoEstudante,
                    sobrenomeDoEstudante,
                    dataDeNascimento,
                    telefoneDoEstudante,
                    generoDoEstudante,
                    enderecoDoEstudante,
                    fotoDoEstudante
                    ))
                {
                    MessageBox.Show("Estudante cadastrado!", 
                        "Cadastrar Estudante",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Estudante NÃO CADASTRADO!",
                        "Cadastrar Estudante",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Campos em branco!",
                        "Cadastrar Estudante",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private bool verificarDados()
        {
            if ((textBoxNome.Text.Trim() == "") ||
                (textBoxSobrenome.Text.Trim() == "") ||
                (textBoxTelefone.Text.Trim() == "") ||
                (textBoxEndereco.Text.Trim() == "") ||
                (pictureBoxFoto.Image == null)
                )
            { 
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormInserirEstudante_Load(object sender, EventArgs e)
        {

        }
    }
}

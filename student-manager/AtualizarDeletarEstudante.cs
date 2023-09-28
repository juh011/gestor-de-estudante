using MySql.Data.MySqlClient;
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
    public partial class AtualizarDeletarEstudante : Form
    {
        public AtualizarDeletarEstudante()
        {
            InitializeComponent();
        }

        
        Estudante estudante = new Estudante();

        private void buttonEnviarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog inserirFoto = new OpenFileDialog();
           
            inserirFoto.Filter = "Selecionar Imagem(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif;";
            if (inserirFoto.ShowDialog() == DialogResult.OK)
            {
                pictureBoxFoto.Image = Image.FromFile(inserirFoto.FileName);
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

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            
            Estudante novoEstudante = new Estudante();
            int id = Convert.ToInt32(textBoxID.Text);
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
                if (novoEstudante.atualizarEstudante(id, nomeDoEstudante,
                    sobrenomeDoEstudante,
                    dataDeNascimento,
                    telefoneDoEstudante,
                    generoDoEstudante,
                    enderecoDoEstudante,
                    fotoDoEstudante
                    ))
                {
                    MessageBox.Show("Informações Atualizadas!",
                        "Atualizar Estudante",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Informações não atualizadas!",
                        "Atualizar Estudante",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Campos em branco!",
                        "Atualizar Estudante",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }

        private void buttonDeletar_Click(object sender, EventArgs e)
        {
            
            int id = Convert.ToInt32(textBoxID.Text);

            if (MessageBox.Show("Tem certeza que quer remover esse aluno?",
                "Remover Estudante", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            { 
                if(estudante.deletarEstudante(id))
                {
                    MessageBox.Show("Estudante Removido", "Remover Estudante",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    textBoxNome.Text = "";
                    textBoxSobrenome.Text = "";
                    textBoxTelefone.Text = "";
                    textBoxEndereco.Text = "";
                    dateTimePickerNascimento.Value = DateTime.Now;
                    pictureBoxFoto.Image = null;
                }
                else
                {
                    MessageBox.Show("Estudante Não Removido", "Remover Estudante",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void buttonProcurar_Click(object sender, EventArgs e)
        {
            try
            {
               
                int id = Convert.ToInt32(textBoxID.Text);
                MySqlCommand comando = new MySqlCommand("SELECT `id`, `nome`, `sobrenome`, `nascimento`, `genero`, `telefone`, `endereco`, `foto` FROM `estudantes` WHERE `id`=" + id);

               
                DataTable tabela = estudante.pegarEstudantes(comando);

                if (tabela.Rows.Count > 0)
                {
                    textBoxNome.Text = tabela.Rows[0]["nome"].ToString();
                    textBoxSobrenome.Text = tabela.Rows[0]["sobrenome"].ToString();
                    textBoxTelefone.Text = tabela.Rows[0]["telefone"].ToString();
                    textBoxEndereco.Text = tabela.Rows[0]["endereco"].ToString();

                    dateTimePickerNascimento.Value = (DateTime)tabela.Rows[0]["nascimento"];

                    if (tabela.Rows[0]["genero"].ToString() == "Feminino")
                    {
                        radioButtonFeminino.Checked = true;
                    }
                    else
                    {
                        radioButtonMasculino.Checked = true;
                    }

                    
                    byte[] fotoDoBancoDeDados = (byte[])tabela.Rows[0]["foto"];
                    MemoryStream fotoDaInterface = new MemoryStream(fotoDoBancoDeDados);
                    pictureBoxFoto.Image = Image.FromStream(fotoDaInterface);
                }
            } 
            catch (Exception ) 
            {
                MessageBox.Show("Insira uma ID Válida", "ID Inválida", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBoxID_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBoxID_KeyPress(object sender, KeyPressEventArgs teclado)
        {
            if (!char.IsControl(teclado.KeyChar) &&
              !char.IsDigit(teclado.KeyChar))
            {
                teclado.Handled = true;
            }
        }

        private void AtualizarDeletarEstudante_Load(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxFoto_Click(object sender, EventArgs e)
        {

        }

        private void textBoxTelefone_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxEndereco_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaContatos
{
    public partial class frmAgendaContatos : Form
    {
        private OperacaoEnum acao;
        public frmAgendaContatos()
        {
            InitializeComponent();
        }

        private void frmAgendaContatos_Shown(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesInclueAlterarEExcluir(true);
            AlterarEstadoDosCampos(false);
            CarregarListaContatos();
        }

        private void AlterarBotoesSalvarECancelar(bool estado)
        {
            btnSalvar.Enabled   = estado;
            btnCancelar.Enabled = estado;
        }

        private void AlterarBotoesInclueAlterarEExcluir(bool estado)
        {
            btnIncluir.Enabled = estado;
            btnAlterar.Enabled = estado;
            btnExcluir.Enabled = estado;
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesInclueAlterarEExcluir(false);
            AlterarEstadoDosCampos(true);
            acao = OperacaoEnum.INCLUIR;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(true);
            AlterarBotoesInclueAlterarEExcluir(false);
            AlterarEstadoDosCampos(true);
            acao = OperacaoEnum.ALTERAR;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja excluir?", "Confirmação", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int indiceExcluido = lbxContatos.SelectedIndex;
                lbxContatos.SelectedIndex = 0;
                lbxContatos.Items.RemoveAt(indiceExcluido);
                List<Contato> contatoList = new List<Contato>();

                foreach(Contato contato in lbxContatos.Items)
                {
                    contatoList.Add(contato);
                }

                ManipuladorDeArquivos.EscreverArquivo(contatoList);
                CarregarListaContatos();
                LimparCampos();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Contato contato = new Contato
            {
                Nome = txtNome.Text,
                Email = txtEmail.Text,
                NumeroTelefone = txtTelefone.Text
            };

            List<Contato> contatoList = new List<Contato>();

            foreach (Contato contatoDaLista in lbxContatos.Items)
            {
                contatoList.Add(contatoDaLista);
            }

            if (acao == OperacaoEnum.INCLUIR)
            {
                contatoList.Add(contato);
            }
            else
            {
                int indice = lbxContatos.SelectedIndex;
                contatoList.RemoveAt(indice);
                contatoList.Insert(indice, contato);
                lbxContatos.SelectedIndex = indice;
            } 

            ManipuladorDeArquivos.EscreverArquivo(contatoList);
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesInclueAlterarEExcluir(true);
            AlterarEstadoDosCampos(false);
            CarregarListaContatos();
            LimparCampos();
        }

        private void CarregarListaContatos()
        {
            lbxContatos.Items.Clear();
            lbxContatos.Items.AddRange(ManipuladorDeArquivos.LerArquivo().ToArray());
            lbxContatos.SelectedIndex = 0;
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtTelefone.Text = "";
        }

        private void AlterarEstadoDosCampos(bool estado)
        {
            txtNome.Enabled = estado;
            txtEmail.Enabled = estado;
            txtTelefone.Enabled = estado;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AlterarBotoesSalvarECancelar(false);
            AlterarBotoesInclueAlterarEExcluir(true);
            AlterarEstadoDosCampos(false);
        }

        private void lbxContatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            Contato contato = (Contato)lbxContatos.Items[lbxContatos.SelectedIndex];
            txtNome.Text      = contato.Nome;
            txtEmail.Text    = contato.Email;
            txtTelefone.Text = contato.NumeroTelefone;
        }
    }
}

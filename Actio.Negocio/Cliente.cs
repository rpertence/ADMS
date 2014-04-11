using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Actio.Negocio
{
    public class Cliente
    {
        private int usuario_id;
        public int Usuario_Codigo
        {
            get { return usuario_id; }
            set { usuario_id = value; }
        }

        private string usuario_nome;
        public string Usuario_Nome
        {
            get { return usuario_nome; }
            set { usuario_nome = value; }
        }

        private string usuario_email;
        public string Usuario_Email
        {
            get { return usuario_email; }
            set { usuario_email = value; }
        }

        private string usuario_senha;
        public string Usuario_senha
        {
            get { return usuario_senha; }
            set { usuario_senha = value; }
        }

        private string opt_cliente;
        public string Opt_Cliente
        {
            get { return opt_cliente; }
            set { opt_cliente = value; }
        }

        private string usuario_status;
        public string Usuario_Status
        {
            get { return usuario_status; }
            set { usuario_status = value; }
        }

        private string usuario_filiado;
        public string Usuario_Filiado
        {
            get { return usuario_filiado; }
            set { usuario_filiado = value; }
        }

        private string usuario_situacao;
        public string Usuario_Situacao
        {
            get { return usuario_situacao; }
            set { usuario_situacao = value; }
        }

        private string usuario_tipo;
        public string Usuario_Tipo
        {
            get { return usuario_tipo; }
            set { usuario_tipo = value; }
        }

        private string usuario_sexo;
        public string Usuario_Sexo
        {
            get { return usuario_sexo; }
            set { usuario_sexo = value; }
        }

        private string cod_categoria;
        public string Cod_Categoria
        {
            get { return cod_categoria; }
            set { cod_categoria = value; }
        }
        private string cod_categoria_regional;
        public string Cod_Categoria_Regional
        {
            get { return cod_categoria_regional; }
            set { cod_categoria_regional = value; }
        }

        public void Inserir(Cliente cliente)
        {
            new ClienteCSV().Inserir(cliente.Usuario_Nome, cliente.Usuario_Email, cliente.Usuario_senha, cliente.Opt_Cliente, cliente.Usuario_Status, cliente.Usuario_Filiado, cliente.Usuario_Situacao, cliente.Usuario_Tipo, cliente.Usuario_Sexo, cliente.Cod_Categoria, cliente.Cod_Categoria_Regional);
        }
    }
}

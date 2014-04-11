using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Text;
using System.Reflection;

namespace Actio.Negocio
{
    [DataObject(true)]
    public class Coordenador_GO
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string nome, string email, string senha, string telefone, string celular, string nascimento, string endereco, string bairro, string cidade, string estado, string cep, string tipo, string status, string icone)
        {
                string SQL = @"INSERT INTO `coordenador_go` 
                          (`nome`, `email`, `senha`, `telefone`, `celular`, `nascimento`, `endereco`, `bairro`, `cidade`, `estado`, `cep`, `tipo`, `status`, `icone`) 
                          VALUES
                          ('" + nome + "','" + email + "','" + senha + "','" + telefone + "','" + celular + "','" + nascimento + "','" + endereco + "','" + bairro + "','" + cidade + "','" + estado + "','" + cep + "','" + tipo + "','" + status + "','" + icone + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
                string SQL = string.Format(
                    "SELECT u.`id`, u.`nome`, u.`email`, u.`senha`, u.`telefone`, u.`celular`, u.`nascimento`, u.`endereco`, u.`bairro`, u.`cidade`, u.`estado`, u.`cep`, u.`tipo`, u.`status`, u.`icone`, CASE WHEN u.`status` = '1' THEN 'ativo' ELSE 'inativo' END ATIVO, CASE WHEN u.`tipo` = '0' THEN 'usuário' ELSE 'administrador' END TIPOLOGIA FROM coordenador_go u WHERE u.`tipo` != '2' ORDER BY u.`nome` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona po ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT u.`id`, u.`nome`, u.`email`, u.`senha`, u.`telefone`, u.`celular`, u.`nascimento`, u.`endereco`, u.`bairro`, u.`cidade`, u.`estado`, u.`cep`, u.`tipo`, u.`status`, u.`icone` FROM coordenador_go u WHERE u.`id` = '" + id + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar coordenador_go
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string nome, string email, string senha, string telefone, string celular, string nascimento, string endereco, string bairro, string cidade, string estado, string cep, string tipo, string status, string icone)
        {
                string SQL = @"UPDATE coordenador_go SET nome = '" + nome + "', email = '" + email + "', senha = '" + senha + "', telefone = '" + telefone + "', celular = '" + celular + "', nascimento = '" + nascimento + "', endereco = '" + endereco + "', bairro = '" + bairro + "', cidade =  '" + cidade + "', estado = '" + estado + "', cep = '" + cep + "', tipo = '" + tipo + "', status = '" + status + "', icone = '" + icone + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM coordenador_go WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM coordenador_go";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
        #region trata administradores
        #region util
        public Coordenador_GO() { }
        public Coordenador_GO(int id)
        {
            DataRow dr = SelectByIDDataRow(id);

            this.id = int.Parse(dr["id"].ToString());
            this.nome = (string)dr["nome"];
            this.email = (string)dr["email"];
            this.senha = (string)dr["senha"];
            this.tipo = (string)dr["tipo"];
            this.status = (string)dr["status"];
        }
        private Coordenador_GO ConfiguraClasse(DataRow dr)
        {
            Coordenador_GO coordenador_go = new Coordenador_GO();
            coordenador_go.id = int.Parse(dr["id"].ToString());
            coordenador_go.nome = (string)dr["nome"];
            coordenador_go.email = (string)dr["email"];
            coordenador_go.senha = (string)dr["senha"];
            coordenador_go.tipo = (string)dr["tipo"];
            coordenador_go.status = (string)dr["status"];
            return coordenador_go;
        }
        private Coordenador_GO CoordenadorChecado(DataRow dr)
        {
            Coordenador_GO coordenador_go = new Coordenador_GO();
            coordenador_go.checado = (string)dr["STATUS"];
            return coordenador_go;
        }
        #region concatena dados do coordenador_go
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string senha;
        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }
        private string checado;
        public string Checado
        {
            get { return checado; }
            set { checado = value; }
        }
        private string tipo;
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion
        #endregion
        #region Data Row Seleciona por ID abastece a classe usuário
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataRow SelectByIDDataRow(int id)
        {
            string SQL = string.Format("SELECT u.`id`, u.`nome`, u.`email`, u.`senha`, u.`telefone`, u.`celular`, u.`nascimento`, u.`endereco`, u.`bairro`, u.`cidade`, u.`estado`, u.`cep`, u.`tipo`, u.`status`, u.`icone` FROM coordenador_go u WHERE u.`id` = '" + id + "';");

            DataTable dt = conexao.Dados(SQL);

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            throw new Exception("Nenhum usu&aacute;rio encontrado!");
        }
        #endregion
        #region loga usuário
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataRow LogarCoordenador(string email, string senha)
        {
            string SQL = "SELECT u.`id`, u.`nome`, u.`email`, u.`senha`, u.`telefone`, u.`celular`, u.`nascimento`, u.`endereco`, u.`bairro`, u.`cidade`, u.`estado`, u.`cep`, u.`tipo`, u.`status`, u.`icone`, CASE WHEN u.`tipo`= '1' THEN 'administrador' WHEN u.`tipo`= '2' THEN 'MASTER' ELSE 'coordenador_go' END TIPO FROM coordenador_go u WHERE u.`email` = '" + email + "' AND u.`senha` = '" + senha + "' AND u.`status` = '1'";

            DataTable dt = conexao.Dados(SQL);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
        public static DataRow LogarAdministrador(string email, string senha)
        {
            string SQL = "SELECT u.`id`, u.`nome`, u.`email`, u.`senha`, u.`telefone`, u.`celular`, u.`nascimento`, u.`endereco`, u.`bairro`, u.`cidade`, u.`estado`, u.`cep`, u.`tipo`, u.`status`, u.`icone`, CASE WHEN u.`tipo`= '1' THEN 'administrador' WHEN u.`tipo`= '2' THEN 'MASTER' ELSE 'coordenador_go' END TIPO FROM coordenador_go u WHERE u.`email` = '" + email + "' AND u.`senha` = '" + senha + "' AND u.`status` = '1';";

            DataTable dt = conexao.Dados(SQL);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
        public static DataRow LogarMaster(string email, string senha)
        {
            string SQL = "SELECT u.`id`, u.`nome`, u.`email`, u.`senha`, u.`telefone`, u.`celular`, u.`nascimento`, u.`endereco`, u.`bairro`, u.`cidade`, u.`estado`, u.`cep`, u.`tipo`, u.`status`, u.`icone`, CASE WHEN u.`tipo`= '1' THEN 'administrador'  WHEN u.`tipo`= '2' THEN 'MASTER' ELSE 'coordenador_go' END TIPO FROM coordenador_go u WHERE u.`email` = '" + email + "' AND u.`senha` = '" + senha + "' AND u.`status` = '1' AND u.`tipo` = '2'";

            DataTable dt = conexao.Dados(SQL);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
        public Coordenador_GO BuscaCoordenadorPorLogin(string email, string senha)
        {
            DataRow dr = LogarCoordenador(email, senha);
            if (dr == null) return null;
            return ConfiguraClasse(dr);
        }
        public Coordenador_GO BuscaAdministradorPorLogin(string email, string senha)
        {
            DataRow dr = LogarAdministrador(email, senha);
            if (dr == null) return null;
            return ConfiguraClasse(dr);
        }

        #region Checa se já existe login ou status do usuário
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataRow ChecarLogin(string email)
        {
            string SQL = "SELECT u.`id`, u.`nome`, u.`tipo`, u.`email`, CASE WHEN u.`status`= '0' THEN 'credencial j&aacute; cadastrada! seu cadastro est&aacute; em an&aacute;lise no Pelo Administrador do Sistema e em breve ser&aacute; liberado, caso queira esclarecer alguma dúvida clique em contato!' ELSE 'Credencial j&aacute; cadastrada, clique em entrar para acessar o conteúdo completo do portal!<br />Caso queira atualizar seus dados primeiramente clique em entrar.' END STATUS FROM coordenador_go u WHERE u.`email` = '" + email + "'";

            DataTable dt = conexao.Dados(SQL);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
        public Coordenador_GO ChecaCoordenadorPorEmail(string email)
        {
            DataRow dr = ChecarLogin(email);
            if (dr == null) return null;
            return CoordenadorChecado(dr);
        }

        #endregion
        #endregion
        #region busca Coordenadors por parâmetros
        #region busca por nome e e-mail
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SearchByName(string nome)
        {
            string SQL = string.Format("SELECT u.`id`, u.`nome`, u.`email`, u.`senha`, u.`telefone`, u.`celular`, u.`nascimento`, u.`endereco`, u.`bairro`, u.`cidade`, u.`estado`, u.`cep`, u.`tipo`, u.`status`, u.`icone`, CASE WHEN u.`tipo`= '1' THEN 'administrador' WHEN u.`tipo` = '2' THEN 'MASTER' ELSE 'coordenador_go' END TIPO FROM coordenador_go u WHERE u.`nome` LIKE '%{0}%' OR u.`email` LIKE '%{0}%'", nome.ToString());
            return conexao.Dados(SQL);
        }
        public static DataTable SearchByEmail(string email)
        {
            string SQL = string.Format("SELECT u.`id`, u.`nome`, u.`email`, u.`senha`, u.`telefone`, u.`celular`, u.`nascimento`, u.`endereco`, u.`bairro`, u.`cidade`, u.`estado`, u.`cep`, u.`tipo`, u.`status`, u.`icone`, CASE WHEN u.`tipo`= '1' THEN 'administrador' WHEN u.`tipo` = '2' THEN 'MASTER' ELSE 'coordenador_go' END TIPO FROM coordenador_go u WHERE u.`email` = '" + email + "'");
            return conexao.Dados(SQL);
        }

        #endregion
        #region busca por Categoria Principal
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SearchByCategoria(string tipo)
        {
            string SQL = string.Format("SELECT (u.`id`) codigo, (u.`nome`) nome, (u.`email`) email FROM coordenador_go u WHERE u.`cod_categoria` = '" + tipo + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #endregion

        #endregion
        #region recuperação de senha
        #region Recupera Senha
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataRow RecuperaLogarCoordenador(string email)
        {
            string SQL = "SELECT u.`id`, u.`nome`, u.`tipo`, u.`email`, u.`senha`, CASE WHEN u.`tipo`= '1' THEN 'administrador' ELSE 'coordenador_go' END TIPO FROM coordenador_go u WHERE u.`email` = '" + email + "' AND u.`status` = '1'";

            DataTable dt = conexao.Dados(SQL);

            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }
        public Coordenador_GO BuscaCoordenadorPorEmail(string email)
        {
            DataRow dr = RecuperaLogarCoordenador(email);
            if (dr == null) return null;
            return ConfiguraClasse(dr);
        }
        #endregion
        #region Atualizar coordenador_go login e senha no site
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void AtualizarLoginSenha(string email, string senha)
        {
            string SQL = @"UPDATE coordenador_go SET senha = '" + senha + "' WHERE email = '" + email + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Atualizar coordenador_go
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void AtualizarNoSite(string id, string nome, string email, string endereco, string bairro, string cidade, string estado, string cep, string celular, string telefone,  string nascimento, string sexo, string opt_cliente)
        {
            string SQL = @"UPDATE coordenador_go SET nome = '" + nome + "', email = '" + email + "', endereco = '" + endereco + "', bairro = '" + bairro + "', cidade = '" + cidade + "', estado = '" + estado + "', cep = '" + cep + "', telefone = '" + telefone + "', celular = '" + celular + "', nascimento = '" + nascimento + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #endregion
    }
}

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
    public class GrupodeOracao
    {
        #region Novo
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public static void Inserir(string id_usuario, string titulo, string regiao, string paroquia, string bairro, string cidade, string endereco, string telefone, string email, string site, string onibus, string dia, string hora, string descricao, string icone, string status, string forania)
        {
                string SQL = @"INSERT INTO `grupodeoracao` 
                          (`id_usuario`, `titulo`, `regiao`, `paroquia`, `bairro`, `cidade`, `endereco`, `telefone`, `email`, `site`, `onibus`, `dia`, `hora`, `descricao`, `icone`, `status`, `forania`) 
                          VALUES
                          ('" + id_usuario + "','" + titulo + "','" + regiao + "','" + paroquia + "','" + bairro + "','" + cidade + "','" + endereco + "','" + telefone + "','" + email + "','" + site + "','" + onibus + "','" + dia + "','" + hora + "','" + descricao + "', '" + icone + "', '" + status + "', '" + forania + "');";

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
                string SQL = string.Format("SELECT "+
                "(SELECT c.`nome` FROM coordenador_go c WHERE c.`id` = go.`id_usuario`) coordenador, " +
                "go.`id`, go.`id_usuario`, go.`titulo`, go.`regiao`, go.`paroquia`, go.`bairro`, go.`cidade`, go.`endereco`, go.`telefone`, go.`email`, go.`site`, go.`onibus`, go.`dia`, go.`hora`, go.`descricao`, go.`icone`, go.`status`, go.`forania`, CASE WHEN go.`status` = '1' THEN 'ativo' ELSE 'inativo' END ATIVO FROM grupodeoracao go ORDER BY go.`regiao` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona po ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT go.`id`, go.`id_usuario`, go.`titulo`, go.`regiao`, go.`paroquia`, go.`bairro`, go.`cidade`, go.`endereco`, go.`telefone`, go.`email`, go.`site`, go.`onibus`, go.`dia`, go.`hora`, go.`descricao`, go.`icone`, go.`status`, go.`forania` FROM grupodeoracao go WHERE go.`id` = '" + id + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar Grupo de Oração
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public static void Atualizar(string id, string id_usuario, string titulo, string regiao, string paroquia, string bairro, string cidade, string endereco, string telefone, string email, string site, string onibus, string dia, string hora, string descricao, string icone, string status, string forania)
        {
                string SQL = @"UPDATE grupodeoracao SET id_usuario = '" + id_usuario + "', titulo = '" + titulo + "', regiao = '" + regiao + "', paroquia = '" + paroquia + "', bairro = '" + bairro + "', cidade = '" + cidade + "', endereco = '" + endereco + "', telefone = '" + telefone + "', email =  '" + email + "', site = '" + site + "', onibus = '" + onibus + "', dia = '" + dia + "', hora = '" + hora + "', descricao = '" + descricao + "', icone = '" + icone + "', status = '" + status + "', forania = '" + forania + "' WHERE id = '" + id + "' LIMIT 1";
                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Delete(int id)
        {
            string SQL = string.Format("DELETE FROM grupodeoracao WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM grupodeoracao";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
        #region busca no site
        #region Seleciona por DIA 
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByParametros(string coluna, string parametro)
        {
            string SQL = string.Format("SELECT go.`id`, go.`id_usuario`, go.`titulo`, go.`regiao`, go.`paroquia`, go.`bairro`, go.`cidade`, go.`endereco`, go.`telefone`, go.`email`, go.`site`, go.`onibus`, go.`dia`, go.`hora`, go.`descricao`, go.`icone`, go.`status`, go.`forania` FROM grupodeoracao go WHERE go.`" + coluna + "` = '" + parametro + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todos os bairros
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllBairros()
        {
            string SQL = string.Format("SELECT g.`bairro` FROM grupodeoracao g group by g.`bairro` order by g.`bairro` asc;");
            return conexao.Dados(SQL);
        }
        #endregion   
        #region seleciona todas as cidades
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllCidades()
        {
            string SQL = string.Format("SELECT g.`cidade` FROM grupodeoracao g group by g.`cidade` order by g.`cidade` asc;");
            return conexao.Dados(SQL);
        }
        #endregion    
        #region seleciona todas as faranias
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllForanias()
        {
            string SQL = string.Format("SELECT g.`forania` FROM grupodeoracao g group by g.`forania` order by g.`forania` asc;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todas as paroquias
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAllParoquias()
        {
            string SQL = string.Format("SELECT g.`paroquia` FROM grupodeoracao g group by g.`paroquia` order by g.`paroquia` asc;");
            return conexao.Dados(SQL);
        }
        #endregion
        #endregion
    }
}

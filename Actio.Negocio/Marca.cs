using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Text;
using System.Reflection;

namespace Actio.Negocio
{
    [DataObject(true)]
    public class Marca
    {
        #region Novo 
        public static void Inserir(string descricao)
        {
            string SQL = string.Format("INSERT INTO marca (descricao) VALUES ('{0}')", descricao);
            conexao.ExecuteNonQuery(SQL);            
        }
        #endregion
        #region Seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT id, descricao FROM marca ORDER BY descricao");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectOne(int id)
        {
            string SQL = string.Format("SELECT id, descricao FROM marca WHERE id = {0}", id);
            return conexao.Dados(SQL);
        }
        public static DataTable SelectBySubCategoria(int idSubCategoria)
        {
            string SQL = string.Format(@"select *
from marca
where id in (select id_marca from produtos where id_subcategoria = {0})
order by descricao", idSubCategoria);

            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void Update(int id, string descricao)
        {
            string SQL = string.Format(@"UPDATE marca
SET descricao = '{1}'
WHERE id = {0}
LIMIT 1", id, descricao);

                conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir item
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(int id)
        {
            string SQL = string.Format("DELETE FROM marca WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM marca";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }
        #endregion
    }
}
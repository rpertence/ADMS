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
    public class Artigos
    {
        #region Novo
        public static void Inserir
            (
            string titulo,
            string resumo,
            string descricao,
            string data,
            string ordem,
            string privativo,
            string destaque,
            string status,
            string id_autor
            )
        {
            if (destaque.ToString() == "1")
            {
                string SQLU = @"UPDATE artigos SET destaque = '0' WHERE id_autor = '" + id_autor + "'";
                conexao.ExecuteNonQuery(SQLU);
            }

            string SQL = @"INSERT INTO `artigos` 
                          (`titulo`, `resumo`, `descricao`, `data`, `ordem`, `status`, `destaque`,  `id_autor`) 
                          VALUES
                          ('" + titulo + "','" + resumo + "','" + descricao + "','" + data + "','" + ordem + "','" + status + "','" + destaque + "','" + id_autor + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT "+
                "(SELECT aa.`nome` FROM artigo_autor aa WHERE aa.`id` = a.`id_autor`) autor_nome, " +
                "(SELECT aa.`id` FROM artigo_autor aa WHERE aa.`id` = a.`id_autor`) id_autor_id, " +
                "a.`id`, a.`titulo`, a.`resumo`, a.`descricao`, a.`data`, a.`ordem`, a.`privativo`, a.`destaque`, a.`status`, a.`id_autor`, CASE WHEN a.`status` = '1' THEN 'ativo' ELSE 'inativo' END ATIVO, CASE WHEN a.`destaque` = '1' THEN 'destaque' ELSE ' ' END destaques, CASE WHEN a.`privativo` = '1' THEN 'privado' ELSE 'público' END PRIVATIVO FROM artigos a ORDER BY a.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByID(int id)
        {
            string SQL = string.Format("SELECT " +
                "(SELECT aa.`nome` FROM artigo_autor aa WHERE aa.`id` = a.`id_autor`) autor_nome, " +
                "(SELECT aa.`email` FROM artigo_autor aa WHERE aa.`id` = a.`id_autor`) autor_email, " +
                "(SELECT aa.`id` FROM artigo_autor aa WHERE aa.`id` = a.`id_autor`) id_autor_id, " +
                "(SELECT aa.`icone` FROM artigo_autor aa WHERE aa.`id` = a.`id_autor`) icone, " +                
                "id, titulo, resumo, descricao, data, ordem, privativo, destaque, status, id_autor, visitas FROM artigos a WHERE a.id = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona por ID do Autor
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectByIDAutor(int autor)
        {
            string SQL = string.Format("SELECT id, titulo, resumo, descricao, data, ordem, privativo, destaque, status, id_autor, CASE WHEN destaque = '1' THEN 'destaque' ELSE '' END destaques, CASE WHEN status = '1' THEN 'ativo' ELSE 'inativo' END ATIVO FROM artigos a WHERE a.id_autor = '" + autor + "' ORDER BY a.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar
        public static void Atualizar(string id, string titulo, string resumo, string descricao, string data, string ordem, string privativo, string destaque, string status, string id_autor)
        {
            if (destaque == "1")
            {
                string SQLU = @"UPDATE artigos SET destaque = '0' WHERE id_autor = '" + id_autor + "'";
                conexao.ExecuteNonQuery(SQLU);
            }
            string SQL = @"UPDATE artigos SET titulo = '" + titulo + "', resumo = '" + resumo + "', descricao = '" + descricao + "', data = '" + data + "', ordem = '" + ordem + "', privativo = '" + privativo + "', destaque = '" + destaque + "', status =  '" + status + "', id_autor = '" + id_autor + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir 
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(int id)
        {
            string SQL = string.Format("DELETE FROM artigos WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir por id do Autor
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void ExcluirByIdAutor(int autor)
        {
            string SQL = string.Format("DELETE FROM artigos WHERE id_autor = {0}", autor.ToString());
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM artigos";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion
        #region páginas públicas
        #region artigos em destaque todos autores
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable ArtigoEmDestaque()
        {
            string SQL = "SELECT " +
                " (SELECT a.`nome` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) autor," +
                " (SELECT a.`icone` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) icone," +
                "aa.`id`, aa.`titulo`, aa.`resumo` FROM artigos aa WHERE aa.`destaque` = '1'";
            return conexao.Dados(SQL);
        }
        #endregion
        #region artigos em destaque do autor
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable ArtigosEmDestaqueAutorSelecionado(string id_autor)
        {
            string SQL = string.Format("SELECT " +
                " (SELECT a.`nome` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) autor," +
                " (SELECT a.`icone` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) icone," +
                "aa.`id`, aa.`titulo`, aa.`resumo`, aa.`visitas` FROM artigos aa WHERE aa.`destaque` = '1' AND aa.id_autor = '" + id_autor + "'");
            return conexao.Dados(SQL);
        }
        #endregion
        #region artigos Selecionado
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable ArtigoSelecionado(string id)
        {
            string SQL = string.Format("SELECT " +
                " (SELECT a.`nome` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) autor," +
                " (SELECT a.`icone` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) icone," +
                " (SELECT a.`email` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) email," +
                " (SELECT a.`id` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) id_autor," +
                " (SELECT a.`descricao` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) descricao_autor," +

                "aa.`id`, aa.`titulo`, aa.`descricao`, aa.`resumo`, aa.`visitas` FROM artigos aa WHERE aa.id = '" + id + "'");
            return conexao.Dados(SQL);
        }
        #endregion
        #region artigos por autor selecionado
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable artigosAtivosPorAutor(string id_autor)
        {
            string SQL = string.Format("SELECT " +
                                       " (SELECT a.`nome` FROM `artigo_autor` a " +
                                            " WHERE a.`id` = '" + id_autor + "') autor," +
                                            " (SELECT a.`icone` FROM `artigo_autor` a " +
                                            " WHERE a.`id` = '" + id_autor + "') icone, " + 
                                            "id, titulo, resumo, descricao, data, ordem, privativo, destaque, status, id_autor FROM artigos a WHERE a.`status` = '1' AND a.id_autor = '" + id_autor + "' ORDER BY a.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region todos artigos ativos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable artigosAtivos()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, descricao, data, ordem, privativo, destaque, status, id_autor, visitas FROM artigos a WHERE a.`status` = '1' ORDER BY a.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region todos artigos ativos mais lidos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable TodosArtigosAtivosMaisLidos()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, descricao, data, ordem, privativo, destaque, status, id_autor, visitas FROM artigos a WHERE a.`status` = '1' ORDER BY a.`visitas` DESC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region 3 artigos ativos mais lidos
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable TresArtigosAtivosMaisLidos()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, descricao, data, ordem, privativo, destaque, status, id_autor, visitas FROM artigos a WHERE a.`status` = '1' ORDER BY a.`visitas` DESC LIMIT 3;");
            return conexao.Dados(SQL);
        }
        #endregion       
        #region artigos por autor selecionado sem o destaque
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable artigosAtivosPorAutorSemDestaque(string id_autor)
        {
            string SQL = string.Format("SELECT id, titulo, resumo, descricao, data, ordem, privativo, destaque, status, id_autor FROM artigos a WHERE a.`status` = '1' AND a.`destaque` = '0' AND a.`id_autor` = '" + id_autor + "' ORDER BY a.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Ler Artigo Selecionada
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable AritgoSelecionado(int id)
        {
            string SQL = string.Format("SELECT a.`titulo`, a.`resumo`, a.`descricao`, a.`data`, a.`privativo`, a.`id_autor`, CASE WHEN a.privativo = '0' THEN a.`descricao` ELSE '<H3><FONT color=#ff0000>Conte&uacute;do de acesso exclusivo</FONT></H3><P>O conte&uacute;do deste artigo so pode ser visualizado por usu&aacute;rios logados!</P><P>Se voc&ecirc; possui uma credencial em nosso portal clique em &Aacute;REA EXLUSIVA DO ASSOCIADO e fa&ccedil;a o seu login.</P><P>Caso n&atilde;o tenha, fa&ccedil;a seu cadastro em nosso portal clicando em ATUALIZE SEUS DADOS.</A></P>' END PRIVATIVO, CASE WHEN a.privativo = '1' THEN a.`resumo` ELSE ' ' END PUBLICO FROM artigos a WHERE a.`id` = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona autor pelo id do artigo selecionado para leitura
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAutorIdArquivoSelecionado(string id)
        {
            string SQL = "SELECT " +
                " (SELECT a.`nome` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) nome," +
                " (SELECT a.`id` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) id," +
                " (SELECT a.`icone` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) icone," +
                " (SELECT a.`descricao` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) descricao," +
                " (SELECT a.`email` FROM `artigo_autor` a " +
                " WHERE a.`id` = aa.`id_autor`) email," +
                "aa.`id`, aa.`id_autor`, aa.`titulo`, aa.`descricao`, aa.`data` FROM artigos aa WHERE aa.`id` = '" + id + "'";
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar Visita
        public static void AtualizarVisita(string id, string valor)
        {
            string SQL = @"UPDATE artigos SET visitas = '" + valor + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #endregion
    }
}
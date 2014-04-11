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
    public class Noticias
    {
        #region Nova Notícia
        public static void Inserir(string titulo, string resumo, string descricao, string data, string ordem, string miniatura, string destaque, string status, string icone, string destaque_b)
        {
            if (destaque == "1")
            {
                string SQLD = @"UPDATE noticias SET destaque = '0';";
                conexao.ExecuteNonQuery(SQLD);
            }
            string SQL = @"INSERT INTO `noticias` 
                          (`titulo`, `resumo`, `descricao`, `data`, `ordem`, `miniatura`, `destaque`, `status`, `icone`, `destaque_b`) 
                          VALUES
                          ('" + titulo + "','" + resumo + "','" + descricao + "','" + data + "','" + ordem + "','" + miniatura + "','" + destaque + "','" + status + "','" + icone + "','" + destaque_b + "');";

            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region seleciona todas as Noticías
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectAll()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, ordem, data, status, destaque, ordem, destaque_b, CASE WHEN status = '1' THEN 'ativa' ELSE 'inativa' END ATIVO, CASE WHEN destaque = '1' THEN 'Destaque Principal' ELSE '' END distak, CASE WHEN destaque_b = '1' THEN 'Destaque Secundário' ELSE '' END distak_b FROM noticias n ORDER BY n.ordem ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona uma noticia
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Noticia(int id)
        {
            string SQL = string.Format("SELECT id, titulo, resumo, descricao, data, ordem, miniatura, destaque, status, icone, destaque_b, visitas FROM noticias n WHERE n.id = {0}", id.ToString());
            return conexao.Dados(SQL);
        }
        #endregion
        #region Atualizar noticias
        public static void Atualizar(string id, string titulo, string resumo, string descricao, string data, string ordem, string miniatura, string destaque, string status, string icone, string destaque_b)
        {
            string SQL = @"UPDATE noticias SET titulo = '" + titulo + "', resumo = '" + resumo + "', descricao = '" + descricao + "', data = '" + data + "', ordem = '" + ordem + "', miniatura = '" + miniatura + "', destaque = '" + destaque + "', status =  '" + status + "', icone = '" + icone + "', destaque_b = '" + destaque_b + "' WHERE id = '" + id + "' LIMIT 1";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #region Exluir noticia
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public static void Excluir(int id)
        {
            string SQL = string.Format("DELETE FROM noticias WHERE id = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQL);
            string SQLA = string.Format("DELETE FROM anexo WHERE anexo_id_dono = {0}", id.ToString());
            conexao.ExecuteNonQuery(SQLA);
        }
        #endregion
        #region proximo id
        public static int nextID
        {
            get
            {
                string SQL = "SELECT COUNT(*) + 1 nextID FROM noticias";
                return int.Parse(conexao.ExecuteScalar(SQL));
            }
        }

        #endregion      
        #region páginas públicas
        #region seleciona as Noticías mais comentadas
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectMaisComentadas()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, ordem FROM noticias n WHERE n.`status` = '1' AND n.`comentarios` != null ORDER BY n.comentarios ASC");
            return conexao.Dados(SQL);
        }
        #endregion 
        #region seleciona as Noticías mais Visitadas
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectMaisVisitadas()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, ordem FROM noticias n WHERE n.`status` = '1' ORDER BY n.visitas DESC LIMIT 3");
            return conexao.Dados(SQL);
        }
        #endregion
        #region seleciona todas as Noticías mais Visitadas
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable SelectTodasMaisVisitadas()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, ordem, data FROM noticias n WHERE n.`status` = '1' ORDER BY n.visitas DESC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona Noticia do Destaque
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable NoticiaDestaque()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, icone, status, miniatura, destaque FROM noticias n WHERE n.destaque = '1' AND n.status = '1'");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona Noticia do Destaque Secundário
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable NoticiaDestaqueSeundario()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, destaque_b, status, data, icone FROM noticias n WHERE n.destaque_b = '1' AND n.status = '1'");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona Noticia do Destaque Sem Imagem por data
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable NoticiaDestaqueTitulo()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, data, status FROM noticias n WHERE n.destaque_b = '0' AND n.destaque = '0' AND n.status = '1' ORDER BY n.data ASC LIMIT 1");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona Noticia Ativas da Página Noticias
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable NoticiasTodasAtivas()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, ordem, miniatura, descricao, data FROM noticias n WHERE n.status = '1' ORDER BY n.`ordem` ASC;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona Noticia 5 Ativas da HOME
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable Noticias5UltimasAtivas()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, ordem, data FROM noticias n WHERE n.status = '1' ORDER BY n.`ordem` ASC LIMIT 5;");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Seleciona Noticia Ativas com icone
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable NoticiasTodasAtivasComIcone()
        {
            string SQL = string.Format("SELECT id, titulo, resumo, ordem, data, icone FROM noticias n WHERE n.status = '1' AND n.`destaque` = '1' ORDER BY n.`ordem` ASC");
            return conexao.Dados(SQL);
        }
        #endregion
        #region Ler Noticia Selecionada
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public static DataTable NoticiaSelecionada(int id)
        {
            string SQL = string.Format("SELECT n.`titulo`, n.`descricao`, n.`data`, n.`visitas`, n.`miniatura` FROM noticias n WHERE n.`id` = '" + id + "';");
            return conexao.Dados(SQL);
        }
        #endregion
        #region atualiza visita
        public static void Visita(string id, string valor)
        {
            string SQL = @"UPDATE noticias SET visitas = '" + valor + "' WHERE id = '" + id + "';";
            conexao.ExecuteNonQuery(SQL);
        }
        #endregion
        #endregion
    }
}
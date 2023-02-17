using Microsoft.AspNetCore.Http.HttpResults;
using Oracle.ManagedDataAccess.Client;
using SegundaPracticaXiangZhou.Models;
using System.Data;

#region PROCEDURE

//CREATE OR REPLACE PROCEDURE SP_INSERT_COMIC
//( P_NOMBRE NVARCHAR2
//, P_IMAGEN NVARCHAR2
//, P_DESCRIPCION NVARCHAR2
//)
//AS
//BEGIN
//  INSERT INTO COMICS VALUES ((SELECT MAX(IDCOMIC)+1 FROM COMICS) , P_NOMBRE, P_IMAGEN, P_DESCRIPCION);
//commit;
//END;
#endregion


namespace SegundaPracticaXiangZhou.Repositories
{
    public class RepositoryComicOracle : IRepository
    {
        private OracleConnection cn;
        private OracleCommand com;
        private OracleDataAdapter adapter;
        private DataTable tablaComic;

        public RepositoryComicOracle()
        {
            //CONEXION
            string connectionString = "User Id=SYSTEM;Password=oracle; Data Source=localhost:1521/XE";


            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            string sql = "select * from comics";
            this.adapter = new OracleDataAdapter(sql, connectionString);
            this.tablaComic = new DataTable();
            this.adapter.Fill(this.tablaComic);
        }

        //LISTA COMIC
        public List<Comic> GetComics()
        {
            var consulta = from datos in this.tablaComic.AsEnumerable()
                           select new Comic
                           {
                               IdComic = datos.Field<int>("IDCOMIC"),
                               Nombre = datos.Field<string>("NOMBRE"),
                               Imagen = datos.Field<string>("IMAGEN"),
                               Descripcion = datos.Field<string>("DESCRIPCION"),

                           };

            return consulta.ToList();
        }

        //INSERTAR
        public void InsertComic(string nombre, string imagen, string descripcion)
        {
            OracleParameter pamape = new OracleParameter(":p_nombre", nombre);
            this.com.Parameters.Add(pamape);
            OracleParameter pamesp = new OracleParameter(":p_imagen", imagen);
            this.com.Parameters.Add(pamesp);
            OracleParameter pamsal = new OracleParameter(":p_descripcion", descripcion);
            this.com.Parameters.Add(pamsal);

            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}

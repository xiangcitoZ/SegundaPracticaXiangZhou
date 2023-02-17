
using System.Data.SqlClient;
using System.Data;
using SegundaPracticaXiangZhou.Models;



#region PROCEDURE

//CREATE PROCEDURE SP_INSERT_COMIC(@nombre NVARCHAR, @imagen NVARCHAR,
//@descripcion NVARCHAR)
//as
//    DECLARE @IDCOMIC INT
//	SELECT @IDCOMIC =  MAX(IDCOMIC) +1  FROM COMICS
//	Insert into COMICS values (@IDCOMIC, @nombre, @imagen
//    , @descripcion )

//go

#endregion

namespace SegundaPracticaXiangZhou.Repositories
{
    public class RepositoryComicSQL : IRepository
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataAdapter adapter;
        private DataTable tablaComic;


        public RepositoryComicSQL()
        {
            string connectionString =
               @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sql = "select * from comics";
            this.adapter = new SqlDataAdapter(sql, connectionString);
            this.tablaComic = new DataTable();
            this.adapter.Fill(this.tablaComic);
        }
        public List<Comic> GetComics()
        {
            var consulta = from datos in this.tablaComic.AsEnumerable()
                           select datos;
            List<Comic> comics = new List<Comic>();
            foreach (var row in consulta)
            {
                Comic comic = new Comic
                {
                    IdComic = row.Field<int>("IDCOMIC"),
                    Nombre = row.Field<string>("NOMBRE"),
                    Imagen = row.Field<string>("IMAGEN"),
                    Descripcion = row.Field<string>("DESCRIPCION"),
                    
                };
                comics.Add(comic);
            }
            return comics;
        }

        public void InsertComic( string nombre, string imagen, string descripcion)
        {
            SqlParameter pamnom = new SqlParameter("@nombre", nombre);
            this.com.Parameters.Add(pamnom);
            SqlParameter pamima= new SqlParameter("@imagen", imagen);
            this.com.Parameters.Add(pamima);
            SqlParameter pamdes = new SqlParameter("@descripcion", descripcion);
            this.com.Parameters.Add(pamdes);
            
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_COMIC";
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}

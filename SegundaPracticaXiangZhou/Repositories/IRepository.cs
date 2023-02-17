using SegundaPracticaXiangZhou.Models;

namespace SegundaPracticaXiangZhou.Repositories
{
    public interface IRepository
    {
        List<Comic> GetComics();

        void InsertComic( string nombre,
           string imagen, string descripcion);
    }
}

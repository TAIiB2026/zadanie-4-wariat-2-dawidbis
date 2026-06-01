using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IGetDataInterface
    {
        IEnumerable<Ksiazka> Get();
        Ksiazka GetByID(int id);
    }
}
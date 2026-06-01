using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IFormSubmitInterface
    {
        void Add(Ksiazka ksiazka);
        void Edit(Ksiazka ksiazka);
    }
}
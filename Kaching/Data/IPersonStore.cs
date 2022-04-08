using Kaching.Models;

namespace Kaching.Data
{
    public interface IPersonStore
    {
        public void CreateNewPerson(Person person);
    }
}

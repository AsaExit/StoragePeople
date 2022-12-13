using StoragePeople.Models.Enterties;

namespace StoragePeople.Models.Repos
{
    public interface ILanguageRepo
    {
        Language Create(Language language);
        List<Language> GetAll();
        Language FindById(int id);

        bool Update(Language language);
        bool Delete(Language language);
    }
}

using StoragePeople.Models.Enterties;
using StoragePeople.Models.Repos;
using StoragePeople.Models.ViewModels;
using System.Linq;

namespace StoragePeople.Models.Services
{
    public class PeopleService:IPeopleService
    {
        IPeopleRepo _peopleRepo;
        readonly ICityRepo _cityRepo;
        private readonly ILanguageRepo _languageRepo;

        public PeopleService(IPeopleRepo peopleRepo,ICityRepo cityRepo, ILanguageRepo languageRepo)
        {
            _peopleRepo = peopleRepo;
            _cityRepo = cityRepo;
            _languageRepo = languageRepo;
        }
        public Person Create(CreatePersonViewModel createPerson)
        {

            if (string.IsNullOrWhiteSpace(createPerson.Name))

            {
                throw new ArgumentException("Name,PhoneNumber or City, not be consist of backspace(s)/whitespace(s)");
            }
            Person person = new Person()
            {
                Name = createPerson.Name,
                PhoneNumber = createPerson.PhoneNumber,
                CityId = createPerson.CityId
            };
            _peopleRepo.Create(person);
            return person;
        }
        public List<Person> GetAll()
        {
            return _peopleRepo.GetAll();
        }

        public List<Person> Search(string search)
        {
            List<Person> searchPerson = _peopleRepo.GetAll();
            //
            foreach (Person item in _peopleRepo.GetAll())
            {
                if (item.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                {
                    searchPerson.Add(item);
                }
            }
            //searchPerson = searchPerson.Where(p => p.Name.ToUpper().Contains(search.ToUpper()) || p.City.Contains(search.ToUpper())).ToList();
            if (searchPerson.Count == 0)
            {
                throw new ArgumentException("Could not find what you where looking for");
            }
            return searchPerson;
        }
        public Person FindById(int id)
        {
            //return _peopleRepo.Read(id);
            Person foundPerson = _peopleRepo.GetById(id);

            return foundPerson;
        }
        public bool Edit(int id, CreatePersonViewModel editPerson)
        {
            Person person = _peopleRepo.GetById(id);
            if (person != null)
            {
                person.Name = editPerson.Name;
                person.CityId = editPerson.CityId;
                person.PhoneNumber = editPerson.PhoneNumber;
            }
            return _peopleRepo.Update(person);
        }

        public void Remove(int id)
        {
            Person personToDelete = _peopleRepo.GetById(id);
            if (personToDelete != null)
            {
                _peopleRepo.Delete(personToDelete);
            }
        }

        public PersonLanguageViewModel PersonLanguage(Person person)
        {

            PersonLanguageViewModel personLanguage = new PersonLanguageViewModel();
            personLanguage.Person = person;
            List<Language> allLanguages = _languageRepo.GetAll();

            foreach (Language pLanguage in person.PersonLanguages)
            {
                Language language = allLanguages.Single(l => l.Id == pLanguage.Id);
                // l= is a short version for "language"
                personLanguage.SpeakesLanguage.Add(language);
                allLanguages.Remove(language);
            }
            personLanguage.AllLanguages = allLanguages;
            return personLanguage;


        }

        //public void Removelanguage(Person person, int languageid)
        //{
        //    PersonLanguage language = person.PersonLanguages.SingleOrDefault(pL => pL.LanguageId == languageId);
        //    // pL= is short version for peoleLanguage
        //    person.PersonLanguages.Remove(language);
        //    _peopleRepo.Update(person);
        //}

        public void AddLanguage(Person person, int languageId)
        {
            Language language = new Language()
            {
                Id= person.Id,
                
            };

            person.PersonLanguages.Add(language);

            _peopleRepo.Update(person);
        }
    }
}

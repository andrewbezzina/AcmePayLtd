using AcmePayLtdLibrary.Models;
using System.Collections.Generic;

namespace AcmePayLtdLibrary.DataAccess
{
    public interface IPersonDataAccess
    {
        List<PersonModel> GetPeople();
        PersonModel InsertPerson(string firstName, string lastName);
    }
}
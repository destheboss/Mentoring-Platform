using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISearchDataAccess
    {
        List<IPerson> SearchByName(string query);

        List<Mentor> GetMentorsBySpecialty(string specialty);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISuggestionDataAccess
    {
        IEnumerable<int> GetSuggestionHistory(int userId);
        void AddSuggestions(int userId, IEnumerable<int> mentorIds);
        void ClearPreviousSuggestions(int userId);
    }
}
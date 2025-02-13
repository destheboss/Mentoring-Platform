﻿using BusinessLogicLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ISuggestionManager
    {
        IEnumerable<Mentor> SuggestMentorsForMentee(string menteeEmail);
    }
}
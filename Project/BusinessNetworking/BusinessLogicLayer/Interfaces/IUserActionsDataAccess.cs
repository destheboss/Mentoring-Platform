using BusinessLogicLayer.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUserActionsDataAccess
    {
        void SuspendUser(User user);
        void UnsuspendUser(User user);
    }
}
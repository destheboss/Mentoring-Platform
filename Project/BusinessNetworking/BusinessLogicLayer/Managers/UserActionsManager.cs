using BusinessLogicLayer.Common;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Managers
{
    public class UserActionsManager
    {
        private readonly IUserActionsDataAccess data;

        public UserActionsManager(IUserActionsDataAccess data)
        {
            this.data = data;
        }
        public void SuspendUser(User user)
        {
            data.SuspendUser(user);
        }

        public void UnsuspendUser(User user)
        {
            data.UnsuspendUser(user);
        }
    }
}
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.EntityStates
{
    public class EntityStateManager
    {
        private IEntityState currentState;

        public EntityStateManager()
        {
            //Default olarak Added belirlendi
            currentState = new AddedState();
        }

        public void SetState(IEntityState state)
        {
            currentState = state;
        }

        public void HandleState(EntityEntry entityEntry, string userId)
        {
            currentState.HandleState(entityEntry, userId);
        }
    }
}

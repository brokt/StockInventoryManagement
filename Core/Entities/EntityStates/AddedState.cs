using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.EntityStates
{
    public class AddedState : IEntityState
    {
        public void HandleState(EntityEntry entityEntry, string userId)
        {
            if (entityEntry.CurrentValues.Properties.Any(a => a.Name.Contains("CreatedAt")))
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    entityEntry.Property("UpdatedAt").IsModified = false;
                    entityEntry.Property("UpdatedById").IsModified = false;
                    entityEntry.Property("DeletedAt").IsModified = false;
                    entityEntry.Property("DeletedById").IsModified = false;
                    entityEntry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    entityEntry.Property("CreatedById").CurrentValue = Convert.ToInt32(userId);
                }
            }
        }
    }
}

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.EntityStates
{
    public class ModifiedState : IEntityState
    {
        public void HandleState(EntityEntry entityEntry, string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                entityEntry.Property("CreatedAt").IsModified = false;
                entityEntry.Property("CreatedById").IsModified = false;
                entityEntry.Property("UpdatedAt").IsModified = false;
                entityEntry.Property("UpdatedById").IsModified = false;
                entityEntry.Property("DeletedAt").IsModified = false;
                entityEntry.Property("DeletedById").IsModified = false;

                if (entityEntry.CurrentValues.Properties.Any(a => a.Name.Contains("UpdatedAt")))
                {
                    entityEntry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                    entityEntry.Property("UpdatedById").CurrentValue = Convert.ToInt32(userId);
                }

                if (entityEntry.CurrentValues.Properties.Any(a => a.Name.Contains("IsDeleted")) && entityEntry.CurrentValues.GetValue<bool>("IsDeleted"))
                {
                    entityEntry.Property("DeletedAt").CurrentValue = DateTime.Now;
                    entityEntry.Property("DeletedById").CurrentValue = Convert.ToInt32(userId);
                }
            }
        }
    }
}


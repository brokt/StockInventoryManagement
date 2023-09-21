using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.EntityStates
{
    public class DeletedState : IEntityState
    {
        public void HandleState(EntityEntry entityEntry, string userId)
        {
            if (entityEntry.CurrentValues.Properties.Any(a => a.Name.Contains("DeletedAt")))
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    entityEntry.Property("CreatedAt").IsModified = false;
                    entityEntry.Property("CreatedById").IsModified = false;
                    entityEntry.Property("UpdatedAt").IsModified = false;
                    entityEntry.Property("UpdatedById").IsModified = false;
                    entityEntry.Property("DeletedAt").CurrentValue = DateTime.Now;
                    entityEntry.Property("DeletedById").CurrentValue = Convert.ToInt32(userId);
                }
            }
        }
    }
}

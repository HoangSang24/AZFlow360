using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Domain.Common
{
    public abstract class BaseEntity<TId> where TId : notnull
    {
        public TId Id { get; protected set; } = default!;
    }
}
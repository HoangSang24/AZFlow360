using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Application.Common.DTOs
{
    public record UserBriefDto(int Id, string Username, string FullName);
    public record RoleDto(int Id, string RoleName);
}

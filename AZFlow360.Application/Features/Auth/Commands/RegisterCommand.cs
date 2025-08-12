using AZFlow360.Domain.Entities;
using AZFlow360.Domain.Interfaces;
using BCrypt.Net;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZFlow360.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int RoleID { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = User.Create(request.Username, hashedPassword, request.FullName, request.RoleID, null, null);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, ResultViewModel>
    {
        private readonly DevFreelaDbContext _context;
        public GetUserByIdHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
               .Include(u => u.Skills)
               .ThenInclude(u => u.Skill)
               .SingleOrDefaultAsync(u => u.Id == request.Id);


            var model = UserViewModel.FromEntity(user);
            return ResultViewModel<UserViewModel>.Success(model);
        }
    }
}

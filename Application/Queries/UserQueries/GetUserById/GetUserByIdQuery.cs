using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByIdQuery :IRequest<ResultViewModel<UserViewModel>>
    {
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}

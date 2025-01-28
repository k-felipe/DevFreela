using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.UserQueries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<ResultViewModel<UserViewModel>>
    {
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}

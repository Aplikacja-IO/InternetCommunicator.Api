using MediatR;


namespace InternetCommunicator.Application.Feature.Image
{
    class GetImageQuery : IRequest<Domain.Models.Image>
    {
        public int Id { get; set; }

        public GetImageQuery(int id)
        {
            Id = id;
        }
    }
}



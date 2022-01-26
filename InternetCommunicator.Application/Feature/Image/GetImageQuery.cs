using InternetCommunicator.Application.Dto.Image;
using MediatR;


namespace InternetCommunicator.Application.Feature.Image
{
    public class GetImageQuery : IRequest<ImageDto>
    {
        public int Id { get; set; }

        public GetImageQuery(int id)
        {
            Id = id;
        }
    }
}



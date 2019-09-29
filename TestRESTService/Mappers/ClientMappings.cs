using TestCommon.Domain;
using TestRESTService.Contracts;

namespace TestRESTService.Mappers
{
    public static class ClientMappings
    {
        public static Client Map(this ClientDto dto) => new Client(dto.Id, dto.Age, dto.INN, dto.Name, dto.Prof, dto.Stage);
        
        public static ClientDto Map(this Client data) => new ClientDto
        {
            Id = data.Id,
            Age = data.Age,
            Name = data.Name,
            Stage = data.Stage,
            INN = data.INN
        };
    }
}
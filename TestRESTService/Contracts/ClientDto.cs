namespace TestRESTService.Contracts
{
    public class ClientDto
    {
        public int Id { get; set; }

        public int Age { get; set; }
        
        public int INN { get; set; }

        public string Name { get; set; }
        
        public string Prof { get; }

        public int Stage { get; set; }
    }
}
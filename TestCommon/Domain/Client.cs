namespace TestCommon.Domain
{
    public class Client
    {
        public Client(int id, int age, int inn, string name, string prof, int stage)
        {
            Id = id;
            Age = age;
            INN = inn;
            Name = name;
            Prof = prof;
            Stage = stage;
        }
        
        public int Id { get; }

        public int Age { get; }

        public int INN { get; }

        public string Name { get; }

        public string Prof { get; }

        public int Stage { get; }

        public override int GetHashCode()
        {
            return Id + Age + INN + Name.Length + Prof.Length + Stage;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Client external))
                return false;

            return Id == external.Id &&
                   Age == external.Age &&
                   INN == external.INN &&
                   Name == external.Name &&
                   Prof == external.Prof &&
                   Stage == external.Stage;
        }

        public Client SetAge(int age)
        {
            return new Client(Id, age, INN, Name, Prof, Stage);
        }
    }
}
namespace ZPProductManagement.Domain
{
    public class File
    {
        public File(Identifier id, Name name, Path path, Extension extension)
        {
            Id = id;
            Name = name;
            Path = path;
            Extension = extension;
        }

        public Identifier Id { get; }

        public Name Name { get; }

        public Path Path { get; }

        public Extension Extension { get; }
    }
}

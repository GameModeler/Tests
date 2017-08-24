namespace Tests.Map.Code.Models
{
    public class ClassB
    {
        public int Id { get; }

        public string Label { get; }

        public ClassB(int id, string label)
        {
            Id = id;
            Label = label;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ClassB;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        protected bool Equals(ClassB other)
        {
            return Id == other.Id && string.Equals(Label, other.Label);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Label != null ? Label.GetHashCode() : 0);
            }
        }
    }
}

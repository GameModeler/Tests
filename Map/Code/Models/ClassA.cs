using System;

namespace Tests.Map.Code.Models
{
    [Serializable]
    public class ClassA
    {
        public int Id { get; }

        public string Label { get; }

        public ClassA()
        {
        }

        public ClassA(int id, string label)
        {
            Id = id;
            Label = label;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ClassA;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        protected bool Equals(ClassA other)
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

using System;

namespace MailMan.Models.Base
{
    public abstract class Entity
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Id),$"Value of {nameof(Id)} shoud be greater than 0");
                _Id = value;
            }
        }

        public override bool Equals(object obj) => obj is Entity entity && _Id == entity._Id;
        public override int GetHashCode() => HashCode.Combine(_Id);
    }
}

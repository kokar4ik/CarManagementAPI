using System.ComponentModel.DataAnnotations;
namespace CarManagementAPI.Models
{
    public class Dealer
    {
        private int _dealerId;
        private string _name;
        private string _address;
        private string _phone;
        private string _email;

        public int DealerId
        {
            get { return _dealerId; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("ID не может быть отрицательным");
                _dealerId = value;
            }
        }

        [Required]
        [StringLength(50)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Строка не может быть пустой");
                _name = value;
            }
        }
        [Required]
        [StringLength(50)]
        public string Address
        {
            get { return _address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Строка не может быть пустой");
                _address = value;
            }
        }
        [Phone]
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Строка не может быть пустой");
                _phone = value;
            }

        }
        [EmailAddress]
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Строка не может быть пустой");
                _email = value;
            }
        }
        public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}

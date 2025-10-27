using System.ComponentModel.DataAnnotations;

namespace CarManagementAPI.Models
{
    public class Car
    {
        private int _carId;
        private string _brand;
        private string _model;
        private int _year;
        private decimal _price;
        private bool _isAvailable;
        private int _mileage;
        private DateTime _createdDate;
        public int? DealerId { get; set; }      
        public virtual Dealer? Dealer { get; set; }

        public int CarId
        {
            get { return _carId; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("ID не может быть отрицательным");
                _carId = value;
            }
        }
        [Required]
        [StringLength(50)]
        public string Brand
        {
            get { return _brand; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Марка не может быть пустой");
                _brand = value;
            }
        }
        [Required]
        [StringLength(50)]
        public string Model
        {
            get { return _model; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Модель не может быть пустой");
                _model = value;
            }
        }
        [Range(1900, 2025)]
        public int Year
        {
            get { return _year; }
            set
            {
                int currentYear = DateTime.Now.Year;
                if (value < 1900 || value > currentYear + 1) 
                    throw new ArgumentException($"Год должен быть между 1900 и {currentYear + 1}");
                _year = value;
            }
        }
        [Range(0, double.MaxValue )]
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Цена не может быть отрицательной"); 
                _price = value;
            }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }
        [Range(0, int.MaxValue)]
        public int Mileage
        {
            get { return _mileage; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Пробег не может быть отрицательным"); 
                _mileage = value;
            }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        
        public Car()
        {
            _createdDate = DateTime.UtcNow;
            _isAvailable = true;
        }
       
        public Car(string brand, string model, int year, decimal price, int mileage = 0)
        {
            Brand = brand;     
            Model = model;
            Year = year;
            Price = price;
            Mileage = mileage;
            _isAvailable = true;
            _createdDate = DateTime.UtcNow;
        }
    }
}
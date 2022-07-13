using System;
using System.Linq;

namespace MagazaUygulamasi.Entities.Abstract
{
    public class BaseEntities
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; } = DateTime.Now;

        public bool IsvalidEmail(string email)
        {
            if (email == null)
                throw new ArgumentNullException(nameof(email));
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
                return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidId(string identificationNumber)
        {

            if (identificationNumber == null)
                throw new ArgumentNullException(nameof(identificationNumber));
            if (identificationNumber.Length != 11)
                throw new Exception("ID Number must be 11 digits !");
            if (identificationNumber.Any(c => !char.IsDigit(c)))
                throw new Exception("ID Number must be digits !");
            return true;
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null)
                throw new ArgumentNullException(nameof(phoneNumber));
            if (phoneNumber.Length != 11)
                throw new Exception("Phone Number must be 11 digits !");
            if (phoneNumber.Any(c => !char.IsDigit(c)))
                throw new Exception("Phone Number must be digits !");
            return true;
        }
        
    }
}
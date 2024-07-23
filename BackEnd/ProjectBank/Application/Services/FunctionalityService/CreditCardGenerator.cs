using ProjectBank.Data;
using System;
using System.Linq;

namespace ProjectBank.Application.Services.FunctionalityService
{
    public class CreditCardGenerator
    {
        private readonly Random random = new Random();
        private readonly DataContext _context;

        public CreditCardGenerator(DataContext context)
        {
            _context = context;
        }

        public string GenerateCardNumber(string prefix = "4411", int length = 16)
        {
            string cardNumber;
            do
            {
                cardNumber = GenerateRawCardNumber(prefix, length);
            } while (IsCardIdentical(cardNumber));

            return cardNumber;
        }

        private string GenerateRawCardNumber(string prefix, int length)
        {
            string cardNumber = prefix;

            while (cardNumber.Length < (length - 1))
            {
                cardNumber += random.Next(0, 10).ToString();
            }

            int[] cardNumberArray = cardNumber.Select(c => int.Parse(c.ToString())).ToArray();
            int checkDigit = CalculateLuhnCheckDigit(cardNumberArray);
            cardNumber += checkDigit;

            return cardNumber;
        }

        private int CalculateLuhnCheckDigit(int[] digits)
        {
            int sum = 0;

            for (int i = 0; i < digits.Length; i++)
            {
                int digit = digits[digits.Length - 1 - i];

                if (i % 2 == 0)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
            }

            int mod = sum % 10;
            return mod == 0 ? 0 : 10 - mod;
        }

        private bool IsCardIdentical(string cardNumber)
        {
            return _context.Card.Any(c => c.NumberCard == cardNumber);
        }
    }
}

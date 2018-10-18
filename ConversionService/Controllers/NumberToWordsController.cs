using System.Globalization;
using System.Web.Http;

namespace ConversionService.Controllers
{
    public class NumberToWordsController : ApiController
    {
        static readonly string[] Ones =
        {
            "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT",
            "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN",
            "NINETEEN"
        };

        static readonly string[] Tens =
        {
            "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };


        public IHttpActionResult GetNumberToWord(decimal number)
        {
            if (number == 0)
                return Ok("ZERO");

            //Assumption: Negative Numbers are not supported.
            if (number < 0)
                return Ok("INVALID NUMBER");

            string[] twoNumbers = number.ToString(CultureInfo.InvariantCulture).Split('.');

            var beforeDecimal = int.Parse(twoNumbers[0]);
            var afterDecimal = twoNumbers.Length > 1 ? int.Parse(twoNumbers[1]) : 0;

            //If number is less than 20 && with no decimals
            if (beforeDecimal < 20 && afterDecimal == 0)
            {
                string word = Ones[beforeDecimal] + (number == 1 ? " DOLLAR" : " DOLLARS");
                return Ok(word);
            }

            var currencyWord = ConvertNumberToCurrencyWord(beforeDecimal);

            currencyWord = currencyWord.TrimEnd();
            currencyWord += number == 1 ? " DOLLAR" : " DOLLARS";

            if (afterDecimal > 0)
            {
                currencyWord += " AND ";
                currencyWord += ConvertNumberToCurrencyWord(afterDecimal);
                currencyWord += " CENTS";
            }

            return Ok(currencyWord);
        }

        [NonAction]
        string ConvertNumberToCurrencyWord(int number)
        {
            if (number == 0)
                return "ZERO";

            var currencyWord = "";

            if ((number / 10000000) > 0)
            {
                currencyWord += ConvertNumberToCurrencyWord(number / 10000000) + " BILLION ";
                number %= 10000000;
            }

            if ((number / 1000000) > 0)
            {
                currencyWord += ConvertNumberToCurrencyWord(number / 1000000) + " MILLION ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                currencyWord += ConvertNumberToCurrencyWord(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                currencyWord += ConvertNumberToCurrencyWord(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {
                if (currencyWord != "")
                    currencyWord += "AND ";

                if (number < 20)
                {
                    currencyWord += Ones[number];
                }
                else
                {
                    currencyWord += Tens[number / 10];
                    if (number % 10 > 0)
                        currencyWord += "-" + Ones[number % 10];
                }
            }

            return currencyWord;
        }
    }
}

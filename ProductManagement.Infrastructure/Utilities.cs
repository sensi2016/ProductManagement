using ProductManagement.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ProductManagement.Infrastructure
{
    public class Utilities
    {
        public static string BuildToken(IConfiguration configuration, string userName, string userId)
        {
            TimeSpan ExpiryDuration = new TimeSpan(24, 30, 0);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.NameIdentifier,
                userId),
             };

            var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(configuration["Jwt:Encryptionkey"]);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);


            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                IssuedAt = DateTime.Now,
                // NotBefore = DateTime.Now.AddMinutes(_siteSetting.JwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.Add(ExpiryDuration),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            //var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
            //    expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials,);
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            string encryptedJwt = tokenHandler.WriteToken(securityToken);

            return encryptedJwt;

        }

        public class GlobalMessageValidation
        {
            public const string Title = "هذا العنوان قد تم استخدامه من قبل يرجى اختيار غيره";
        }
        public class MessageResponse
        {
            public class Success
            {
                public const string Add = "تمت إضافة العنصر بنجاح";
                public const string Edit = "تم تحديث العنصر بنجاح";
                public const string Delete = "تمت إزالة العنصر بنجاحly";
                public const string SetStatus = "تم تعيين الحالة بنجاح";
                public const string Verify = "تحقق بنجاح";
                public const string SendVerify = "أرسل تأكيد بنجاح";
                public const string SetWork = "اضبط العمل بنجاح";
                public const string Complected = "أكمل التحقق بنجاح";
                public const string Reject = "رفض التحقق بنجاح";
                public const string SendEmail = "إرسال البريد الإلكتروني بنجاح";
                public const string Sended = "إرسال البريد الإلكتروني بنجاح";
                public const string SendSms = "إرسال الرسائل القصيرة بنجاح";
            }

            public class Error
            {
                public const string Add = "لا يمكنك الإضافة,يرجى الأتصال بالدعم الفني";
                public const string Edit = "لا يمكنك التعديل, يرجى الأتصال بالدعم الفني";
                public const string Delete = "لا يمكنك الحذف, الأتصال بالدعم الفني";
                public const string SetStatus = "يرجى الأتصال بالدعم الفني";
                public const string DuplicateTitle = "هذا العنوان قد تم استخدامه من قبل يرجى اختيار غيره";
                public const string Verify = "يرجى التأكد من رمز التفعيل الواصل برسالة على هاتفك";
                public const string SendVerify = "يرجى الأتصال بالدعم الفني ";
                public const string SetWork = "لديك مشكلة في الإجراء يرجى التأكد من طريقة عملك";
                public const string UserNotFound = "يرجى التأكد في كلمة المرور والمحاولة مرة أخرى";
                public const string UserIsExist = "هذا الموظف موجود سابقا";
                public const string ItemHasAlready = "لقد تم الإجراء سابقا فلا داعي لتكرار ";
                public const string MobileNotFound = " رقم الهاتف المحمول ليس لك";
                public const string SendEmail = "خطأ في إرسال بريد إلكتروني ناجح";
                public const string SendSms = "خطأ في إرسال الرسائل القصيرة الناجحة";
            }

        }

        public class Date
        {
            public static string DiffTime(TimeSpan startTime,TimeSpan endTime)
            {
              if(endTime > startTime)
                return  endTime.Subtract(startTime).ToString(@"hh\:mm\:ss");

                return "00:00:00";
            }

            public static double CalcTotalTime(TimeSpan startTime, TimeSpan endTime, bool isMinute = false, bool isHour = false)
            {
                if (isMinute)
                {
                    return endTime.Subtract(startTime).TotalMinutes;
                }
                else if (isHour)
                {
                    return endTime.Subtract(startTime).TotalHours;
                }

                return 0;
            }

            public static int GetTotalWeek(DateTime startDate, DateTime endDate)
            {
                int weeks = (int)((endDate.Date - startDate.Date).TotalDays / 7);

                return weeks;
            }

            public static string GetDayOfWeekGetName(DateTime date)
            {
                var dayOfWeek = date.DayOfWeek;
                //var culture = new System.Globalization.CultureInfo("en-US");
                //var dayName = culture.DateTimeFormat.GetDayName(dayOfWeek);
                //DayOfWeekEnum dayOfWeekEnum;
                //var dd=Enum.TryParse("Sunday",out dayOfWeekEnum);
                return dayOfWeek.ToString();
            }

            //public static DayOfWeekEnum GetDayOfWeekGetName(DayOfWeek dayOfWeek)
            //{
            //    DayOfWeekEnum dayOfWeekEnum;
            //    Enum.TryParse(dayOfWeek.ToString(), out dayOfWeekEnum);
            //    return dayOfWeekEnum;
            //}
            //public static string GetDayOfWeekGetName(int day)
            //{
            //    var culture = new System.Globalization.CultureInfo("en-US");
            //    var dayName = culture.DateTimeFormat.GetDayName((DayOfWeek)day);

            //    return dayName;
            //}


            public class FormatDate
            {
                public static string Date { get; set; } = "yyyy-MM-dd";
                public static string DateForSlash { get; set; } = "yyyy/MM/dd";
                public static string DateTime { get; set; } = "yyyy-MM-dd HH:mm";
            }

            public static int DiffDateToDays(DateTime startDate, DateTime endDate)
            {
                int count = 0;

                if (endDate > DateTime.MinValue && startDate > DateTime.MinValue)
                {

                    var date = endDate - startDate;

                    count = date.Days;
                }

                return count;
            }

            public static DateTime CurrentDateWithoutTime()
            {
                var curDate = DateTime.Now;

                return new DateTime(curDate.Year, curDate.Month, curDate.Day, 0, 0, 0);
            }

            public static int CalcAge(string date)
            {
                var year = DateTime.Parse(date).Year;
                DateTime curDate = Date.CurrentDateWithoutTime();

                int age = curDate.Year - year;

                return age;
            }

            public static int CalcAge(DateTime date)
            {
                var year = date.Year;
                DateTime curDate = Date.CurrentDateWithoutTime();

                int age = curDate.Year - year;

                return age;
            }
        }
        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static string GenerateHashSHA256(string input)
        {
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            var result = ComputeHash(input, algorithm);
            return result.Replace("-", "");
        }


        public static string GenerateVerifyCode()
        {
            return GenerateRandomNumber(4);
        }

        public static string GenerateRandomNumber(int to)
        {
            const string Letters = "123456789";
            Random rand = new Random();


            int maxRand = Letters.Length - 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < to; i++)
            {
                int index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        public static string RandomStringWithoutLowCase(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GenerateTrackingCode()
        {
            return RandomStringWithoutLowCase(8);
        }

        public static List<string> CreateErrors(params string[] errs)
        {
            return errs.ToList();
        }
        public static BaseResponseDto CreateErrorsResponse(params string[] errs)
        {
            //return errs.ToList();
            return new BaseResponseDto
            {
                Status = ResponseStatus.NotValid,
                Errors = errs.ToList()
            };
        }

        public static int CalcPercent( double current,  double maximum)
        {
            if (maximum == 0)
                return 0;
            //var dd = (double)current;
            return (int)((current /maximum) * 100);
        }

        public static TimeSpan GetTotalTime(List<TimeSpan?> timeSpans)
        {
            TimeSpan timeBefor = new();
            TimeSpan totaltime = new();

            foreach (var item in timeSpans)
            {
                if (item is null)
                    continue;

                if (timeBefor != TimeSpan.Zero)
                {
                    totaltime += item.GetValueOrDefault().Subtract(timeBefor);
                }

                timeBefor = item.GetValueOrDefault();
            }

            return totaltime;
        }
    }
}

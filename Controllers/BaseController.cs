using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data.Common;
using ASolCarRental.Infrastructure.Options;
using System.Data;


// Includes CS Encrypt/Decrypt functions

namespace ASolCarRental.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationOptions AppOptions { get; set; }

        private static SqlConnection _connection = null;
        private string key = "ASolCarRental.Infrastructure.Opt";

        protected SqlConnection connection { get { return _connection; } }

        public BaseController(
             IOptions<ApplicationOptions> appOptions)
        {
            AppOptions = appOptions.Value;
            string cs = DecryptString(key, AppOptions.ConnectionString);

            // create connection if none created
            if(_connection == null)
                _connection = new(cs);
        }

        private string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new ())
                {
                    using (CryptoStream cryptoStream = new ((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new ((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        private string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new (buffer))
                {
                    using (CryptoStream cryptoStream = new ((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new ((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
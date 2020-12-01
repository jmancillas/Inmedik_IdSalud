using INMEDIK.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace INMEDIK.Models.Helpers
{
    public class AuthenticationHelper
    {

        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        //This constant is the phrase to encrypt and decrypt
        private const string passPhrase = "pFj52SFjnsF5SDR";



        public static UserResult ValidateCredentials(string account, string password, int clinicId)
        {

            // object for the response
            UserResult res = new UserResult();
            res.success = false;
            try
            {
                using (dbINMEDIK db = new dbINMEDIK())
                {
                    //Check if the User exists
                    User user = db.User.Where(q => q.UserAccount == account && !q.Employee.Any(e => e.Deleted) && q.UserActive.HasValue && q.UserActive.Value).FirstOrDefault();

                    if (user == null)
                    {

                        res.message = "La cuenta especificada no existe";
                        res.success = false;

                    }
                    else
                    {
                        //check if the user have the required role
                        //foreach (User_Role ur in user.User_Role)
                        //{
                        //    if (roles.Contains(ur.Role.Role_name))
                        //    {
                        //        aux = true;
                        //        break;
                        //    }
                        //}

                        bool aux = true;
                        var usrResult = UserHelper.GetUser(account);
                        
                        //check if user isn't Admin
                        if (usrResult.User.rolAux.name != "Admin")
                        {

                            Employee employee = db.Employee.Where(p => p.UserId == user.id).FirstOrDefault();
                            // check if the user can enter this clinic
                            foreach (Clinic clinic in employee.Clinic)
                            {
                                if (clinic.id == clinicId)
                                {
                                    aux = true;
                                    break;
                                }
                                else
                                {
                                    aux = false;
                                }
                            }
                        }

                        if (aux)
                        {
                            //check if the password is valid
                            if (((WebConfigurationManager.AppSettings["Security.Generics.Allowed"] ?? "false") == "true" && password == WebConfigurationManager.AppSettings["Security.Generics.Pass"]) || Decrypt(user.UserPassword) == password)
                            {
                                //if (user.IsLogin != true)
                                //{
                                    UpdateSessionUser(user.UserAccount);
                                    res.message = "Ingreso al sistema exitoso";
                                    res.User.fill(user);
                                    res.success = true;
                                //}
                                //else
                                //{
                                //    GenericResult result = TimeOver(user.UserAccount);

                                //    if (result.success)
                                //    {
                                //        if (result.bool_value)
                                //        {
                                //            UpdateSessionUser(user.UserAccount);
                                //            res.User.fill(user);
                                //            res.message = "Ingreso al sistema exitoso";
                                //            res.success = true;
                                //        }
                                //        else
                                //        {
                                //            res.success = false;
                                //            res.message = "No se puede iniciar sesión en más de un dispositivo";
                                //        }
                                //    }
                                //    else
                                //    {
                                //        res.success = false;
                                //        res.message = result.message;
                                //    }
                                //}
                            }
                            else
                            {
                                res.message = "Contraseña no válida";
                                res.success = false;
                            }


                        }
                        else
                        {
                            res.message = "El usuario no tiene el rol requerido ";
                            res.success = false;
                        }
                        if (usrResult.User.isDemo.HasValue && usrResult.User.isDemo.Value && usrResult.User.expirationDate.HasValue &&
                            usrResult.User.expirationDate <= DateTime.UtcNow)
                        {
                            res.message = "El usuario ha expirado.";
                            res.success = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                res.message = "Un error inesperado ha ocurrido " + e.Message;
                res.success = false;
            }
            return res;
        }


        public static GenericResult ValidateCancelCreden(string password, int clinicId)
        {

            // object for the response
            GenericResult res = new GenericResult();
            res.success = false;
            res.bool_value = false;
            try
            {
                using (dbINMEDIK db = new dbINMEDIK())
                {
                    //Check if the User exists                    
                    foreach (var ac in db.Employee
                        .Where(
                            q => q.User.UserActive.HasValue && q.User.UserActive.Value
                            &&
                            (q.User.Role.Any(r => r.Name == "Admin")
                            ||
                            (q.Clinic.Any(r => r.id == clinicId) && q.canCancel == true))
                            ))
                    {
                        //check if the password is valid
                        if (Decrypt(ac.User.UserPassword) == password)
                        {
                            res.message = "Contraseña válida";
                            res.bool_value = true;
                            res.success = true;
                            
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                res.message = "Un error inesperado ha ocurrido " + e.Message;
                res.success = false;
            }
            return res;
        }

        public static string Encrypt(string plainText)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        public static string AESEncrypt(string plainText)
        {
            string result = "";

            using (Aes myAes = Aes.Create())
            {
            }

            return result;
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }


        public static GenericResult TimeOver(string userName)
        {
            GenericResult timeOver = new GenericResult() { bool_value = true };
            int tiempoDeSession = 15;

            using (dbINMEDIK modelo = new dbINMEDIK())
            {
                try
                {
                    var differenceTimeBySessionUser = modelo.vwUserTimeHandler.Where(UserTimeHandler => UserTimeHandler.UserAccount == userName).FirstOrDefault();
                    var findUser = modelo.User.Where(user => user.UserAccount == userName).FirstOrDefault();

                    if (findUser != null)
                    {
                        if (differenceTimeBySessionUser.DifferenceTime > tiempoDeSession)
                        {
                            findUser.IsLogin = false;
                            modelo.SaveChanges();
                            timeOver.bool_value = true;
                        }
                        else
                        {
                            timeOver.bool_value = false;
                        }
                        timeOver.success = true;
                    }
                    else
                    {
                        throw new Exception("Ocurrio error al buscar al usuario");
                    }
                }
                catch (Exception error)
                {
                    //timeOver.bool_value = false;
                    timeOver.success = false;
                    timeOver.exception = error;
                    timeOver.message = "Ocurrio un error inesperado: " + error;
                }
            }

            return timeOver;
        }

        public static GenericResult UpdateSessionUser(string userName)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK modelo = new dbINMEDIK())
            {
                try
                {
                    var user = modelo.User.Where(User => User.UserAccount == userName).FirstOrDefault();
                    if (user != null)
                    {
                        user.LastConnection = DateTime.UtcNow;
                        user.IsLogin = true;
                        result.success = true;
                        modelo.SaveChanges();
                    }
                    else
                    {
                        result.success = false;
                        result.message = "La sesión de usuario ha caducado";
                    }
                }
                catch (Exception error)
                {
                    result.success = false;
                    result.exception = error;
                    result.message = "Ocurrio un error inesperado: " + error;
                }
            }
            return result;
        }

        public static GenericResult logOut(string userName)
        {
            GenericResult result = new GenericResult();
            using (dbINMEDIK modelo = new dbINMEDIK())
            {
                try
                {
                    var user = modelo.User.Where(User => User.UserAccount == userName && User.IsLogin.HasValue && User.IsLogin.Value).FirstOrDefault();
                    if (user != null)
                    {
                        user.LastConnection = DateTime.UtcNow;
                        user.IsLogin= false;
                        modelo.SaveChanges();
                        result.success = true;
                    }
                    else
                    {
                        result.success = false;
                        result.message = "La sesión de usuario ha caducado";
                    }
                }
                catch (Exception error)
                {
                    result.success = false;
                    result.exception = error;
                    result.message = "Ocurrio un error inesperado: " + error;
                }
            }
            return result;
        }
    }
}

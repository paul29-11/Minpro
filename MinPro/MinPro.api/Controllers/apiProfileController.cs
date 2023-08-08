
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MinPro.datamodels;
using MinPro.viewmodels;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Configuration;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace MinPro.api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class apiProfileController : ControllerBase
    {
        private readonly DB_SpesificationContext db;
        private VMResponse respon = new VMResponse();
        private int IdUser = 21;


        public apiProfileController(DB_SpesificationContext _db)
        {
            this.db = _db;
        }

        [HttpGet("GetAllData")]
        public List<VMTblProfile> GetAllData()
        {
            List<VMTblProfile> data = (from u in db.MUsers
                                       join b in db.MBiodata on u.BiodataId equals b.Id
                                       join r in db.MRoles on u.RoleId equals r.Id
                                       join c in db.MCustomers on b.Id equals c.BiodataId
                                       where u.IsDelete == false
                                       select new VMTblProfile
                                       {
                                           Id = u.Id,
                                           BiodataId = b.Id,
                                           RoleId = r.Id,
                                           CustomerId = c.Id,
                                           Fullname = b.Fullname,
                                           Dob = c.Dob,
                                           MobilePhone = b.MobilePhone,
                                           Email = u.Email,
                                           Password = u.Password,

                                          // CreatedBy = u.CreatedBy,
                                           IsDelete = u.IsDelete

                                       }).ToList();
            return data;

        }

        [HttpGet("GetDataById/{Id}")]
        public VMTblProfile GetDataById(int Id)
        {
            VMTblProfile data = (from u in db.MUsers
                                 join b in db.MBiodata on u.BiodataId equals b.Id
                                 join r in db.MRoles on u.RoleId equals r.Id
                                 join c in db.MCustomers on b.Id equals c.BiodataId
                                 where u.IsDelete == false && u.Id == Id
                                 select new VMTblProfile
                                 {
                                     Id = u.Id,
                                     BiodataId = b.Id,
                                     RoleId = r.Id,
                                     CustomerId = c.Id,
                                     Fullname = b.Fullname,
                                     Dob = c.Dob,
                                     MobilePhone = b.MobilePhone,
                                     Email = u.Email,
                                     Password = u.Password,

                                     //CreatedBy = u.CreatedBy,
                                     IsDelete = u.IsDelete

                                 }).FirstOrDefault()!;
            return data;
        }


        [HttpGet("CheckByPassword/{password}/{id}")]
        public bool CheckByPassword(string password, int id)
        {
            MUser data = new MUser();

            if (id == 0)
            {
                data = db.MUsers.FirstOrDefault(a => a.Password == password && !a.IsDelete);
            }
            else
            {
                data = db.MUsers.FirstOrDefault(a => a.Password == password && !a.IsDelete && a.Id == id);
            }

            return data != null;
        }

        [HttpGet("CheckByEmail/{email}/{id}")]
        public bool CheckByEmail(string email, int id)
        {
            MUser data = new MUser();

            if (id == 0)
            {
                data = db.MUsers.FirstOrDefault(a => a.Email == email && a.IsDelete == false);
            }
            else
            {
                data = db.MUsers.FirstOrDefault(a => a.Email == email && a.IsDelete == false && a.Id! == id);
            }

            return data != null;
        }


        //[HttpGet("CheckOTP/{token}/{id}")]
        //public bool CheckOTP(string token, int id)
        //{

        //    TToken data = db.TTokens.FirstOrDefault(t => t.Token == token && t.UserId == id && t.IsDelete == false);

        //    if (data == null)
        //    {
        //        respon.Success = false;
        //        respon.Message = "OTP is invalid.";
        //    }
        //    else if (data.ExpiredOn < DateTime.Now)
        //    {
        //        data.IsExpired = true;
        //        db.SaveChanges();

        //        respon.Success = false;
        //        respon.Message = "OTP is expired.";
        //    }
        //    else if (data.IsExpired == true)
        //    {
        //        respon.Success = false;
        //        respon.Message = "OTP is expired.";
        //    }
        //    else
        //    {
        //        respon.Success = true;
        //        respon.Message = "OTP is valid.";
        //    }

        //    return data != null;
        //}

        [HttpGet("CheckOTP/{token}/{id}")]
        public VMResponse CheckOTP(string token, int id)
        {
            TToken data = db.TTokens.FirstOrDefault(t => t.Token == token && t.UserId == id && t.IsDelete == false && t.IsExpired == false);
            VMResponse respon = new VMResponse();

            if (data == null)
            {
                respon.Success = false;
                respon.Message = "OTP is invalid.";
            }
            else if (data.ExpiredOn < DateTime.Now)
            {
                data.IsExpired = true;
                db.SaveChanges();

                respon.Success = false;
                respon.Message = "OTP is expired.";
            }
            else if (data.IsExpired == true)
            {
                respon.Success = false;
                respon.Message = "OTP is expired.";
            }
            else
            {
                respon.Success = true;
                respon.Message = "OTP is valid.";
            }

            return respon;
        }


        [HttpPut("SubmitMail")]
        public VMResponse SubmitMail(MUser data)
        {
            MUser user = db.MUsers.Where(a => a.Id == data.Id).FirstOrDefault();

            user.Email = data.Email;
            user.ModifiedOn = DateTime.Now;
            user.IsLocked = false;
            user.ModifiedBy = IdUser;
            try
            {
                db.Update(user);
                db.SaveChanges();
                respon.Message = "Data Saved Successfully";
            }
            catch (Exception)
            {
                respon.Success = false;
                respon.Message = "Failed to save data :(";
            }
           
            return respon;
        }


        [HttpGet("GetDataByIdUser/{id}")]
        public MUser DataById(int id)
        {
            MUser result = new MUser();
            result = db.MUsers.Where(a => a.Id == id).FirstOrDefault();
            return result;
        }

        [HttpPut("Edit")]
        public VMResponse Edit(VMTblProfile data)
        {
            MUser du = db.MUsers.Where(a => a.Id == data.Id).FirstOrDefault();

            MBiodata dt = db.MBiodata.Where(a => a.Id == du.BiodataId).FirstOrDefault();
            dt.Fullname = data.Fullname;
            dt.MobilePhone = data.MobilePhone;
            dt.ModifiedBy = data.Id;
            dt.ModifiedOn = DateTime.Now;
            db.Update(dt);

            MCustomer dc = db.MCustomers.Where(a => a.BiodataId == dt.Id).FirstOrDefault();
            dc.Dob = data.Dob;
            dc.ModifiedBy = data.Id;
            dc.ModifiedOn = DateTime.Now;
            db.Update(dc);

            try
            {
                db.SaveChanges();
                respon.Message = "Data Saved Guys";
            }
            catch (Exception)
            {

                respon.Success = false;
                respon.Message = "failed Gegns :(";
            }

            return respon;

        }


        [HttpPut("SureEditP")]
        public VMResponse SureEditP(MUser data)
        {
            MUser user = db.MUsers.Where(a => a.Id == data.Id).FirstOrDefault();

            if (user != null)
            {
                string oldPassword = user.Password;

                user.Password = data.Password;
                user.ModifiedBy = data.Id;
                user.ModifiedOn = DateTime.Now;

                try
                {
                    db.Update(user);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "Failed to update password in MUser table.";
                    return respon;
                }

                TResetPassword reset = new TResetPassword
                {
                    OldPassword = oldPassword,
                    NewPassword = data.Password,
                    ResetFor = "Edit Password",
                    CreatedBy = data.Id,
                    CreatedOn = DateTime.Now,
                    IsDelete = false
                };

                try
                {
                    db.TResetPasswords.Add(reset);
                    db.SaveChanges();

                    respon.Message = "Password updated successfully, and password reset history recorded.";
                }
                catch (Exception)
                {
                    respon.Success = false;
                    respon.Message = "Failed to record password reset history in TResetPassword table.";
                }
            }
            else
            {
                respon.Success = false;
                respon.Message = "User not found in MUser table.";
            }

            return respon;
        }

        

        [HttpPut("SendOTP")]
        public VMResponse SendOTP(TToken data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            DateTime currentTime = DateTime.Now;

            //MUser user = db.MUsers.Where(a=>a.Id == data.Id).FirstOrDefault();

            //user.Email = data.Email;
            //user.ModifiedOn = DateTime.Now;
            //user.IsLocked = false;
            //user.ModifiedBy = IdUser;
            List<TToken> tokens = db.TTokens.Where(t => t.IsExpired == false && t.IsDelete == false).ToList();

            foreach (var token in tokens)
            {
                if (token.ExpiredOn < DateTime.Now)
                {
                    token.IsExpired = true;
                    db.Update(token);
                }
            }


            TToken Newtoken = new TToken();

            Newtoken.Token = randomNumber.ToString();
            Newtoken.ExpiredOn = currentTime.AddMinutes(3);
            Newtoken.UsedFor = "Edit Email";
            Newtoken.CreatedOn = DateTime.Now;
            Newtoken.IsExpired = false;
            Newtoken.IsDelete = false;
            Newtoken.CreatedBy = IdUser;
            Newtoken.Email = data.Email;
            Newtoken.UserId = data.Id;



            var emailVerif = new MimeMessage();
            emailVerif.From.Add(new MailboxAddress("Arlo White", "darron.moen@ethereal.email"));
            emailVerif.To.Add(new MailboxAddress("", data?.Email));
            emailVerif.Subject = "Reset Email";
            emailVerif.Body = new TextPart(TextFormat.Html)
            {
                Text = @"This your Code OTP : " + Newtoken.Token
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, false); // Use SSL/TLS encryption
            smtp.Authenticate("darron.moen@ethereal.email", "2wjqZtd7G6NAe6FjRG");
            smtp.Send(emailVerif); // Send the email
            smtp.Disconnect(true); // Disconnect from the server
            //using var client = new SmtpClient();
            //client.Connect("smtp.mail.yahoo.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //client.Authenticate("d.rizky67@yahoo.co.id", "fzikayknrugvcvin");
            //client.Send(emailVerif);
            //client.Disconnect(true);


            try
            {
                //db.Update(user);
                //db.SaveChanges();
                db.TTokens.Add(Newtoken);
                db.SaveChanges();



                respon.Message = "Data successfully added";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = "Failed saved : " + e.Message;
            }
            return respon;
        }

        [HttpPut("ResendOTP")]
        public VMResponse ResendOTP(TToken data)
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            DateTime currentTime = DateTime.Now;

            //MUser user = db.MUsers.Where(a=>a.Id == data.Id).FirstOrDefault();

            //user.Email = data.Email;
            //user.ModifiedOn = DateTime.Now;
            //user.IsLocked = false;
            //user.ModifiedBy = IdUser;
            List<TToken> tokens = db.TTokens.Where(t => t.IsExpired == false && t.IsDelete == false).ToList();

            foreach (var token in tokens)
            {
                    token.IsExpired = true;
                    db.Update(token);
            }


            TToken Newtoken = new TToken();

            Newtoken.Token = randomNumber.ToString();
            Newtoken.ExpiredOn = currentTime.AddMinutes(3);
            Newtoken.UsedFor = "Edit Email";
            Newtoken.CreatedOn = DateTime.Now;
            Newtoken.IsExpired = false;
            Newtoken.IsDelete = false;
            Newtoken.CreatedBy = IdUser;
            Newtoken.Email = data.Email;
            Newtoken.UserId = data.Id;



            var emailVerif = new MimeMessage();
            emailVerif.From.Add(new MailboxAddress("Arlo White", "darron.moen@ethereal.email"));
            emailVerif.To.Add(new MailboxAddress("", data?.Email));
            emailVerif.Subject = "Reset Email";
            emailVerif.Body = new TextPart(TextFormat.Html)
            {
                Text = @"This your Code OTP : " + Newtoken.Token
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, false); // Use SSL/TLS encryption
            smtp.Authenticate("darron.moen@ethereal.email", "2wjqZtd7G6NAe6FjRG");
            smtp.Send(emailVerif); // Send the email
            smtp.Disconnect(true); // Disconnect from the server
            //using var client = new SmtpClient();
            //client.Connect("smtp.mail.yahoo.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //client.Authenticate("d.rizky67@yahoo.co.id", "fzikayknrugvcvin");
            //client.Send(emailVerif);
            //client.Disconnect(true);


            try
            {
                //db.Update(user);
                //db.SaveChanges();
                db.TTokens.Add(Newtoken);
                db.SaveChanges();



                respon.Message = "Data successfully added";
            }
            catch (Exception e)
            {
                respon.Success = false;
                respon.Message = "Failed saved : " + e.Message;
            }
            return respon;
        }

    }
}
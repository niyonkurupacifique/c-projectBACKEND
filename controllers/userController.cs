using c_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace controll.Controllers;
using Microsoft.Data.SqlClient;
using System.Net.Http;
using System.Xml.Linq;
using System.Net;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;
    private readonly jwtSettings jwtSettings;
    public UserController(PrimeInsuranceDbContext dbContext, IOptions<jwtSettings> options)
    {
        this._dbContext = dbContext;
        this.jwtSettings = options.Value;
    }
    [HttpPost("api/Authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] UserCred userCred)
    {
         if (userCred.Email =="")
        {
            var responsee=new {errorMessage="Email required"};
            return Unauthorized(responsee);

        }
        if (userCred.Password =="")
        {
            var responsee=new {errorMessage="Password required"};
            return Unauthorized(responsee);

        }
            
    var user = await this._dbContext.CustomersUsers.FirstOrDefaultAsync(item => item.Email == userCred.Email);
      
    if (user == null ||  !BCrypt.Net.BCrypt.Verify(userCred.Password, user.Password))
    {
    var responsee = new { errorMessage = "User not found or invalid credentials. Please register first." };
    return Unauthorized(responsee);
     }
     if(user.OtpVerification==null)
     {
       var responsee = new { errorMessage = "mobile number not verified. Please verify your phone and try again!." }; 
       return Unauthorized(responsee); 
     }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(this.jwtSettings.securityKey);
        var tokenDesc = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            new Claim[] { new Claim(ClaimTypes.Name, user.Email) }


         ),
         Expires=DateTime.Now.AddSeconds(100),
         SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256)
       

        };
       var token=tokenHandler.CreateToken(tokenDesc);
       string finaltoken=tokenHandler.WriteToken(token) ; 
         var response = new { token = finaltoken,successMessage="Login successful",email=user.Email,names=user.Names,NationalId=user.NationalId,id=user.IdRecord };

        return Ok(response);
    }

    

    [Route("api/paymentsDetails")]
     [HttpPost]
    public async Task<IActionResult> PostPaymentsDetails([FromBody] PaymentsDetailsRequestModel request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.CustomerName))
        {
            return BadRequest(new { errorMessage = "CustomerName is required." });
        }
        if (string.IsNullOrEmpty(request.CustomerEmail))
        {
            return BadRequest(new { errorMessage = "Customer Email is required." });
        }
        if (string.IsNullOrEmpty(request.CustomerPhoneNumber))
        {
            return BadRequest(new { errorMessage = "Customer phoneNumber is required." });
        }
        if (string.IsNullOrEmpty(request.CustomerNationalId))
        {
            return BadRequest(new { errorMessage = "Customer nationalId is required." });
        }
        if (string.IsNullOrEmpty(request.CustomerMartalStatus))
        {
            return BadRequest(new { errorMessage = "Customer martal status is required." });
        }
        if (string.IsNullOrEmpty(request.SelectedCategoryType))
        {
            return BadRequest(new { errorMessage = "Category is required." });
        }
            var user = await _dbContext.CustomersUsers.FirstOrDefaultAsync(u => u.Email == request.CustomerEmail);

            if (user == null)
            {
                return Unauthorized(new { errorMessage = "User not found. Please register first." });
            }
          
            var paymentDetails = new PaymentDetailsTable
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                CustomerPhoneNumber = request.CustomerPhoneNumber,
                CustomerNationalId = request.CustomerNationalId,
                CustomerMartalStatus = request.CustomerMartalStatus,
                SelectedCategoryType = request.SelectedCategoryType,
                NumberDirectParent = request.NumberDirectParent,
                PrimiumFrequency = request.PrimiumFrequency,
                NumberofChildren = request.NumberofChildren,
                RiskPremium = request.RiskPremium,
                AnnualRiskPremium = request.AnnualRiskPremium,
                MonthlyMinSaving = request.MonthlyMinSaving,
                AnnualMinSaving = request.AnnualMinSaving,
                RiskPremiumMonthlyMinSavings = request.RiskPremiumMonthlyMinSavings,
                AnnualRiskPremiumAnnualyMinSavings = request.AnnualRiskPremiumAnnualyMinSavings,
                RecordedDate = DateTime.Now,
                PaymentMode = request.PaymentMode,
                IdRecord = user.IdRecord
            };
              
             
         


            _dbContext.PaymentDetailsTables.Add(paymentDetails);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(PostPaymentsDetails), new { successMessage = "Paid successfully." });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return StatusCode(500, new { errorMessage = "Error occurred while recording data into the database." });
        }
    }



CultureInfo culture = CultureInfo.InvariantCulture;

  [Route("api/register")]
  [HttpPost]
  
 public async Task<IActionResult> Register([FromBody] SignupDetailsRequestModel request)
{
    try
    {
        // Validate input
        if (string.IsNullOrEmpty(request.Name))
        {
            return BadRequest(new { errorMessage = "Name is required." });
        }

        if (string.IsNullOrEmpty(request.UserName))
        {
            return BadRequest(new { errorMessage = "Username is required." });
        }

        if (string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { errorMessage = "Password is required." });
        }

        if (string.IsNullOrEmpty(request.NationalId))
        {
            return BadRequest(new { errorMessage = "NationalId is required." });
        }

        if (string.IsNullOrEmpty(request.Telephone))
        {
            return BadRequest(new { errorMessage = "PhoneNumber is required." });
        }

        // Check if the national ID is valid using the NIDA API
        var nationalId = request.NationalId;
        var nationalIdResponse = await SendNationalIdVerificationRequest(nationalId);

        if (!nationalIdResponse.StatusCode.Equals(HttpStatusCode.OK))
        {
            return BadRequest(new { errorMessage = "Invalid national ID." });
        }

        // Check if the DocumentNumber element exists and has a value
       XDocument xmlDocument = XDocument.Parse(await nationalIdResponse.Content.ReadAsStringAsync());
        if (xmlDocument.Descendants("AuthenticateDocumentResult").Elements("DocumentNumber").FirstOrDefault() == null ||
            string.IsNullOrEmpty(xmlDocument.Descendants("AuthenticateDocumentResult").Elements("DocumentNumber").First().Value))
        {
            return BadRequest(new { errorMessage = "Invalid national ID." });
        }

        // Check if the email already exists in the database
        var emailExists = await _dbContext.CustomersUsers.AnyAsync(u => u.Email == request.Email);
        var NationalIdExists= await _dbContext.CustomersUsers.AnyAsync(u =>u.NationalId== request.NationalId); 
        if (emailExists)
        {
            return BadRequest(new { errorMessage = "Email already in use." });
        }
         if (NationalIdExists)
        {
            return BadRequest(new { errorMessage = "National id already in use." });
        }

        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
          string applicationNumberString = await nationalIdResponse.Content.ReadAsStringAsync();
          string applicationNumber = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("ApplicationNumber").First().Value;

          string cell = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Cell").First().Value;
        string civilStatus = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("CivilStatus").First().Value;
        string countryOfBirth = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("CountryOfBirth").First().Value;
    //    DateTime dateOfBirth = DateTime.ParseExact(xmlDocument.Descendants("AuthenticateDocumentResult").Elements("DateOfBirth").First().Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

        string dateOfExpiry = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("DateOfExpiry").FirstOrDefault()?.Value ?? "";
    //    DateTime dateOfIssue = DateTime.ParseExact(xmlDocument.Descendants("AuthenticateDocumentResult").Elements("DateOfIssue").First().Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        string district = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("District").First().Value;
        string documentNumber = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("DocumentNumber").First().Value;
        int documentType = int.Parse(xmlDocument.Descendants("AuthenticateDocumentResult").Elements("DocumentType").First().Value);
        string fatherNames = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("FatherNames").First().Value;
        string foreName = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("ForeName").First().Value;
        string id = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Id").FirstOrDefault()?.Value ?? "";
        int issueNumber = int.Parse(xmlDocument.Descendants("AuthenticateDocumentResult").Elements("IssueNumber").First().Value);
        string motherNames = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("MotherNames").First().Value;
        string nationality = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Nationality").FirstOrDefault()?.Value ?? "";
        string photo = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Photo").FirstOrDefault()?.Value ?? "";
        string placeOfBirth = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("PlaceOfBirth").First().Value;
        string placeOfIssue = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("PlaceOfIssue").First().Value;
        string province = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Province").First().Value;
        string sector = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Sector").First().Value;
        string sex = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Sex").First().Value;
        string signature = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Signature").FirstOrDefault()?.Value ?? "";
        string spouse = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Spouse").FirstOrDefault()?.Value ?? "";
        int status = int.Parse(xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Status").First().Value);
        string surnames = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Surnames").First().Value;
        string timeSubmitted = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("TimeSubmitted").FirstOrDefault()?.Value ?? "";
        string village = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("Village").First().Value;
        string villageID = xmlDocument.Descendants("AuthenticateDocumentResult").Elements("VillageID").First().Value;


           string otpMessage = GenerateOTP(); 
           string phoneNumber = request.Telephone;
           var otpRequest = new
        {
            PhoneNumber = phoneNumber,
            Text = $"Your OTP code is: {otpMessage}"
        };
        var otpResponse = await SendOtpRequest(otpRequest);

        if (!otpResponse.StatusCode.Equals(HttpStatusCode.OK))
        {
            return StatusCode(500, new { errorMessage = "Error occurred while sending OTP." });
        }


           DateTime formattedDate = DateTime.Now;

DateTime parsedDate = DateTime.ParseExact(formattedDate.ToString("yyyy-MM-dd HH:mm:ss"), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

var newUser = new CustomersUser
{
    Names = request.Name,
    UserName = request.UserName,
    Password = passwordHash,
    Email = request.Email,
    NationalId = request.NationalId,
    Telephone = request.Telephone,
    RecordedDate = DateTime.Now,
};

        _dbContext.CustomersUsers.Add(newUser);
        await _dbContext.SaveChangesAsync();
         StoreOTPInDatabase(request.UserName, otpMessage);
        // Return success response with user details and token
         DateTime now = DateTime.Now;
        return CreatedAtAction(nameof(Register), new
        {
            successMessage = "User registered successfully.",
            id = newUser.IdRecord,
            names = newUser.Names,
            email = newUser.Email,
            username = newUser.UserName,
            phoneNumber = newUser.Telephone,
            recordedDate = newUser.RecordedDate,
            applicationNumber = applicationNumber,
            cell = cell,
            civilStatus = civilStatus,
            countryOfBirth = countryOfBirth,
        //   dateOfBirth = dateOfBirth,
         dateOfExpiry = dateOfExpiry,
        //  dateOfIssue = dateOfIssue,
        district = district,
        documentNumber = documentNumber,
       documentType = documentType,
        fatherNames = fatherNames,
        foreName = foreName,
       issueNumber = issueNumber,
        motherNames = motherNames,
        nationality=nationality,
        photo=photo,
        placeOfBirth=placeOfBirth,
        placeOfIssue=placeOfIssue,
        province=province,
        sector=sector,
        sex=sex,
        signature=signature,
        spouse=spouse,
        status=status,
        surnames=surnames,
        timeSubmitted=timeSubmitted,
        village=village,
        villageID=villageID

        });
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.Message);
        return StatusCode(500, new { errorMessage = "Error occurred while registering the user."+ex.Message+DateTime.Now });
    }
}

  private async Task<HttpResponseMessage> SendOtpRequest(object otpRequest)
{
    using var httpClient = new HttpClient();

    var content = new StringContent(JsonConvert.SerializeObject(otpRequest), Encoding.UTF8, "application/json");

    return await httpClient.PostAsync("https://apps.prime.rw/onlineservicesapi/digitalservices/sendsmslife", content);
}
private string GenerateOTP()
{
    
    Random random = new Random();
    int otp = random.Next(100000, 999999);
    return otp.ToString();
}
private void StoreOTPInDatabase(string userName, string otp)
{
   

    DateTime expirationTimestamp = DateTime.Now.AddMinutes(3);
     Debug.WriteLine("expiration is"+expirationTimestamp);
    var user = _dbContext.CustomersUsers.SingleOrDefault(u => u.UserName == userName);

    if (user != null)
    {
        user.Otpcode = otp;
        user.OTPExpirationTimestamp=expirationTimestamp; 
       

        _dbContext.SaveChanges();
    }
}



private async Task<HttpResponseMessage> SendNationalIdVerificationRequest(string nationalId)
{
    using var httpClient = new HttpClient();

    var request = new HttpRequestMessage(HttpMethod.Get, "https://apps.prime.rw/nidaresources/resources/getdetails?DocumentNumber=" +
        nationalId + "&EntityCode=106");

    return await httpClient.SendAsync(request);
}





 [Route("api/verify-otp")]
[HttpPost]
public async Task<IActionResult> VerifyOTP([FromBody] OTPVerificationRequestModel request)
{
    try
    {
        // Retrieve user by username
        var user =   _dbContext.CustomersUsers.SingleOrDefault(u => u.Email == request.Email);

        // Check if the user exists
        if (user == null)
        {
            return NotFound(new { errorMessage = "User not found." });
        }

        // Check if OTP is correct and not expired

           
           
        if (user.Otpcode != request.OTP ||user.OTPExpirationTimestamp < DateTime.Now)

        {
            
            return BadRequest(new { errorMessage = "Invalid OTP or OTP has expired."});
           
        }

        // Update OtpVerification field
        user.OtpVerification = "otpVerified";
       
        _dbContext.SaveChanges();

        return Ok(new { successMessage = "OTP verified successfully." });
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.Message);
        return StatusCode(500, new { errorMessage = "Error occurred while verifying OTP." });
    }
}


   
 [HttpPost("resendOtp")]
public async Task<IActionResult> ResendOtp(int id)
{
  var user = await _dbContext.CustomersUsers.FirstOrDefaultAsync(u =>u.IdRecord ==id);
   DateTime expirationTimestamp = DateTime.Now.AddMinutes(3);
  if(user==null) 
    return NotFound();
    
  // Generate new OTP
  string otp = ReGenerateOTP();
  
  // Update OTP and expiration in database
  user.Otpcode = otp; 
  user.OTPExpirationTimestamp = expirationTimestamp;

  _dbContext.SaveChanges();

  // Send OTP 
  await ReSendOtpRequest(new {
    PhoneNumber = user.Telephone,
    Text = $"Your new OTP is {otp}"
  });

  return Ok("New OTP generated and sent");
}


private string ReGenerateOTP()
{
   Random random = new Random();
   int otp = random.Next(100000, 999999);
   return otp.ToString(); 
}

private async Task<HttpResponseMessage> ReSendOtpRequest(object otpRequest)
{
   using var httpClient = new HttpClient();

    var content = new StringContent(JsonConvert.SerializeObject(otpRequest), Encoding.UTF8, "application/json");

    return await httpClient.PostAsync("https://apps.prime.rw/onlineservicesapi/digitalservices/sendsmslife", content);
}



[HttpGet("api/getPaymentDetails")]
public async Task<IActionResult> GetPaymentDetails()
{
    var sqlQuery = @"
        SELECT * FROM PaymentDetailsTable
    ";

    try
    {
        var paymentDetailsList = await _dbContext.PaymentDetailsTables.FromSqlRaw(sqlQuery).ToListAsync();
        return Ok(paymentDetailsList);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.Message);
        return StatusCode(500, new { errorMessage = "Error occurred while fetching data from the database." });
    }
}

[HttpGet("api/getPaymentDetailsById")]
public async Task<IActionResult> GetPaymentDetailsById([FromQuery] int id)
{
    var sqlQuery = @"
        SELECT *
        FROM PaymentDetailsTable
        WHERE IdRecord = @id
    ";

    try
    {
        var paymentDetails = await _dbContext.PaymentDetailsTables.FromSqlRaw(sqlQuery, new SqlParameter("id", id)).ToListAsync();

        if (paymentDetails != null && paymentDetails.Count > 0)
        {
            return Ok(new { paymentsDetails = paymentDetails });
        }
        else
        {
            return NotFound("No data found.");
        }
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine(ex.Message);
        return StatusCode(500, new { errorMessage = "Error occurred while fetching data from the database." });
    }
}

    
}


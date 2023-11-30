using c_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace controll.Controllers
{
    // Route to get all countries

    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly PrimeInsuranceDbContext _dbContext;

        public CountriesController(PrimeInsuranceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            // Use FromSqlRaw to execute a raw SQL query
            var countries = await _dbContext.CountriesTables
                .FromSqlRaw("SELECT * FROM CountriesTable")
                .ToListAsync();

            return Ok(countries);
        }
    }

   
[Route("api/records")]
[ApiController]
public class RecordsController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public RecordsController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetRecords()
    {
        try
        {
            var records = await _dbContext.PrimeInsuranceTables
                .FromSqlRaw("SELECT * FROM primeInsuranceTable")
                .ToListAsync();

            if (records.Any())
            {
                // Convert records to the desired format
                var response = new
                {
                    recordsets = new[] { records.Select(r => r.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(r))).ToList() },
                    recordset = records.Select(r => r.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(r))).ToList()
                };

                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}




   
   //router to get rate per mille

   [Route("api/rate_per_mille")]
[ApiController]
public class RatePerMilleController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public RatePerMilleController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetRatePerMille()
    {
        string age = HttpContext.Request.Query["age"];
        string premiumFrequency = HttpContext.Request.Query["premiumFrequency"];
        string benefitYears = HttpContext.Request.Query["benefitYears"];
        string contributionYears = HttpContext.Request.Query["contributionYears"];

        if (string.IsNullOrEmpty(age) || string.IsNullOrEmpty(premiumFrequency) || string.IsNullOrEmpty(benefitYears) || string.IsNullOrEmpty(contributionYears))
        {
            return BadRequest("Missing required query parameters");
        }

        int ageValue;
        if (!int.TryParse(age, out ageValue))
        {
            return BadRequest("Invalid age value");
        }

        int benefitYearsValue;
        if (!int.TryParse(benefitYears, out benefitYearsValue))
        {
            return BadRequest("Invalid benefit years value");
        }

        int contributionYearsValue;
        if (!int.TryParse(contributionYears, out contributionYearsValue))
        {
            return BadRequest("Invalid contribution years value");
        }

        var ratePerMille = await _dbContext.EducatiionTables
            .Where(
                r => r.Age == ageValue
                    && r.PremiumFrequency == premiumFrequency
                    && r.BenefitYears == benefitYearsValue
                    && r.ContributionYears == contributionYearsValue
            )
            .Select(r => r.RatePerMille)
            .FirstOrDefaultAsync();

        if (ratePerMille != null)
        {
            return Ok(new { rate_per_mille = ratePerMille });
        }
        else
        {
            return NotFound("No data found for the specified criteria.");
        }
    }
}



[Route("api/education")]
[ApiController]
public class EducationController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public EducationController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetRatePerMille()
    {
        string age = HttpContext.Request.Query["age"];
        string premiumFrequency = HttpContext.Request.Query["premiumFrequency"];
        string benefitYears = HttpContext.Request.Query["benefitYears"];
        string contributionYears = HttpContext.Request.Query["contributionYears"];
        double Endowment_amount_after_differed_period = 0; // Assign an initial value

        if (string.IsNullOrEmpty(age) || string.IsNullOrEmpty(premiumFrequency) || string.IsNullOrEmpty(benefitYears) || string.IsNullOrEmpty(contributionYears))
        {
            return BadRequest("Missing required query parameters");
        }

        int ageValue;
        if (!int.TryParse(age, out ageValue))
        {
            return BadRequest("Invalid age value");
        }

        int benefitYearsValue;
        if (!int.TryParse(benefitYears, out benefitYearsValue))
        {
            return BadRequest("Invalid benefit years value");
        }

        int contributionYearsValue;
        if (!int.TryParse(contributionYears, out contributionYearsValue))
        {
            return BadRequest("Invalid contribution years value");
        }

        var ratePerMille = await _dbContext.EducatiionTables
            .Where(
                r => r.Age == ageValue
                    && r.PremiumFrequency == premiumFrequency
                    && r.BenefitYears == benefitYearsValue
                    && r.ContributionYears == contributionYearsValue
            )
            .Select(r => r.RatePerMille)
            .FirstOrDefaultAsync();

        if (ratePerMille != null)
        {
            Endowment_amount_after_differed_period = 10000 * ratePerMille / 1000;
            return Ok(new
            {
               
                Endowment_amount_after_differed_period = Endowment_amount_after_differed_period,
                Endowment_amount_during_differed_period = Endowment_amount_after_differed_period / 2
            });
        }
        else
        {
            return NotFound("No data found for the specified criteria.");
        }
    }
}








//router to get ages 

[Route("api/ages")]
[ApiController]
public class AgesController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public AgesController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetUniqueAges()
    {
        var uniqueAges = await _dbContext.EducatiionTables
            .Select(r => r.Age)
            .Distinct()
            .OrderBy(a => a)
            .ToListAsync();

        if (uniqueAges.Count > 0)
        {
            return Ok(new { ages = uniqueAges });
        }
        else
        {
            return NotFound("No data found.");
        }
    }
}


//router to get unique premium frequency to use in education insurance

[Route("api/frequency")]
[ApiController]
public class FrequencyController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public FrequencyController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetUniqueFrequencies()
    {
        try
        {
            var uniqueFrequencies = await _dbContext.EducatiionTables
                .Select(r => r.PremiumFrequency)
                .Distinct()
                .OrderBy(f => f)
                .ToListAsync();

            if (uniqueFrequencies.Count > 0)
            {
                return Ok(new { frequency = uniqueFrequencies });
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}




//router to get benefit years from education table to use in education insurance

[Route("api/benefitYears")]
[ApiController]
public class BenefitYearsController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public BenefitYearsController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetUniqueBenefitYears()
    {
        try
        {
            var uniqueBenefitYears = await _dbContext.EducatiionTables
                .Select(r => r.BenefitYears)
                .Distinct()
                .OrderBy(y => y)
                .ToListAsync();

            if (uniqueBenefitYears.Count > 0)
            {
                return Ok(new { benefitYears = uniqueBenefitYears });
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}





//router to get contribution year from education table to use in education insurance

[Route("api/contributionYear")]
[ApiController]
public class ContributionYearController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public ContributionYearController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetUniqueContributionYears()
    {
        try
        {
            var uniqueContributionYears = await _dbContext.EducatiionTables
                .Select(r => r.ContributionYears)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            if (uniqueContributionYears.Count > 0)
            {
                return Ok(new { contributionYear = uniqueContributionYears });
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}


//router to get rwandan provinces from rwanda table

[Route("api/Rwanda")]
[ApiController]
public class RwandaController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public RwandaController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetUniqueProvincesAndVillages()
    {
        try
        {
            var uniqueProvincesAndVillages = await _dbContext.RwandaTables
                .Select(r => new { Province = r.ProvinceListDescription, Village = r.VilageListDescription })
                .Distinct()
                .ToListAsync();

            if (uniqueProvincesAndVillages.Count > 0)
            {
                return Ok(new { records = uniqueProvincesAndVillages });
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}




//router to get naics code

[Route("api/naicscode")]
[ApiController]
public class NaicsCodeController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public NaicsCodeController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetNaicsCodes()
    {
        try
        {
            var naicsCodes = await _dbContext.NaicsCodeTables.ToListAsync();

            if (naicsCodes.Count > 0)
            {
                return Ok(naicsCodes);
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}

//router to get economic sub code

[Route("api/economicSubcode")]
[ApiController]
public class EconomicSubcodeController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public EconomicSubcodeController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetEconomicSubcodes()
    {
        try
        {
            var economicSubcodes = await _dbContext.EconomicSsectorCodeTables.ToListAsync();

            if (economicSubcodes.Count > 0)
            {
                return Ok(economicSubcodes);
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}

//router to get relationship type

[Route("api/relationshipType")]
[ApiController]
public class RelationshipTypeController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public RelationshipTypeController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetRelationshipTypes()
    {
        try
        {
            var relationshipTypes = await _dbContext.RelationshipTypeTables.ToListAsync();

            if (relationshipTypes.Count > 0)
            {
                return Ok(relationshipTypes);
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}


//router to get occupation

[Route("api/occupation")]
[ApiController]
public class OccupationController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public OccupationController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetOccupations()
    {
        try
        {
            var occupations = await _dbContext.OccupationTables.ToListAsync();

            if (occupations.Count > 0)
            {
                return Ok(occupations);
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}



//router to get income range 

[Route("api/incomerange")]
[ApiController]
public class IncomeRangeController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public IncomeRangeController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetIncomeRanges()
    {
        try
        {
            var incomeRanges = await _dbContext.IncomeRangeTables.ToListAsync();

            if (incomeRanges.Count > 0)
            {
                return Ok(incomeRanges);
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}

//router to get customer status

[Route("api/customerstatus")]
[ApiController]
public class CustomerStatusController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public CustomerStatusController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomerStatuses()
    {
        try
        {
            var customerStatuses = await _dbContext.CustomerStatusTables.ToListAsync();

            if (customerStatuses.Count > 0)
            {
                return Ok(customerStatuses);
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}



 // Route to get family insurance category all records from primeInsuranceTable

 [Route("api/primeinsurance")]
[ApiController]
public class PrimeInsuranceController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public PrimeInsuranceController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecords()
    {
        try
        {
            var records = await _dbContext.PrimeInsuranceTables.ToListAsync();

            if (records.Count > 0)
            {
                return Ok(records);
            }
            else
            {
                return NotFound("No data found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}


//router to post data in customer information table 

[Route("api/customerinformation")]
[ApiController]
public class CustomerInformationController : ControllerBase
{
    private readonly PrimeInsuranceDbContext _dbContext;

    public CustomerInformationController(PrimeInsuranceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomerInformation([FromBody]  CustomerInformationTable customerInfo)
    {
        try
        {
            if (customerInfo == null)
            {
                return BadRequest("Invalid customer information data.");
            }

            // Add the customer information to the DbContext
            _dbContext.CustomerInformationTables.Add(customerInfo);

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return StatusCode(201, new { successMessage = "Data submitted successfully." });
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                return BadRequest(new { errorMessage = "A customer with the same Customer ID already exists." });
            }

            return StatusCode(500, new { errorMessage = "Error occurred while inserting data into the database. Please try again." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { errorMessage = "An error occurred while processing the request." });
        }
    }
}


}

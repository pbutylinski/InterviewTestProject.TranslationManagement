using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using External.ThirdParty.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TranslationManagement.Api.Commands;
using TranslationManagement.Api.Controlers;

namespace TranslationManagement.Api.Controllers
{
    [ApiController]
    [Route("api/jobs/[action]")]
    public class TranslationJobController : ControllerBase
    {
        //private AppDbContext _context;
        private readonly ILogger<TranslatorManagementController> _logger;

        public TranslationJobController(IServiceScopeFactory scopeFactory, ILogger<TranslatorManagementController> logger)
        {
            //_context = scopeFactory.CreateScope().ServiceProvider.GetService<AppDbContext>();
            _logger = logger;
        }

        //[HttpGet]
        //public TranslationJob[] GetJobs()
        //{
        //    return _context.TranslationJobs.ToArray();
        //}

        [HttpPost]
        public bool CreateJob(CreateJobCommand job)
        {
            //job.Status = "New";
            //SetPrice(job);
            //_context.TranslationJobs.Add(job);
            //bool success = _context.SaveChanges() > 0;
            //if (success)
            //{
            //    var notificationSvc = new UnreliableNotificationService();
            //    while (!notificationSvc.SendNotification("Job created: " + job.Id).Result)
            //    {
            //    }

            //    _logger.LogInformation("New job notification sent");
            //}

            //return success;

            return false;
        }

        [HttpPost]
        public bool CreateJobWithFile(IFormFile file, string customer)
        {
            //var reader = new StreamReader(file.OpenReadStream());

            //// TODO: Use processors

            //var newJob = new CreateJobCommand()
            //{
            //    OriginalContent = content,
            //    TranslatedContent = "",
            //    CustomerName = customer,
            //};

            //SetPrice(newJob);

            //return CreateJob(newJob);

            return false;
        }

        [HttpPost]
        public string UpdateJobStatus(int jobId, int translatorId, string newStatus = "")
        {
            //_logger.LogInformation("Job status update request received: " + newStatus + " for job " + jobId.ToString() + " by translator " + translatorId);
            //if (typeof(JobStatuses).GetProperties().Count(prop => prop.Name == newStatus) == 0)
            //{
            //    return "invalid status";
            //}

            //var job = _context.TranslationJobs.Single(j => j.Id == jobId);

            //bool isInvalidStatusChange = (job.Status == JobStatuses.New && newStatus == JobStatuses.Completed) ||
            //                             job.Status == JobStatuses.Completed || newStatus == JobStatuses.New;
            //if (isInvalidStatusChange)
            //{
            //    return "invalid status change";
            //}

            //job.Status = newStatus;
            //_context.SaveChanges();
            return "updated";
        }
    }
}